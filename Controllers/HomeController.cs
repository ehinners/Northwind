using System;
using Northwind.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        private NorthwindContext _northwindContext;
        public HomeController(NorthwindContext db) => _northwindContext = db;
        public IActionResult Index() => View(_northwindContext.Categories.OrderBy(b => b.CategoryName));
    }
}