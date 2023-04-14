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
    public class ServicesController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServicesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View(await _context.Services.ToListAsync());
        }

        // GET: Services/Details/5
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

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Name,Text,ImageFile")] Service service)
        {
            if (ModelState.IsValid)
            {

                if (service.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + service.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await service.ImageFile.CopyToAsync(fileStream);
                    }
                    service.Image = fileName;

                }



                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
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

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image,Name,Text,ImageFile")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (service.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + service.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await service.ImageFile.CopyToAsync(fileStream);
                        }
                        service.Image = fileName;

                    }


                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
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

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(decimal id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
