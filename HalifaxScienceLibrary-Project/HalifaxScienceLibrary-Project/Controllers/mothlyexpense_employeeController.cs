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
    public class mothlyexpense_employeeController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: mothlyexpense_employee
        public async Task<ActionResult> Index()
        {
            var mothlyexpense_employee = db.mothlyexpense_employee.Include(m => m.employee).Include(m => m.monthly_expense);
            return View(await mothlyexpense_employee.ToListAsync());
        }

        // GET: mothlyexpense_employee/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mothlyexpense_employee mothlyexpense_employee = await db.mothlyexpense_employee.FindAsync(id);
            if (mothlyexpense_employee == null)
            {
                return HttpNotFound();
            }
            return View(mothlyexpense_employee);
        }

        // GET: mothlyexpense_employee/Create
        public ActionResult Create()
        {
            ViewBag.SIN = new SelectList(db.employees, "SIN", "fname");
            ViewBag.monthlyExpense_id = new SelectList(db.monthly_expense, "monthlyExpense_id", "year");
            return View();
        }

        // POST: mothlyexpense_employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "hours_worked,date,SIN,monthlyExpense_id")] mothlyexpense_employee mothlyexpense_employee)
        {
            if (ModelState.IsValid)
            {
                db.mothlyexpense_employee.Add(mothlyexpense_employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SIN = new SelectList(db.employees, "SIN", "fname", mothlyexpense_employee.SIN);
            ViewBag.monthlyExpense_id = new SelectList(db.monthly_expense, "monthlyExpense_id", "year", mothlyexpense_employee.monthlyExpense_id);
            return View(mothlyexpense_employee);
        }

        // GET: mothlyexpense_employee/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mothlyexpense_employee mothlyexpense_employee = await db.mothlyexpense_employee.FindAsync(id);
            if (mothlyexpense_employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.SIN = new SelectList(db.employees, "SIN", "fname", mothlyexpense_employee.SIN);
            ViewBag.monthlyExpense_id = new SelectList(db.monthly_expense, "monthlyExpense_id", "year", mothlyexpense_employee.monthlyExpense_id);
            return View(mothlyexpense_employee);
        }

        // POST: mothlyexpense_employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "hours_worked,date,SIN,monthlyExpense_id")] mothlyexpense_employee mothlyexpense_employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mothlyexpense_employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SIN = new SelectList(db.employees, "SIN", "fname", mothlyexpense_employee.SIN);
            ViewBag.monthlyExpense_id = new SelectList(db.monthly_expense, "monthlyExpense_id", "year", mothlyexpense_employee.monthlyExpense_id);
            return View(mothlyexpense_employee);
        }

        // GET: mothlyexpense_employee/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mothlyexpense_employee mothlyexpense_employee = await db.mothlyexpense_employee.FindAsync(id);
            if (mothlyexpense_employee == null)
            {
                return HttpNotFound();
            }
            return View(mothlyexpense_employee);
        }

        // POST: mothlyexpense_employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            mothlyexpense_employee mothlyexpense_employee = await db.mothlyexpense_employee.FindAsync(id);
            db.mothlyexpense_employee.Remove(mothlyexpense_employee);
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
