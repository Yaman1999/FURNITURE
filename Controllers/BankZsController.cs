using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FURNITURE.Models;

namespace FURNITURE.Controllers
{
    public class BankZsController : Controller
    {
        private readonly ModelContext _context;

        public BankZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: BankZs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankZs.ToListAsync());
        }

        // GET: BankZs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankZ = await _context.BankZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankZ == null)
            {
                return NotFound();
            }

            return View(bankZ);
        }

        // GET: BankZs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNumber,Cvv,Amount")] BankZ bankZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankZ);
        }

        // GET: BankZs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankZ = await _context.BankZs.FindAsync(id);
            if (bankZ == null)
            {
                return NotFound();
            }
            return View(bankZ);
        }

        // POST: BankZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CardNumber,Cvv,Amount")] BankZ bankZ)
        {
            if (id != bankZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankZExists(bankZ.Id))
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
            return View(bankZ);
        }

        // GET: BankZs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankZ = await _context.BankZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankZ == null)
            {
                return NotFound();
            }

            return View(bankZ);
        }

        // POST: BankZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var bankZ = await _context.BankZs.FindAsync(id);
            _context.BankZs.Remove(bankZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankZExists(decimal id)
        {
            return _context.BankZs.Any(e => e.Id == id);
        }
    }
}
