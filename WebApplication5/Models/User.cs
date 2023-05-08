
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public decimal Balance { get; set; }

        public List<Product> PurchasedProducts;
}
}