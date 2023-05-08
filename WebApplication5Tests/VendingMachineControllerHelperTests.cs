using FakeItEasy;
using NUnit.Framework;
using WebApplication5.Helpers;


namespace WebApplication5Tests
{
    class VendingMachineControllerHelperTests
    {
        private VendingMachineControllerHelper _vendingMachineControllerHelper;

        [SetUp]
        public void BasicSetup()
        {
            _vendingMachineControllerHelper = new VendingMachineControllerHelper();            
        }

        [Test]
        public void BuildProduct_Builds_Product()
        {
            // arrange
            BasicSetup();

            // act
            var product = _vendingMachineControllerHelper.BuildProduct("productName", 10.00m, 100);

            // assert
            Assert.That(product.Name == "productName");
            Assert.That(product.Price == 10.00m);
            Assert.That(product.Quantity== 100);
        }
    }
}
