using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Helpers
{
    public interface IVendingMachineControllerHelper
    {
        Product BuildProduct(string productName, decimal price, int quantity);
    }
}