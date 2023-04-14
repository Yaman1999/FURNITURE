using FURNITURE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FURNITURE.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StatisticsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Statistics()
        {
            var customers = _context.UserAccountZs.ToList().Take(3);
            var products = _context.ProductZs.Include(x=>x.Category).ToList().TakeLast(5);

            var model = Tuple.Create<IEnumerable<UserAccountZ>,IEnumerable<ProductZ>>(customers, products);

            ViewBag.paymentssum = _context.PaymentZs.Sum(x => x.Amount);
            ViewBag.salessum = _context.PaymentZs.Sum(x=>x.Amount);
            ViewBag.contactuscount = _context.ContactUsZs.Count();
            ViewBag.orderscount = _context.ProductOrderZs.Where(x=>x.Status=="1").Count();
            ViewBag.productsscount = _context.ProductZs.Count();
            ViewBag.categoriescount = _context.CategoryZs.Count();
            ViewBag.customerscount = _context.UserAccountZs.Count();
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View(model);
        }
    }
}
