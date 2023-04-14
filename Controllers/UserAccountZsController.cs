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
    public class UserAccountZsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserAccountZsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: UserAccountZs
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View(await _context.UserAccountZs.ToListAsync());
        }

        // GET: UserAccountZs/Details/5
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

            var userAccountZ = await _context.UserAccountZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccountZ == null)
            {
                return NotFound();
            }

            return View(userAccountZ);
        }

        // GET: UserAccountZs/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View();
        }

        // POST: UserAccountZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,Phone,Image,Email,ImageFile")] UserAccountZ userAccountZ)
        {
            if (ModelState.IsValid)
            {
                if (userAccountZ.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + userAccountZ.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userAccountZ.ImageFile.CopyToAsync(fileStream);
                    }
                    userAccountZ.Image = fileName;

                }
                _context.Add(userAccountZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccountZ);
        }

        // GET: UserAccountZs/Edit/5
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

            var userAccountZ = await _context.UserAccountZs.FindAsync(id);
            if (userAccountZ == null)
            {
                return NotFound();
            }
            return View(userAccountZ);
        }

        // POST: UserAccountZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fullname,Phone,Image,Email,ImageFile")] UserAccountZ userAccountZ)
        {
            if (id != userAccountZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userAccountZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" +
                        userAccountZ.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(userAccountZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))
                        {
                            await userAccountZ.ImageFile.CopyToAsync(fileStream);
                        }
                        userAccountZ.Image = fileName;
                    }
                    _context.Update(userAccountZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountZExists(userAccountZ.Id))
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
            return View(userAccountZ);
        }

        // GET: UserAccountZs/Delete/5
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

            var userAccountZ = await _context.UserAccountZs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccountZ == null)
            {
                return NotFound();
            }

            return View(userAccountZ);
        }

        // POST: UserAccountZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userAccountZ = await _context.UserAccountZs.FindAsync(id);
            _context.UserAccountZs.Remove(userAccountZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountZExists(decimal id)
        {
            return _context.UserAccountZs.Any(e => e.Id == id);
        }
    }
}
