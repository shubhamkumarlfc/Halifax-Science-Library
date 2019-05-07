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

namespace HalifaxScienceLibrary_Project.Controllers
{
    public class monthly_expenseController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: monthly_expense
        public async Task<ActionResult> Index()
        {
            var monthly_expense = db.monthly_expense.Include(m => m.rent);
            return View(await monthly_expense.ToListAsync());
        }

        // GET: monthly_expense/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_expense monthly_expense = await db.monthly_expense.FindAsync(id);
            if (monthly_expense == null)
            {
                return HttpNotFound();
            }
            return View(monthly_expense);
        }

        // GET: monthly_expense/Create
        public ActionResult Create()
        {
            ViewBag.rent_id = new SelectList(db.rents, "rent_id", "year");
            return View();
        }

        // POST: monthly_expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "monthlyExpense_id,year,month,cost_heat,cost_water,cost_electricity,rent_id")] monthly_expense monthly_expense)
        {
            if (ModelState.IsValid)
            {
                db.monthly_expense.Add(monthly_expense);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.rent_id = new SelectList(db.rents, "rent_id", "year", monthly_expense.rent_id);
            return View(monthly_expense);
        }

        // GET: monthly_expense/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_expense monthly_expense = await db.monthly_expense.FindAsync(id);
            if (monthly_expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.rent_id = new SelectList(db.rents, "rent_id", "year", monthly_expense.rent_id);
            return View(monthly_expense);
        }

        // POST: monthly_expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "monthlyExpense_id,year,month,cost_heat,cost_water,cost_electricity,rent_id")] monthly_expense monthly_expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthly_expense).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.rent_id = new SelectList(db.rents, "rent_id", "year", monthly_expense.rent_id);
            return View(monthly_expense);
        }

        // GET: monthly_expense/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_expense monthly_expense = await db.monthly_expense.FindAsync(id);
            if (monthly_expense == null)
            {
                return HttpNotFound();
            }
            return View(monthly_expense);
        }

        // POST: monthly_expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            monthly_expense monthly_expense = await db.monthly_expense.FindAsync(id);
            db.monthly_expense.Remove(monthly_expense);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
