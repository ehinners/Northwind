using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private NorthwindContext _northwindContext;
        public ProductController(NorthwindContext db) => _northwindContext = db;
        public ActionResult Category() => View(_northwindContext.Categories);

        public IActionResult ProductDetail(int id) => View(new ProductViewModel
        {
            category = _northwindContext.Categories.FirstOrDefault(b => b.CategoryId == id),
            Products = _northwindContext.Products.Where(p => p.CategoryId == id)
        });
    }
}