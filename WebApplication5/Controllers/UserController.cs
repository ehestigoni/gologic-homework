using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication5.Helpers;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UserController : ApiController
    {
        public UserController()
        {
            _sharedData = new SharedData.SharedData();
            _users = _sharedData.GetUsers();
            _userControllerHelper = new UserControllerHelper();
        }


        public UserController(Dictionary<string, User> users, IUserControllerHelper userControllerHelper)
        {
            _users = users;
            _userControllerHelper = userControllerHelper;
        }


        private SharedData.SharedData _sharedData;

        private IUserControllerHelper _userControllerHelper;

        private Dictionary<string, User> _users;

               
        [HttpGet]
        public decimal AddRemoveUserCredit(string user, decimal money)
        {
            return _userControllerHelper.AddRemoveUserCredit(user, money, _users);
        }


        [HttpGet]
        public decimal GetUserCreditBack(string user)
        {
            return _userControllerHelper.GetUserCreditBack(user, _users);
        }


        [HttpGet]
        public decimal GetUserBalance(string user)
        {
            return _userControllerHelper.GetUserBalance(user, _users);
        }


        [HttpGet]
        public List<Product> GetUserPurchases(string user)
        {
            return _userControllerHelper.GetUserPurchases(user, _users);
        }


        [HttpGet]
        public Product AddUserPurchases(string user, Product product)
        {
            return _userControllerHelper.AddUserPurchases(user, product, _users);
        }


    }
}