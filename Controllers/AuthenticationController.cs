using FURNITURE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FURNITURE.Controllers
{
    public class AuthenticationController : Controller
    {
		private readonly ModelContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AuthenticationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }



		// POST: UserAccountZs/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([Bind("Id,Fullname,Phone,Image,Email,ImageFile")] UserAccountZ userAccountZ,string username,string pass)
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

				LoginZ login = new LoginZ();
				login.UserName = username;
				login.Passwordd = pass;
				login.UserId= userAccountZ.Id;
				login.RoleId = 2;

				_context.Add(login);
				await _context.SaveChangesAsync();



				return RedirectToAction("Index","Home");
			}
			return View(userAccountZ);
		}



        // POST: LoginZs/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Id,UserName,Passwordd,UserId,RoleId")] LoginZ loginZ)
        {
           var auth = _context.LoginZs.Where(x=>x.UserName==loginZ.UserName && x.Passwordd==loginZ.Passwordd).FirstOrDefault();

			if (auth!= null)
			{
				var customer = _context.UserAccountZs.Where(p=>p.Id==auth.UserId).FirstOrDefault();
                switch (auth.RoleId)
                {
                    case 1:
						HttpContext.Session.SetInt32("adminloggedin", 70);

                        HttpContext.Session.SetString("useremail", customer.Email);
                        HttpContext.Session.SetString("userfullname", customer.Fullname);
                        HttpContext.Session.SetString("img", customer.Image);
                        HttpContext.Session.SetString("pass", auth.Passwordd);
                        HttpContext.Session.SetInt32("id", (int)customer.Id);
                        HttpContext.Session.SetString("username", auth.UserName);

                        return RedirectToAction("Index", "Home");
                    case 2:

                        HttpContext.Session.SetInt32("loggedin", 60);

						HttpContext.Session.SetString("useremail",customer.Email);
						HttpContext.Session.SetInt32("usernumber", (int)customer.Phone);
						HttpContext.Session.SetString("userfullname",customer.Fullname);
                        HttpContext.Session.SetString("img", customer.Image);
                        HttpContext.Session.SetString("pass", auth.Passwordd);
                        HttpContext.Session.SetInt32("id", (int)customer.Id);
                        HttpContext.Session.SetString("username", auth.UserName);

                        return RedirectToAction("Index", "Home");
                }

            }
			else
			{
				
				ViewBag.NotCorrect = 0;
			}
			return View(loginZ);
        }



		// Log out To clear session
		public IActionResult Logout()
		{
		
			HttpContext.Session.Clear();


			return RedirectToAction("Index","Home");

		}



    }
}
