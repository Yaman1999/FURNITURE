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
    public class RoleZsController : Controller
    {
        private readonly ModelContext _context;

        public RoleZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: RoleZs
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleZs.ToListAsync());
        }

        // GET: RoleZs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleZ = await _context.RoleZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleZ == null)
            {
                return NotFound();
            }

            return View(roleZ);
        }

        // GET: RoleZs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RoleZ roleZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleZ);
        }

        // GET: RoleZs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleZ = await _context.RoleZs.FindAsync(id);
            if (roleZ == null)
            {
                return NotFound();
            }
            return View(roleZ);
        }

        // POST: RoleZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name")] RoleZ roleZ)
        {
            if (id != roleZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleZExists(roleZ.Id))
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
            return View(roleZ);
        }

        // GET: RoleZs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleZ = await _context.RoleZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleZ == null)
            {
                return NotFound();
            }

            return View(roleZ);
        }

        // POST: RoleZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var roleZ = await _context.RoleZs.FindAsync(id);
            _context.RoleZs.Remove(roleZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleZExists(decimal id)
        {
            return _context.RoleZs.Any(e => e.Id == id);
        }
    }
}
