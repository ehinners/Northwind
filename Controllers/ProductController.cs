using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private NorthwindContext _northwindContext;
        public ProductController(NorthwindContext db) => _northwindContext = db;
        public ActionResult Category() => View(_northwindContext.Categories.OrderBy(b => b.CategoryName));

        public IActionResult ProductDetail(int id) => View(new ProductViewModel
        {
            category = _northwindContext.Categories.FirstOrDefault(b => b.CategoryId == id),
            Products = _northwindContext.Products.Where(p => p.CategoryId == id && p.Discontinued == false)
        });

        public ActionResult DiscountDetail()
        {
            IEnumerable<Discount> discounts = (from discount in _northwindContext.Discounts
            join product in _northwindContext.Products on discount.ProductID equals product.ProductId
            where discount.StartTime <= DateTime.Now && discount.EndTime > DateTime.Now
            select new Discount { 
                DiscountID = discount.DiscountID,
                Code = discount.Code,
                StartTime = discount.StartTime,
                EndTime = discount.EndTime,
                ProductID = discount.ProductID,
                DiscountPercent = discount.DiscountPercent,
                Title = discount.Title,
                Description = discount.Description,
                Product = product
            }).ToList();

            return View(discounts);
        }
    }
}