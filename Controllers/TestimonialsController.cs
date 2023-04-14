using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FURNITURE.Models;
using Microsoft.AspNetCore.Http;

namespace FURNITURE.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            var modelContext = _context.Testimonials.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Status,UserId")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", testimonial.UserId);
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.Include(x=>x.User).FirstOrDefaultAsync(x=> x.Id==id);

            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", testimonial.UserId);
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Message,Status,UserId")] Testimonial testimonial)
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                    {
                   
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", testimonial.UserId);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(decimal id)
        {
            return _context.Testimonials.Any(e => e.Id == id);
        }
    }
}
