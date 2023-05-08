using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Helpers
{
    public class UserControllerHelper: IUserControllerHelper
    {
        public decimal GetUserBalance(string user, Dictionary<string, User> users)
        {
            if (users.ContainsKey(user))
            {
                return users[user].Balance;
            }
            return 0;
        }


        public decimal AddRemoveUserCredit(string user, decimal money, Dictionary<string, User> users)
        {
            if (users.ContainsKey(user))
            {
                users[user].Balance += money;
            }
            else
            {
                users[user] = new User() { Name = user, Balance = money, PurchasedProducts = new List<Product>() };
            }

            return users[user].Balance;
        }


        public Product AddUserPurchases(string user, Product product, Dictionary<string, User> users)
        {
            if (users.ContainsKey(user))
            {
                bool bAlreadyContainsProduct = users[user].PurchasedProducts.Any(x => x.Name == product.Name);
                if (!bAlreadyContainsProduct)
                {
                    users[user].PurchasedProducts.Add(product);
                }
                else
                {
                    Product prod = users[user].PurchasedProducts.FirstOrDefault(p => p.Name == product.Name);
                    prod.Quantity += product.Quantity;
                }
            }
            else
            {
                users[user] = new User() { Name = user, Balance = 0, PurchasedProducts = new List<Product>() { product } };
            }
            return product;
        }


        public decimal GetUserCreditBack(string user, Dictionary<string, User> users)
        {
            var moneyToBeReturned = 0.00m;
            if (users.ContainsKey(user))
            {
                moneyToBeReturned = users[user].Balance;
                users[user].Balance = 0;
            }

            return moneyToBeReturned;
        }

        public List<Product> GetUserPurchases(string user, Dictionary<string, User> users)
        {
            if (users.ContainsKey(user))
            {
                return users[user].PurchasedProducts;
            }
            return new List<Product>();
        }
    }
}