using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            var discounts = _northwindContext.Discounts.Include(p => p.Product);

            return View(discounts);
        }
    }
}