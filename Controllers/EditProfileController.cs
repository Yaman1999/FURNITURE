using FURNITURE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FURNITURE.Controllers
{
	public class EditProfileController : Controller
	{
		private readonly ModelContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public EditProfileController(ModelContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}









		// GET: UserAccountZs/EditProfile/5
		public async Task<IActionResult> EditProfile(decimal? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ViewBag.id = HttpContext.Session.GetInt32("id");
			ViewBag.img = HttpContext.Session.GetString("img");
			ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
			ViewBag.userid = HttpContext.Session.GetInt32("id");

			var userAccountZ = await _context.UserAccountZs.FindAsync(id);

             
			if (userAccountZ == null)
			{
				return NotFound();
			}
			return View(userAccountZ);
		}



		// POST: UserAccountZs/EditProfile/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditProfile(decimal id, [Bind("Id,Fullname,Phone,Image,Email,ImageFile")] UserAccountZ userAccountZ ,string pass, string username)
		{
           
			


            if (ModelState.IsValid)
            {

                try {

                    if (userAccountZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + userAccountZ.ImageFile.FileName;
                        string extension = Path.GetExtension(userAccountZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userAccountZ.ImageFile.CopyToAsync(fileStream);
                        }
                        userAccountZ.Image = fileName;
                        HttpContext.Session.SetString("img", userAccountZ.Image);

                    }
                    else
                    {

                        ViewBag.img = HttpContext.Session.GetString("img");
                        userAccountZ.Image = ViewBag.img;

                    }

                    HttpContext.Session.SetString("username", username);

                    _context.Update(userAccountZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                {
                    throw; 
                }

                _context.Update(userAccountZ);
                await _context.SaveChangesAsync();



                LoginZ Login = (from acc in _context.LoginZs where acc.UserId.Equals(userAccountZ.Id) select acc).FirstOrDefault(); 


                if (Login is null) 
                { 
                    Login = new LoginZ();
                    Login.Id = userAccountZ.Id;
                    Login.UserName = username; 
                    _context.Add(Login);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Login.Passwordd = pass;
                    Login.UserName = username;
                    _context.Update(Login);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction("EditProfile","EditProfile");
			}
			return View(userAccountZ);
        }

        // GET: UserAccountZs/AdminProfile/5
        public async Task<IActionResult> AdminProfile(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            var userAccountZ = await _context.UserAccountZs.FindAsync(id);
           
              
               

            
            if (userAccountZ == null)
            {
                return NotFound();
            }
            return View(userAccountZ);
        }


        // POST: UserAccountZs/AdminProfile/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminProfile(decimal id, [Bind("Id,Fullname,Phone,Image,Email,ImageFile")] UserAccountZ userAccountZ, string pass, string username)
        {
           

   
            if (ModelState.IsValid)
            {



                try {

                    if (userAccountZ.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + userAccountZ.ImageFile.FileName;
                        
                        string extension = Path.GetExtension(userAccountZ.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userAccountZ.ImageFile.CopyToAsync(fileStream);
                        }
                        userAccountZ.Image = fileName;
                       
                        HttpContext.Session.SetString("img", userAccountZ.Image);

                    }
                    else
                    {
                        ViewBag.img = HttpContext.Session.GetString("img");
                        userAccountZ.Image = ViewBag.img;
                    }

                    HttpContext.Session.SetString("username", username);

                    _context.Update(userAccountZ); 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                {
                    throw; 
                }

                _context.Update(userAccountZ);
                await _context.SaveChangesAsync();



                LoginZ Login = (from acc in _context.LoginZs where acc.UserId.Equals(userAccountZ.Id) select acc).FirstOrDefault(); 

                if (Login is null) 
                { Login = new LoginZ();
                    Login.Id = userAccountZ.Id;
                    Login.UserName = username;
                    _context.Add(Login);
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    Login.Passwordd = pass;
                    Login.UserName = username;
                    _context.Update(Login);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction("AdminProfile", "EditProfile");
            }
            return View(userAccountZ);
        }







        private bool UserAccountZExists(decimal id)
		{
			return _context.UserAccountZs.Any(e => e.Id == id);
		}


	}
}
