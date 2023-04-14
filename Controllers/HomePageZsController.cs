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
    public class HomePageZsController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomePageZsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: HomePageZs
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View(await _context.HomePageZs.ToListAsync());
        }

        // GET: HomePageZs/Details/5
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

            var homePageZ = await _context.HomePageZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homePageZ == null)
            {
                return NotFound();
            }

            return View(homePageZ);
        }

        // GET: HomePageZs/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View();
        }

        // POST: HomePageZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Logo,Paragraph,Email,Phone,Address,Text1,ImageFile,ImageLogo,ImageFile2")] HomePageZ homePageZ)
        {
            if (ModelState.IsValid)
            {
                if (homePageZ.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + homePageZ.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await homePageZ.ImageFile.CopyToAsync(fileStream);
                    }
                    homePageZ.Image = fileName;
                }

                if (homePageZ.ImageFile2 != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + homePageZ.ImageFile2.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await homePageZ.ImageFile2.CopyToAsync(fileStream);
                    }
                    homePageZ.Image2 = fileName;
                }

                if (homePageZ.ImageLogo != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + homePageZ.ImageLogo.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await homePageZ.ImageLogo.CopyToAsync(fileStream);
                    }
                    homePageZ.Logo = fileName;
                }

                _context.Add(homePageZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homePageZ);
        }

        // GET: HomePageZs/Edit/5
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




            var homePageZ = await _context.HomePageZs.FindAsync(id);
            if (homePageZ == null)
            {
                return NotFound();
            }
            return View(homePageZ);
        }

        // POST: HomePageZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image,Logo,Paragraph,Email,Phone,Address,Text1,ImageFile,ImageLogo,ImageFile2")] HomePageZ homePageZ)
        {
            if (id != homePageZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (homePageZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" +
                        homePageZ.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(homePageZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))
                        {
                            await homePageZ.ImageFile.CopyToAsync(fileStream);
                        }
                        homePageZ.Image = fileName;

                    }
                   


                    if (homePageZ.ImageFile2 != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + homePageZ.ImageFile2.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePageZ.ImageFile2.CopyToAsync(fileStream);
                        }
                        homePageZ.Image2 = fileName;
                    }

                    if (homePageZ.ImageLogo != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + homePageZ.ImageLogo.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePageZ.ImageLogo.CopyToAsync(fileStream);
                        }
                        homePageZ.Logo = fileName;
                    }

                    _context.Update(homePageZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageZExists(homePageZ.Id))
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
            return View(homePageZ);
        }

        // GET: HomePageZs/Delete/5
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

            var homePageZ = await _context.HomePageZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homePageZ == null)
            {
                return NotFound();
            }

            return View(homePageZ);
        }

        // POST: HomePageZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var homePageZ = await _context.HomePageZs.FindAsync(id);
            _context.HomePageZs.Remove(homePageZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomePageZExists(decimal id)
        {
            return _context.HomePageZs.Any(e => e.Id == id);
        }
    }
}
