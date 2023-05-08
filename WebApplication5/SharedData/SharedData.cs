using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;
using System.Runtime.Caching;
using WebApplication5.Stubs;

namespace WebApplication5.SharedData
{
    public class SharedData: ISharedData
    {
        const string _availableProducts = "availableProducts";
        const string _users = "users";
        const string _userBalances = "userBalances";
        const string _userPurchases = "userPurchases";
        
        public Dictionary<string, Product>  GetAvailableProducts()
        {
            if (! MemoryCache.Default.Contains(_availableProducts))
            {
                MemoryCache.Default[_availableProducts] = new InitialListOfProducts().GetStubbedList().ToDictionary(x => x.Name, x => x);
            }

            return MemoryCache.Default[_availableProducts] as Dictionary<string, Product>;
        }

        public Dictionary<string, decimal> GetUserBalances()
        {
            if (!MemoryCache.Default.Contains(_userBalances))
            {
                MemoryCache.Default[_userBalances] = new Dictionary<string, decimal>();
            }

            return MemoryCache.Default[_userBalances] as Dictionary<string, decimal>;
        }

        public Dictionary<string, List<Product>> GetUserPurchases()
        {
            if (!MemoryCache.Default.Contains(_userPurchases))
            {
                MemoryCache.Default[_userPurchases] = new Dictionary<string, List<Product>>();
            }

            return MemoryCache.Default[_userPurchases] as Dictionary<string, List<Product>>;
        }


        public Dictionary<string, User> GetUsers()
        {
            if (!MemoryCache.Default.Contains(_users))
            {
                MemoryCache.Default[_users] = new Dictionary<string, User>();
            }

            return MemoryCache.Default[_users] as Dictionary<string, User>;
        }

    }



}