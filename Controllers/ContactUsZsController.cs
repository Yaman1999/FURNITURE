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
    public class ContactUsZsController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactUsZs
        public async Task<IActionResult> Index()
        {

            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            return View(await _context.ContactUsZs.ToListAsync());
        }

        // GET: ContactUsZs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            if (id == null)
            {
                return NotFound();
            }

            var contactUsZ = await _context.ContactUsZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUsZ == null)
            {
                return NotFound();
            }

            return View(contactUsZ);
        }

        // GET: ContactUsZs/Create
        public IActionResult Create()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            return View();
        }

        // POST: ContactUsZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email,Message")] ContactUsZ contactUsZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsZ);
        }

        // GET: ContactUsZs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            if (id == null)
            {
                return NotFound();
            }

            var contactUsZ = await _context.ContactUsZs.FindAsync(id);
            if (contactUsZ == null)
            {
                return NotFound();
            }
            return View(contactUsZ);
        }

        // POST: ContactUsZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Phone,Email,Message")] ContactUsZ contactUsZ)
        {
            if (id != contactUsZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUsZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsZExists(contactUsZ.Id))
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
            return View(contactUsZ);
        }

        // GET: ContactUsZs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            if (id == null)
            {
                return NotFound();
            }

            var contactUsZ = await _context.ContactUsZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUsZ == null)
            {
                return NotFound();
            }

            return View(contactUsZ);
        }

        // POST: ContactUsZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactUsZ = await _context.ContactUsZs.FindAsync(id);
            _context.ContactUsZs.Remove(contactUsZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsZExists(decimal id)
        {
            return _context.ContactUsZs.Any(e => e.Id == id);
        }
    }
}
