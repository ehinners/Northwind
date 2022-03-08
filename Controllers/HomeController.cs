using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;
namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public HomeController(NorthwindContext db) => _northwindContext = db;

        
        public ActionResult Index()
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
    }).Take(3).ToList();

    return View(discounts);
}
    }
}