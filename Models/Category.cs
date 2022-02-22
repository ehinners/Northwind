using System.Collections.Generic;

namespace Northwind.Models
{
    public class Category
    {
        // primary key
        public   int  CategoryId {get; set;}
        public   string CategoryName {get; set;}
        public   string Description {get; set;}
        public   ICollection<Product> Products {get; set;}
    }
}