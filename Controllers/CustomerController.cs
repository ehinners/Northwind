using System;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class CustomerController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public CustomerController(NorthwindContext db) => _northwindContext = db;

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer model)
        {
            _northwindContext.AddCustomer(model);
            return RedirectToAction("Register");
        }
    }
}