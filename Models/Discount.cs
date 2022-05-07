using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Northwind.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        
        public int Code { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int ProductID { get; set; }
        [Range(0, 1.00, ErrorMessage = "Please enter a value between {1} and {2}")]
        public decimal DiscountPercent { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public Product Product { get; set; }

        ICollection<Product> Products {get; set;}

    }
}