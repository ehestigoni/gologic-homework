using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.SharedData
{
    public interface ISharedData
    {
        Dictionary<string, Product> GetAvailableProducts();

        Dictionary<string, decimal> GetUserBalances();

        Dictionary<string, List<Product>> GetUserPurchases();

        Dictionary<string, User> GetUsers();
    }
}