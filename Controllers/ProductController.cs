using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private int discountCodeMin = 1000;
        private int discountCodeMax = 9999;
        private NorthwindContext _northwindContext;
        public ProductController(NorthwindContext db) => _northwindContext = db;
        public ActionResult Category() => View(_northwindContext.Categories.OrderBy(b => b.CategoryName));

        public IActionResult ProductDetail(int id) => View(new ProductViewModel
        {
            category = _northwindContext.Categories.FirstOrDefault(b => b.CategoryId == id),
            Products = _northwindContext.Products.Where(p => p.CategoryId == id && p.Discontinued == false)
        });

        public IActionResult Index(int id){
            ViewBag.id = id;
            return View(_northwindContext.Categories.OrderBy(c => c.CategoryName));
        }

        public ActionResult DiscountDetail()
        {
            var discounts = _northwindContext.Discounts.Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now).Include(p => p.Product);

            return View(discounts);
        }

        [Authorize(Roles = "northwind-employee")]
        public IActionResult AddDiscount() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "northwind-employee")]
        public IActionResult AddDiscount(Discount model)
        {
            if (ModelState.IsValid)
            {
                if (_northwindContext.Discounts.Any(b => b.Title == model.Title))
                {
                    ModelState.AddModelError("", "Title MUST be unique");
                }
                else
                {
                    Random rnd = new Random();
                    int newCode = rnd.Next(discountCodeMin, discountCodeMax+1);
                    while(_northwindContext.Discounts.Any(b => b.Code == newCode))
                    {
                        newCode = rnd.Next(discountCodeMin, discountCodeMax+1);
                    }
                    model.Code = newCode;
                    _northwindContext.AddDiscount(model);
                    return RedirectToAction("DiscountDetail");
                }
            }
            return View();
        }

        [Authorize(Roles = "northwind-employee")]
        public IActionResult EditDiscount(int id) => View( _northwindContext.Discounts.FirstOrDefault(c => c.DiscountID == id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "northwind-employee")]
        public IActionResult EditDiscount(Discount model)
        {
            if (ModelState.IsValid)
            {
                if (_northwindContext.Discounts.Any(b => b.Title == model.Title && (b.DiscountID != model.DiscountID)))
                {
                    ModelState.AddModelError("", "Title must be unique");
                    return View(model);
                }
                else
                {
                    _northwindContext.EditDiscount(model);
                    return RedirectToAction("DiscountDetail");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "northwind-employee")]
        public IActionResult DeleteDiscount(int id)
        {
            _northwindContext.DeleteDiscount(_northwindContext.Discounts.FirstOrDefault(b => b.DiscountID == id));
            return RedirectToAction("DiscountDetail");
        }
    }
}