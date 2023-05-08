using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Controllers;
using WebApplication5.Helpers;
using WebApplication5.Models;


namespace WebApplication5Tests
{
    class UserControllerHelperTests
    {


        private IUserControllerHelper _userControllerHelper;
        private Dictionary<string, User> _users;

        [SetUp]
        public void BasicSetup()
        {           
            _userControllerHelper = new UserControllerHelper();
            _users = CreateFakeUsersDictionary();
        }


        [Test]
        public void AddRemoveUserCredit_FirstTime_AddsUser_ReturnsCredit()
        {
            // arrange
            BasicSetup();

            // act
            var userCredit = _userControllerHelper.AddRemoveUserCredit("NewUser", 5.00m,_users);

            // assert
            Assert.That(userCredit == 5.00m);
        }


        [Test]
        public void AddRemoveUserCredit_MultipleTimes_Accumulate_And_ReturnsCredit()
        {
            // arrange
            BasicSetup();

            // act
            var userCredit = _userControllerHelper.AddRemoveUserCredit("Harry", 5.00m, _users);

            // assert
            Assert.That(userCredit == 105.00m);
        }


        [Test]
        public void GetUserBalance_CantFindUser_Returns_0()
        {
            // arrange
            BasicSetup();

            // act
            var userBalance = _userControllerHelper.GetUserBalance("NewUser", _users);

            // assert
            Assert.That(userBalance == 0);
        }


        [Test]
        public void GetUserBalance_FindsUser_Returns_Credit()
        {
            // arrange
            BasicSetup();

            // act
            var userBalance = _userControllerHelper.GetUserBalance("Harry", _users);

            // assert
            Assert.That(userBalance == 100);
        }


        [Test]
        public void GetUserCreditBack_FindsUser_ReturnsValue_And_Resets_Credit()
        {
            // arrange
            BasicSetup();

            // act
            var returnedValueBalance = _userControllerHelper.GetUserCreditBack("Harry", _users);

            // assert
            Assert.That(returnedValueBalance == 100);
            var userBalance = _userControllerHelper.GetUserBalance("Harry", _users);
            Assert.That(userBalance == 0);
        }

        [Test]
        public void GetUserCreditBack_CantFindUser_Returns_0()
        {
            // arrange
            BasicSetup();

            // act
            var userBalance = _userControllerHelper.GetUserCreditBack("NewUser", _users);

            // assert
            Assert.That(userBalance == 0);
        }


        [Test]
        public void GetUserPurchases_FindsUser_ReturnsValue()
        {
            // arrange
            BasicSetup();

            // act
            var products = _userControllerHelper.GetUserPurchases("Harry", _users);

            // assert
            Assert.That(products[0].Name == "Product1");
            Assert.That(products[1].Name == "Product2");                     
        }

        [Test]
        public void GetUserPurchases_CantFindUser_Returns_EmptyList()
        {
            // arrange
            BasicSetup();

            // act
            var products = _userControllerHelper.GetUserPurchases("NewUser", _users);

            // assert
            Assert.That(products.Count == 0);
        }



        private Dictionary<string, User> CreateFakeUsersDictionary()
        {
            var products = new List<Product>()
            {
                new Product{Name="Product1", Quantity=1, Price =10.00m},
                new Product{Name="Product2", Quantity=2, Price =11.00m},
            };

            return new Dictionary<string, User>() { { "Harry", new User() { Name = "Harry", Balance = 100, PurchasedProducts = products } } };
        }
    }
}
