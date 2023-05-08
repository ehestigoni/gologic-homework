using System.Collections.Generic;
using WebApplication5.Models;

namespace WebApplication5.Stubs
{
    public class InitialListOfProducts
    {
        // Stubbed list of products
        private List<Product> _products = new List<Product>()
        {
            new Product{Name="Product1", Quantity=1, Price =10.00m},
            new Product{Name="Product2", Quantity=2, Price =11.00m},
            new Product{Name="Product3", Quantity=3, Price =12.00m},
            new Product{Name="Product4", Quantity=4, Price =13.50m},
        };


        public List<Product>  GetStubbedList()
        {
            return _products;
        }
    }
}