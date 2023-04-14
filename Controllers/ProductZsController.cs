using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FURNITURE.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FURNITURE.Controllers
{
    public class ProductZsController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductZsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductZs
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            var modelContext = _context.ProductZs.Include(p => p.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductZs/Details/5
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

            var productZ = await _context.ProductZs
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productZ == null)
            {
                return NotFound();
            }

            return View(productZ);
        }

        // GET: ProductZs/Create
        public IActionResult Create()

        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            ViewData["CategoryId"] = new SelectList(_context.CategoryZs, "Id", "Name");
            return View();
        }

        // POST: ProductZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Price,Value,CategoryId,ImageFile")] ProductZ productZ)
        {
            if (ModelState.IsValid)
            {
                if (productZ.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + productZ.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await productZ.ImageFile.CopyToAsync(fileStream);
                    }
                    productZ.Image = fileName;

                }
                _context.Add(productZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryZs, "Id", "Name", productZ.CategoryId);
            return View(productZ);
        }

        // GET: ProductZs/Edit/5
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

            var productZ = await _context.ProductZs.FindAsync(id);
            if (productZ == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryZs, "Id", "Name", productZ.CategoryId);
            return View(productZ);
        }

        // POST: ProductZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Image,Price,Value,CategoryId,ImageFile")] ProductZ productZ)
        {
            if (id != productZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (productZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" +
                        productZ.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(productZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))
                        {
                            await productZ.ImageFile.CopyToAsync(fileStream);
                        }
                        productZ.Image = fileName;
                    }
                    _context.Update(productZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductZExists(productZ.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.CategoryZs, "Id", "Name", productZ.CategoryId);
            return View(productZ);
        }

        // GET: ProductZs/Delete/5
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

            var productZ = await _context.ProductZs
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productZ == null)
            {
                return NotFound();
            }

            return View(productZ);
        }

        // POST: ProductZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productZ = await _context.ProductZs.FindAsync(id);
            _context.ProductZs.Remove(productZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductZExists(decimal id)
        {
            return _context.ProductZs.Any(e => e.Id == id);
        }
    }
}
