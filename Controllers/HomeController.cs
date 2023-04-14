using FURNITURE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FURNITURE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var homepage = _context.HomePageZs.ToList();
            var testi = _context.Testimonials.Include(x=>x.User).ToList();
            var contact = _context.ContactUsZs.ToList();
            var aboutUs = _context.AboutUsZs.ToList();
            var services = _context.Services.ToList();

            var model = Tuple.Create<IEnumerable<HomePageZ>, IEnumerable<Testimonial>,IEnumerable<ContactUsZ>, IEnumerable<AboutUsZ>, IEnumerable<Service>>(homepage, testi, contact,aboutUs, services);



            ViewBag.adminloggedin = HttpContext.Session.GetInt32("adminloggedin");
            ViewBag.loggedin = HttpContext.Session.GetInt32("loggedin");
            return View(model);
        }
        public async Task<IActionResult> Products()
        {

            var homepage = await _context.HomePageZs.ToListAsync();
            var category =await _context.CategoryZs.Take(5).ToListAsync();
            var products =await _context.ProductZs.ToListAsync();
            var model =  Tuple.Create<IEnumerable<HomePageZ>, IEnumerable<CategoryZ>,IEnumerable<ProductZ>>(homepage, category,products);

            ViewBag.adminloggedin = HttpContext.Session.GetInt32("adminloggedin");
             ViewBag.loggedin = HttpContext.Session.GetInt32("loggedin");


            return View(model);
        }
        public async Task<IActionResult> Gallery()
        {
            var homepage = await _context.HomePageZs.ToListAsync();
            var gallery = await _context.Galleries.ToListAsync();

            var model = Tuple.Create<IEnumerable<HomePageZ>,IEnumerable<Gallery>>(homepage, gallery);
            ViewBag.adminloggedin = HttpContext.Session.GetInt32("adminloggedin");
           ViewBag.loggedin = HttpContext.Session.GetInt32("loggedin");

            return View(model);
        }



        // GET: About_us

        public IActionResult About_us()
        {

            var homepage = _context.HomePageZs.ToList();
            var aboutus = _context.AboutUsZs.ToList();
            var services = _context.Services.ToList();
           var model = Tuple.Create<IEnumerable<HomePageZ>,IEnumerable<AboutUsZ>,IEnumerable<Service>>(homepage, aboutus,services);


            ViewBag.adminloggedin = HttpContext.Session.GetInt32("adminloggedin");
            ViewBag.loggedin = HttpContext.Session.GetInt32("loggedin");


            return View(model);
        }








        // GET: Contact_us
        public async Task<IActionResult> Contact_us()
        {


            var model = await _context.HomePageZs.ToListAsync();


            ViewBag.adminloggedin = HttpContext.Session.GetInt32("adminloggedin");
            ViewBag.loggedin = HttpContext.Session.GetInt32("loggedin");
            return View(model);
        }
        // POST: Contact_us/Create_from_ContactUspage
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_from_ContactUspage([Bind("Id,Name,Phone,Email,Message")] ContactUsZ contactUsZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsZ);
                await _context.SaveChangesAsync();
                return RedirectToAction("Contact_us","Home");
            }
            return View(contactUsZ);
        }

        // POST: Contact_us/Create_from_homepage
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_from_homepage([Bind("Id,Name,Phone,Email,Message")] ContactUsZ contactUsZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsZ);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
