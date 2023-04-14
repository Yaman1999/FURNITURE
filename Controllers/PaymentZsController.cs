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
    public class PaymentZsController : Controller
    {
        private readonly ModelContext _context;

        public PaymentZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: PaymentZs
        public async Task<IActionResult> Index()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            var modelContext = _context.PaymentZs.Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: PaymentZs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentZ = await _context.PaymentZs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentZ == null)
            {
                return NotFound();
            }

            return View(paymentZ);
        }

        // GET: PaymentZs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id");
            return View();
        }

        // POST: PaymentZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,PayDate,UserId")] PaymentZ paymentZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", paymentZ.UserId);
            return View(paymentZ);
        }

        // GET: PaymentZs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentZ = await _context.PaymentZs.FindAsync(id);
            if (paymentZ == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", paymentZ.UserId);
            return View(paymentZ);
        }

        // POST: PaymentZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Amount,PayDate,UserId")] PaymentZ paymentZ)
        {
            if (id != paymentZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentZExists(paymentZ.Id))
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
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", paymentZ.UserId);
            return View(paymentZ);
        }

        // GET: PaymentZs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentZ = await _context.PaymentZs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentZ == null)
            {
                return NotFound();
            }

            return View(paymentZ);
        }

        // POST: PaymentZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var paymentZ = await _context.PaymentZs.FindAsync(id);
            _context.PaymentZs.Remove(paymentZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentZExists(decimal id)
        {
            return _context.PaymentZs.Any(e => e.Id == id);
        }
    }
}
