using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication5.Helpers;
using WebApplication5.Models;
using WebApplication5.SharedData;

namespace WebApplication5.Controllers
{
    public class VendingMachineController:ApiController
    {

        public VendingMachineController()
        {
            _sharedData = new SharedData.SharedData();
            _vendingMachineControllerHelper = new VendingMachineControllerHelper();        
            _availableProducts = _sharedData.GetAvailableProducts();
            _userControllerHelper = new UserControllerHelper();
         
        }

        public VendingMachineController( IVendingMachineControllerHelper vendingMachineControllerHelper,
            Dictionary<string, Product> availableProducts, IUserControllerHelper userControllerHelper, ISharedData sharedData)
        {
            _vendingMachineControllerHelper = vendingMachineControllerHelper;
            _availableProducts = availableProducts;
            _userControllerHelper = userControllerHelper;
            _sharedData = sharedData;
        }


        private ISharedData _sharedData;
        private IVendingMachineControllerHelper _vendingMachineControllerHelper;
        private Dictionary<string, Product> _availableProducts;
        private IUserControllerHelper _userControllerHelper;
                     

        [HttpGet]
        public List<Product> GetAvailableProducts()
        {
            return _availableProducts.Values.ToList();
        }
                      

        [HttpGet]
        public bool PurchaseProduct(string user, string productName, int quantity)
        {
            bool bRetVal = false;

            // Check if we have the requested quantity in stock
            var product = GetProductByName(productName);            
            if ((product != null) && product.Quantity >= quantity)
            {
                // check user has enough balance
                var moneyRequired = product.Price * quantity;                
                var userBalance = _userControllerHelper.GetUserBalance(user, _sharedData.GetUsers());
                if (userBalance > moneyRequired)
                {
                    // proceed with purchase
                    
                    // reduce stock
                    _availableProducts[productName].Quantity -= quantity;

                    // reduce user balance
                    _userControllerHelper.AddRemoveUserCredit(user, -moneyRequired, _sharedData.GetUsers());

                    // keep track of user purchases 
                    var userProduct = _vendingMachineControllerHelper.BuildProduct(product.Name, product.Price, quantity);

                    _userControllerHelper.AddUserPurchases(user, userProduct, _sharedData.GetUsers());

                    bRetVal = true;
                }                
            }

            return bRetVal;
        }


        [HttpGet]
        public Product GetProductByName(string strName)
        {
            var Prod = from prod in GetAvailableProducts()
                       where prod.Name.Equals(strName)
                       select prod;

            return Prod.ToList().FirstOrDefault();
        }

    }
}