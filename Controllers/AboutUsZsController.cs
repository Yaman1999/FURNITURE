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
    public class AboutUsZsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AboutUsZsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AboutUsZs
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View(await _context.AboutUsZs.ToListAsync());
        }

        // GET: AboutUsZs/Details/5
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

            var aboutUsZ = await _context.AboutUsZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUsZ == null)
            {
                return NotFound();
            }

            return View(aboutUsZ);
        }

        // GET: AboutUsZs/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View();
        }

        // POST: AboutUsZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Paragraph1,Paragraph2,Paragraph3,ImageFile")] AboutUsZ aboutUsZ)
        {
            if (ModelState.IsValid)
            {
                if (aboutUsZ.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + aboutUsZ.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await aboutUsZ.ImageFile.CopyToAsync(fileStream);
                    }
                    aboutUsZ.Image = fileName;

                }
                _context.Add(aboutUsZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUsZ);
        }

        // GET: AboutUsZs/Edit/5
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

            var aboutUsZ = await _context.AboutUsZs.FindAsync(id);
            if (aboutUsZ == null)
            {
                return NotFound();
            }
            return View(aboutUsZ);
        }

        // POST: AboutUsZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image,Paragraph1,Paragraph2,Paragraph3,ImageFile")] AboutUsZ aboutUsZ)
        {
            if (id != aboutUsZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutUsZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" +
                        aboutUsZ.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(aboutUsZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))
                        {
                            await aboutUsZ.ImageFile.CopyToAsync(fileStream);
                        }
                        aboutUsZ.Image = fileName;
                    }
                    _context.Update(aboutUsZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsZExists(aboutUsZ.Id))
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
            return View(aboutUsZ);
        }

        // GET: AboutUsZs/Delete/5
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

            var aboutUsZ = await _context.AboutUsZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUsZ == null)
            {
                return NotFound();
            }

            return View(aboutUsZ);
        }

        // POST: AboutUsZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutUsZ = await _context.AboutUsZs.FindAsync(id);
            _context.AboutUsZs.Remove(aboutUsZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsZExists(decimal id)
        {
            return _context.AboutUsZs.Any(e => e.Id == id);
        }
    }
}
