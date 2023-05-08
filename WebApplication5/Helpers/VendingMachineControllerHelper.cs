using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;
using WebApplication5.Stubs;

namespace WebApplication5.Helpers
{
    public class VendingMachineControllerHelper: IVendingMachineControllerHelper
    {

        public Product BuildProduct(string productName, decimal price, int quantity)
        {
            return new Product()
            {
                Name = productName,
                Price = price,
                Quantity = quantity
            };
        }
    }
}