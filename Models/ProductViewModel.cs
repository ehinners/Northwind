using System.Collections.Generic;

namespace Northwind.Models
{
    public class ProductViewModel
    {
        public Category category { get; set; }

        public Discount discount { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}