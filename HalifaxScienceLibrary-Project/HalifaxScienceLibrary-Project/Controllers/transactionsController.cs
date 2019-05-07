using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HalifaxScienceLibrary_Project;
using System.Globalization;

namespace HalifaxScienceLibrary_Project.Controllers
{
    public class transactionsController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: transactions
        public async Task<ActionResult> Index()
        {
            var transactions = db.transactions.Include(t => t.customer);
            return View(await transactions.ToListAsync());
        }

        // GET: transactions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: transactions/Create
        public ActionResult Create()
        {
            ViewBag.cust_id = new SelectList(db.customers, "cust_id", "fname");
            return View();
        }

        // POST: transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "trn_code,date,total_price,cust_id")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                
                db.transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cust_id = new SelectList(db.customers, "cust_id", "fname", transaction.cust_id);
            return View(transaction);
        }

        // GET: transactions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.cust_id = new SelectList(db.customers, "cust_id", "fname", transaction.cust_id);
            return View(transaction);
        }

        // POST: transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "trn_code,date,total_price,cust_id")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cust_id = new SelectList(db.customers, "cust_id", "fname", transaction.cust_id);
            return View(transaction);
        }

        // GET: transactions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = await db.transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            
            transaction transaction = await db.transactions.FindAsync(id);

            DateTime startDate = transaction.date;
            DateTime expiryDate = DateTime.Today.Subtract(TimeSpan.FromDays(30));
            if (startDate <= expiryDate)
            {
                ViewBag.ErrorMessage = "Transaction cannot be deleted as it is older than 30 days !! ";
                var transactions = db.transactions.ToListAsync();
                return View("Index", await transactions);
            }
            else { 
            db.transactions.Remove(transaction);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
