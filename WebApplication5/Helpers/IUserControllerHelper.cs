using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Helpers
{
    public interface IUserControllerHelper
    {
        decimal GetUserBalance(string user, Dictionary<string, User> users);

        decimal AddRemoveUserCredit(string user, decimal money, Dictionary<string, User> users);

        Product AddUserPurchases(string user, Product product, Dictionary<string, User> users);

        decimal GetUserCreditBack(string user, Dictionary<string, User> users);

        List<Product> GetUserPurchases(string user, Dictionary<string, User> users);
    }
}