using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FURNITURE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FURNITURE.Controllers
{
    public class OrderZsController : Controller
    {
        private readonly ModelContext _context;

        public OrderZsController(ModelContext context)
        {
            _context = context;
        }

        // GET: OrderZs
        public async Task<IActionResult> Index()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            var modelContext = _context.OrderZs.Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: OrderZs/Details/5
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

            var orderZ = await _context.OrderZs
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderZ == null)
            {
                return NotFound();
            }

            return View(orderZ);
        }









        // GET: OrderZs/Create
        public IActionResult Create()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id");
            return View();
        }

        // POST: OrderZs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,UserId")] OrderZ orderZ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderZ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", orderZ.UserId);
            return View(orderZ);
        }








        // GET: OrderZs/Edit/5
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

            var orderZ = await _context.OrderZs.FindAsync(id);
            if (orderZ == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", orderZ.UserId);
            return View(orderZ);
        }

        // POST: OrderZs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Date,UserId")] OrderZ orderZ)
        {
            if (id != orderZ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderZ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderZExists(orderZ.Id))
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
            ViewData["UserId"] = new SelectList(_context.UserAccountZs, "Id", "Id", orderZ.UserId);
            return View(orderZ);
        }









        // GET: OrderZs/Delete/5
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

            var orderZ = await _context.OrderZs
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderZ == null)
            {
                return NotFound();
            }

            return View(orderZ);
        }

        // POST: OrderZs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderZ = await _context.OrderZs.FindAsync(id);
            _context.OrderZs.Remove(orderZ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }














        // GET: Unpaid Order Jointable3 for AdminSEARCH
        public IActionResult UnpaidOrders()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");


            var products = _context.ProductZs.Include(x => x.Category).ToList();
            var order = _context.OrderZs.Include(x => x.User).ToList();
            var productorder = _context.ProductOrderZs.ToList();

            var model = from o in order
                        join po in productorder on o.Id equals po.OrderId
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order = o, ProductOrder = po, Product = p };


            ViewBag.total = _context.ProductOrderZs.Where(x => x.Status == "0").Sum(x => x.Product.Price);

            return View(model);
        }


        //post unpaid orders

        [HttpPost]
        [ValidateAntiForgeryToken] // this accept only asp-action not regular action in  forms
        public IActionResult UnpaidOrders(DateTime? StartDate, DateTime? EndDate)
        {



            var products = _context.ProductZs.Include(x => x.Category).ToList();
            var order = _context.OrderZs.Include(x => x.User).ToList();
            var productorder = _context.ProductOrderZs.ToList();






            var model = from o in order
                        join po in productorder on o.Id equals po.OrderId
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order = o, ProductOrder = po, Product = p };



            if (StartDate == null && EndDate == null)
            {

                ViewBag.total = model.Where(x => x.ProductOrder.Status == "0").Sum(x => x.ProductOrder.Product.Price);
                return View(model);
            }
            else if (StartDate == null && EndDate != null)
            {
                var result = model.Where(x => x.ProductOrder.Status == "0").Where(p => p.Order.Date.Value.Date <= EndDate).ToList();
                ViewBag.total = result.Sum(x => x.ProductOrder.Product.Price);
                return View(result);
            }
            else if (StartDate != null && EndDate == null)
            {
                var result2 = model.Where(x => x.ProductOrder.Status == "0").Where(p => p.Order.Date.Value.Date >= StartDate).ToList();
                ViewBag.total = result2.Sum(x => x.ProductOrder.Product.Price);
                return View(result2);
            }
            else
            {
                var result3 = model.Where(x => x.ProductOrder.Status == "0").Where(p => p.Order.Date.Value.Date >= StartDate && p.Order.Date.Value.Date <= EndDate).ToList();
                ViewBag.total = result3.Sum(x => x.ProductOrder.Product.Price);
                return View(result3);
            }
        }





        // GET: OrderJointable3forAdminSEARCH
        public  IActionResult Search()
        {
            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");


              var products = _context.ProductZs.Include(x=>x.Category).ToList();
              var order = _context.OrderZs.Include(x=>x.User).ToList();
             var productorder = _context.ProductOrderZs.ToList();

            var model = from o in order join po in productorder on o.Id equals po.OrderId 
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order=o,ProductOrder=po,Product=p };


            ViewBag.total = _context.ProductOrderZs.Where(x=>x.Status=="1").Sum(x => x.Product.Price);

            return View(model);
        }


        //post search

        [HttpPost]
        [ValidateAntiForgeryToken] // this accept only asp-action not regular action in  forms
        public  IActionResult Search(DateTime? StartDate,DateTime? EndDate) 
        {



            var products = _context.ProductZs.Include(x => x.Category).ToList();
            var order = _context.OrderZs.Include(x => x.User).ToList();
            var productorder = _context.ProductOrderZs.ToList();






            var model = from o in order
                        join po in productorder on o.Id equals po.OrderId
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order = o, ProductOrder = po, Product = p };



            if (StartDate == null && EndDate==null)
            {

                ViewBag.total = model.Where(x=>x.ProductOrder.Status=="1").Sum(x => x.ProductOrder.Product.Price);
                return View(model);
            }
            else if (StartDate == null && EndDate != null)
            {
                var result = model.Where(x => x.ProductOrder.Status == "1").Where(p => p.Order.Date.Value.Date <= EndDate).ToList();
                ViewBag.total = result.Sum(x => x.ProductOrder.Product.Price);
                return View(result);
            }
            else if (StartDate != null && EndDate == null)
            {
                var result2 = model.Where(x => x.ProductOrder.Status == "1").Where(p => p.Order.Date.Value.Date >= StartDate).ToList();
                ViewBag.total = result2.Sum(x => x.ProductOrder.Product.Price);
                return View(result2);
            }
            else
            {
                var result3 = model.Where(x => x.ProductOrder.Status == "1").Where(p => p.Order.Date.Value.Date >= StartDate && p.Order.Date.Value.Date <=EndDate).ToList();
                ViewBag.total = result3.Sum(x => x.ProductOrder.Product.Price);
                return View(result3);
            }
        }
















        public IActionResult Reports() 
        {


            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");


            var products = _context.ProductZs.Include(x => x.Category).ToList();
            var order = _context.OrderZs.Include(x => x.User).ToList();
            var productorder = _context.ProductOrderZs.ToList();


            



            var model = from o in order
                        join po in productorder on o.Id equals po.OrderId
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order = o, ProductOrder = po, Product = p };

            ViewBag.total = _context.ProductOrderZs.Where(x => x.Status == "1").Sum(x => x.Product.Price);

            ViewBag.orders = model.Count();

            ViewBag.TotalAmount = _context.PaymentZs.Sum(x => x.Amount);

            ViewBag.profits = (double)ViewBag.TotalAmount * 0.20;

            var payments = _context.PaymentZs.Include(x=>x.User).ToList();

            var modelNew = Tuple.Create<IEnumerable<JoinTable3>,IEnumerable<PaymentZ>>(model, payments);


            return View(modelNew); 
        }


        //post reports

        [HttpPost]
        [ValidateAntiForgeryToken] // this makes us use asp-action not regular action in search form
        public IActionResult Reports(DateTime? StartDate, DateTime? EndDate)
        {


            ViewBag.img = HttpContext.Session.GetString("img");
            ViewBag.pass = HttpContext.Session.GetString("pass");
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.userid = HttpContext.Session.GetInt32("id");



            var products = _context.ProductZs.Include(x => x.Category).ToList();
            var order = _context.OrderZs.Include(x => x.User).ToList();
            var productorder = _context.ProductOrderZs.ToList();






            var model = from o in order
                        join po in productorder on o.Id equals po.OrderId
                        join p in products on po.ProductId equals p.Id
                        select new JoinTable3 { Order = o, ProductOrder = po, Product = p };


            ViewBag.total = _context.ProductOrderZs.Where(x => x.Status == "1").Sum(x => x.Product.Price);

            ViewBag.orders = model.Count();

            ViewBag.TotalAmount = _context.PaymentZs.Sum(x => x.Amount);


            var payments = _context.PaymentZs.Include(x => x.User).ToList();








            if (StartDate == null && EndDate == null)
            {
                ViewBag.TotalAmount = _context.PaymentZs.Sum(x => x.Amount);
                ViewBag.profits = (double)ViewBag.TotalAmount * 0.20;
                var modelNew = Tuple.Create<IEnumerable<JoinTable3>, IEnumerable<PaymentZ>>(model, payments);

                return View(modelNew);
            }
            else if (StartDate == null && EndDate != null)
            {
                var modelNew1 = Tuple.Create<IEnumerable<JoinTable3>, IEnumerable<PaymentZ>>(model, payments.Where(x => x.PayDate.Value.Date <= EndDate));
                ViewBag.TotalAmount = modelNew1.Item2.Sum(x => x.Amount);
                ViewBag.profits = (double)ViewBag.TotalAmount * 0.20;
                return View(modelNew1);
            }
            else if (StartDate != null && EndDate == null)
            {
                var modelNew2 = Tuple.Create<IEnumerable<JoinTable3>, IEnumerable<PaymentZ>>(model, payments.Where(x => x.PayDate.Value.Date >= StartDate));
                ViewBag.TotalAmount = modelNew2.Item2.Sum(x => x.Amount);
                ViewBag.profits = (double)ViewBag.TotalAmount * 0.20;

                return View(modelNew2);
            }
            else
            {
                var modelNew3 = Tuple.Create<IEnumerable<JoinTable3>, IEnumerable<PaymentZ>>(model, payments.Where(x => x.PayDate.Value.Date <= EndDate && x.PayDate.Value.Date>=StartDate));

                ViewBag.TotalAmount = modelNew3.Item2.Sum(x => x.Amount);
                ViewBag.profits = (double)ViewBag.TotalAmount * 0.20;

                return View(modelNew3);
            }
        }






        private bool OrderZExists(decimal id)
        {
            return _context.OrderZs.Any(e => e.Id == id);
        }
    }
}
