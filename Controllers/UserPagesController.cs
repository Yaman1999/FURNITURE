using FURNITURE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using NLog.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using Org.BouncyCastle.Utilities.Net;

namespace FURNITURE.Controllers
{
    public class UserPagesController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public UserPagesController(ModelContext context, IWebHostEnvironment webHostEnvironment,IConfiguration configuration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

       
      





        public async Task<IActionResult> Discover()
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            return View(await _context.CategoryZs.ToListAsync());
        }


        // page to view the product to each category
        public async Task<IActionResult> Productss(int id)
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            var productss = await _context.ProductZs.Where(x => x.CategoryId == id).ToListAsync();

            return View(productss);
        }



       



        [HttpGet]
        public  IActionResult AddTestimonials()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddTestimonials(Testimonial testimonial)
        {

          

            ViewBag.id = HttpContext.Session.GetInt32("id");

            testimonial.UserId = ViewBag.id;
            testimonial.Status = "Under Procces";
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Discover","UserPages");
        }






       


        // Search method for products in userpage

        public async Task<IActionResult> Products(string ProductName)
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            var Products = await _context.ProductZs.ToListAsync();
            if (!String.IsNullOrEmpty(ProductName))
            {
                Products =  Products.Where(x=> x.Name.ToLower().Contains(ProductName.ToLower())).ToList();
            }

            return View( Products);
        }




        // get MyBag 
        public  IActionResult MyBag(int id)
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            var productOrder = _context.ProductOrderZs.Include(x=>x.Product).ToList();
            var order = _context.OrderZs.Where(x=>x.UserId==id).Include(x=>x.User).ToList();


            var model = from o in order join po in productOrder on o.Id equals po.OrderId
                        select new JoinTable2 { productOrder = po, order = o};


            


           ViewBag.total = model.Where(x=>x.productOrder.Status=="0"&&x.order.UserId==id).Sum(x => x.productOrder.Product.Price);
          

            return View(model);
        }





        public IActionResult AddToCart(int id)
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");


            OrderZ order = new OrderZ();
            order.UserId = ViewBag.userid;
            order.Date = DateTime.Now;

            _context.Add(order);
            _context.SaveChanges();


            ProductOrderZ product_order = new ProductOrderZ();
            product_order.OrderId = order.Id;
            product_order.ProductId = id;
            product_order.Status = "0";


            _context.Add(product_order);
            _context.SaveChanges();


            return RedirectToAction("Products","UserPages");
        }








        public async  Task<IActionResult> MyPayments(int id )
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");


            var productorder = await _context.ProductOrderZs.Include(x => x.Product).Include(p => p.Order)
                .Where(x=>x.Status=="1"&&x.Order.User.Id==id).ToListAsync();


            return View(productorder);
        }

        public IActionResult ContactUsViaEmail()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            return View();
        }

        // POST: ContactUsZs/Create Contact us via email
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUsViaEmail([Bind("Id,Name,Phone,Email,Message")] ContactUsZ contactUsZ)
        {
            if (ModelState.IsValid)
            {

                ViewBag.id = HttpContext.Session.GetInt32("id");
                ViewBag.img = HttpContext.Session.GetString("img");
                ViewBag.pass = HttpContext.Session.GetString("pass");
                ViewBag.username = HttpContext.Session.GetString("username");
                ViewBag.userid = HttpContext.Session.GetInt32("id");
                ViewBag.email = HttpContext.Session.GetString("useremail");
                ViewBag.fullname = HttpContext.Session.GetString("userfullname");
                ViewBag.phone = HttpContext.Session.GetInt32("usernumber");

                contactUsZ.Name= ViewBag.fullname;
                contactUsZ.Email= ViewBag.email;
                contactUsZ.Phone= ViewBag.phone;


                _context.Add(contactUsZ);
                await _context.SaveChangesAsync();
                return RedirectToAction("Discover", "UserPages");
            }
            return View(contactUsZ);
        }


      

        //get
        // Check Credit card 

        
        public  IActionResult Checkout()
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");

            return View();
        }


        // POST: 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Checkout([Bind("Id,CardNumber,Cvv,Amount")] BankZ bankZ)
        {

            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");




            var checkcard = _context.BankZs.Where(x => x.CardNumber == bankZ.CardNumber && x.Cvv == bankZ.Cvv).FirstOrDefault();
            var amount = _context.ProductOrderZs.Where(x=>x.Order.UserId == HttpContext.Session.GetInt32("id") && x.Status=="0")
                .Sum(x => x.Product.Price );



            if (checkcard != null )
            {
                if (amount==0)
                {
                    ViewBag.NoProductsInCart = true;
                }
                else if (checkcard.Amount >= amount )
                {

                    var product = _context.ProductOrderZs.Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderId == x.Order.Id).ToList();

                    PaymentZ payment = new PaymentZ();
                    payment.Amount = amount;
                    payment.PayDate = DateTime.Now;
                    payment.UserId = HttpContext.Session.GetInt32("id");

                    _context.Add(payment);
                    _context.SaveChanges();

                    foreach (var item in product)
                    {

                        item.Status = "1";
                        _context.Update(item);
                        _context.SaveChanges();

                    }
                    //BankZ UserCard = new BankZ();
                    var UserCard = _context.BankZs.Where(x => x.Id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
                    UserCard.Amount = checkcard.Amount - amount;


                    _context.Update(UserCard);
                    _context.SaveChanges();

                    var user = _context.UserAccountZs.Where(x => x.Id == HttpContext.Session.GetInt32("id")).FirstOrDefault();



                    //var Renderer = new ChromePdfRenderer();
                    //var pdf = Renderer.RenderHtmlAsPdf($" <h1> The Total is : $ {amount}  </h1> \n <h1>  Product names :\n {theOrder}  </h1> <br> " +
                    //    $"<br> <h3>Fisrt Name :</h3>{user.Fullname}" +
                    //    $"<br> <h3>Your Email :</h3>{user.Email} <h3>Your Number :</h3> {user.Phone} <br>");
                    //pdf.SaveAs("Invoice.pdf");

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("yaman", "awawdehyaman51@gmail.com"));
                    message.To.Add(MailboxAddress.Parse(user.Email));
                    message.Subject = "Invoice";
                    var builder = new BodyBuilder();
                    builder.HtmlBody = "<p> Thank you for shopping at [Furnish Store]. We hope that it’s exactly what you were looking for. </p>";
                    builder.Attachments.Add(@"C:\Users\Msi1\Downloads\Invoice.pdf");
                    message.Body = builder.ToMessageBody();


                    string emailaddress = "awawdehyaman51@gmail.com";
                    string password = "ipblmrjbtlgklxtp";

                    SmtpClient client = new SmtpClient();
                     try
                     {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate(emailaddress, password);
                        client.Send(message);

                     }
                    catch (Exception ex)
                     {
 
                        throw ex;
                     }
                     finally
                     {
                        client.Disconnect(true);
                        client.Dispose();
                     }
                    return Redirect("/UserPages/MyPayments/" + HttpContext.Session.GetInt32("id"));

                }
                else
                {
                    ViewBag.NOMONEY = true;
                    
                    return View();
                }
            }
            else
            {
                ViewBag.NotCorrectCard = true;
            }
            return View();
        }

    }
}
