using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public HomeController(NorthwindContext db) => _northwindContext = db;

        
        public ActionResult Index()
        {
            var discounts = _northwindContext.Discounts.Include(p => p.Product).Take(3);
           return View(discounts);
        }
    }
}