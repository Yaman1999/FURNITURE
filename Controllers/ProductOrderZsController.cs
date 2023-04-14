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
    public class ProductOrderZsController : Controller
    {
        private readonly ModelContext _context;

        public ProductOrderZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProductOrderZs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProductOrderZs.Include(p => p.Order).Include(p => p.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductOrderZs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderZ = await _context.ProductOrderZs
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrderZ == null)
            {
                return NotFound();
            }

            return View(productOrderZ);
        }

        // GET: ProductOrderZs/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.OrderZs, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.ProductZs, "Id", "Id");
            return View();
        }

        // POST: ProductOrderZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,ProductId,OrderId")] ProductOrderZ productOrderZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productOrderZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.OrderZs, "Id", "Id", productOrderZ.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductZs, "Id", "Id", productOrderZ.ProductId);
            return View(productOrderZ);
        }

        // GET: ProductOrderZs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderZ = await _context.ProductOrderZs.FindAsync(id);
            if (productOrderZ == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.OrderZs, "Id", "Id", productOrderZ.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductZs, "Id", "Id", productOrderZ.ProductId);
            return View(productOrderZ);
        }

        // POST: ProductOrderZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Status,ProductId,OrderId")] ProductOrderZ productOrderZ)
        {
            if (id != productOrderZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOrderZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOrderZExists(productOrderZ.Id))
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
            ViewData["OrderId"] = new SelectList(_context.OrderZs, "Id", "Id", productOrderZ.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductZs, "Id", "Id", productOrderZ.ProductId);
            return View(productOrderZ);
        }

        // GET: ProductOrderZs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrderZ = await _context.ProductOrderZs
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrderZ == null)
            {
                return NotFound();
            }

            return View(productOrderZ);
        }

        // POST: ProductOrderZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productOrderZ = await _context.ProductOrderZs.FindAsync(id);
            _context.ProductOrderZs.Remove(productOrderZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOrderZExists(decimal id)
        {
            return _context.ProductOrderZs.Any(e => e.Id == id);
        }
    }
}
