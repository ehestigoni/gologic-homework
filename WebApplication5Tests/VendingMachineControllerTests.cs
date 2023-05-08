using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Controllers;
using WebApplication5.Helpers;
using WebApplication5.Models;
using WebApplication5.SharedData;

namespace WebApplication5Tests
{
    public class VendingMachineControllerTests
    {

        private IVendingMachineControllerHelper _vendingMachineControllerHelper;
        private IUserControllerHelper _userControllerHelper;
        private ISharedData _sharedData;
        private Dictionary<string, Product> _availableProducts;
        VendingMachineController _vendingMachineController;


        [SetUp]
        public void BasicSetup()
        {
            _availableProducts = CreateFakeAvailableProductsDictionary();
            _vendingMachineControllerHelper = A.Fake<IVendingMachineControllerHelper>();
            _userControllerHelper = A.Fake<IUserControllerHelper>();
            _sharedData = A.Fake<ISharedData>();

            _vendingMachineController = new VendingMachineController(_vendingMachineControllerHelper, _availableProducts, _userControllerHelper, _sharedData);
        }


        [Test]
        public void GetAvailableProducts_SanityTest()
        {
            // arrange
            BasicSetup();
            var expectedAvailableProducts = CreateFakeAvailableProductsList();

            // act
            var availableProducts = _vendingMachineController.GetAvailableProducts();

            // assert
            Assert.That(availableProducts[0].Name == expectedAvailableProducts[0].Name);
            Assert.That(availableProducts[0].Price == expectedAvailableProducts[0].Price);
            Assert.That(availableProducts[0].Quantity == expectedAvailableProducts[0].Quantity);

            Assert.That(availableProducts[1].Name == expectedAvailableProducts[1].Name);
            Assert.That(availableProducts[1].Price == expectedAvailableProducts[1].Price);
            Assert.That(availableProducts[1].Quantity == expectedAvailableProducts[1].Quantity);
        }


        [Test]
        public void GetAvailableProducts_NoStock_StillReturnsProduct_quantity_0()
        {
            // arrange
            BasicSetup();
            var productsDictionary = new List<Product>()
            {
                new Product{Name="Product1", Quantity=0, Price =10.00m},
            }.ToDictionary(x => x.Name, x => x);

            _vendingMachineController = new VendingMachineController(_vendingMachineControllerHelper, productsDictionary, _userControllerHelper, _sharedData);


            // act
            var availableProducts = _vendingMachineController.GetAvailableProducts();

            // assert
            Assert.That(availableProducts[0].Name == "Product1");
            Assert.That(availableProducts[0].Quantity == 0);
            Assert.That(availableProducts[0].Price == 10.00m);
        }


        [Test]
        public void PurchaseProduct_CantFindsProduct_Returns_False()
        {
            // arrange
            BasicSetup();

            // act
            var bRes = _vendingMachineController.PurchaseProduct("Harry", "NonexistentProduct", 1);

            // assert
            Assert.That(bRes == false);
        }


        [Test]
        public void PurchaseProduct_FindsProduct_NotInStock_Returns_False()
        {
            // arrange
            BasicSetup();

            // act
            var bRes = _vendingMachineController.PurchaseProduct("Harry", "Product1", 5);

            // assert
            Assert.That(bRes == false);
        }



        [Test]
        public void PurchaseProduct_NotEnoughCredit_Returns_False()
        {
            // arrange
            BasicSetup();

            // act
            var bRes = _vendingMachineController.PurchaseProduct("Harry", "Product4", 1);

            // assert
            Assert.That(bRes == false);
        }



        [Test]
        public void PurchaseProduct_AllGood_DeductUserBalance_Returns_True()
        {
            // arrange
            BasicSetup();
            A.CallTo(() => _userControllerHelper.AddRemoveUserCredit("Harry", A<decimal>.Ignored, A<Dictionary<string, User>>.Ignored)).Returns(90);
            A.CallTo(() => _userControllerHelper.GetUserBalance("Harry", A<Dictionary<string, User>>.Ignored)).Returns(100);
            // act
            var bRes = _vendingMachineController.PurchaseProduct("Harry", "Product1", 1);

            // assert
            Assert.That(bRes == true);

            A.CallTo(() => _userControllerHelper.AddRemoveUserCredit("Harry", A<decimal>.Ignored, A<Dictionary<string, User>>.Ignored)).MustHaveHappened();
        }


        [Test]
        public void PurchaseProduct_AllGood_DeductsProductAvailability_Returns_True()
        {
            // arrange
            BasicSetup();
            A.CallTo(() => _userControllerHelper.GetUserBalance("Harry", A<Dictionary<string, User>>.Ignored)).Returns(100);

            // act
            var bRes = _vendingMachineController.PurchaseProduct("Harry", "Product1", 1);

            // assert
            Assert.That(bRes == true);

            var availableProducts = _vendingMachineController.GetAvailableProducts();
            Assert.That(availableProducts[0].Quantity == 0);
        }



        [Test]
        public void PurchaseProduct_AllGood_AddsToUserPurchases_Returns_True()
        {
            // arrange
            BasicSetup();
            A.CallTo(() => _vendingMachineControllerHelper.BuildProduct("Product1", A<decimal>.Ignored, A<int>.Ignored))
                .Returns(new Product() { Name = "NewProduct", Price = 1000, Quantity = 2000 });

            A.CallTo(() => _userControllerHelper.GetUserBalance("NewUser", A<Dictionary<string, User>>.Ignored)).Returns(100);



            // act
            var bRes = _vendingMachineController.PurchaseProduct("NewUser", "Product1", 1);

            // assert
            Assert.That(bRes == true);

            A.CallTo(() => _vendingMachineControllerHelper.BuildProduct("Product1", A<decimal>.Ignored, A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => _userControllerHelper.AddUserPurchases("NewUser", A<Product>.Ignored, A<Dictionary<string, User>>.Ignored)).MustHaveHappened();          

        }




        [Test]
        public void GetProductByName_FoundProduct_Returns_Product()
        {
            // arrange
            BasicSetup();

            // act
            var product = _vendingMachineController.GetProductByName("Product1");

            // assert
            Assert.That(product.Name == "Product1");
        }










        // Test Helpers

        private List<Product> CreateFakeAvailableProductsList()
        {
            List<Product> products = new List<Product>()
            {
                new Product{Name="Product1", Quantity=1, Price =10.00m},
                new Product{Name="Product2", Quantity=2, Price =11.00m},
                new Product{Name="Product3", Quantity=3, Price =12.00m},
                new Product{Name="Product4", Quantity=4, Price =200.00m},
            };
            return products;

        }

        private Dictionary<string, Product> CreateFakeAvailableProductsDictionary()
        {
            return CreateFakeAvailableProductsList().ToDictionary(x => x.Name, x => x);

        }

        private Dictionary<string, decimal> CreateFakeUserBalances()
        {
            var userBalances = new Dictionary<string, decimal>()
            {
                {"Harry", 100.00m},
                {"Gina", 200.00m},
                {"Bill", 300.00m},

            };
            return userBalances;
        }


        private Dictionary<string, List<Product>> CreateFakeUserPurchases()
        {
            var products1 = new List<Product>()
            {
                new Product{Name="Product1", Quantity=1, Price =10.00m},
                new Product{Name="Product2", Quantity=2, Price =11.00m},
            };

            var products2 = new List<Product>()
            {
                new Product{Name="Product3", Quantity=3, Price =12.00m},
                new Product{Name="Product4", Quantity=4, Price =13.50m},
            };


            var userPurchases = new Dictionary<string, List<Product>>()
            {
                {"Harry", products1 },
                {"Gina", products2 },

            };
            return userPurchases;
        }
    }
}