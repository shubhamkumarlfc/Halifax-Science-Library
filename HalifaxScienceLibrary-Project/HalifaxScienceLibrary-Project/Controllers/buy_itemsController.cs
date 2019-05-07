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
    public class buy_itemsController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: buy_items
        public async Task<ActionResult> Index()
        {
            var buy_items = db.buy_items.Include(b => b.item).Include(b => b.transaction);
            return View(await buy_items.ToListAsync());
        }

        // GET: buy_items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_items buy_items = await db.buy_items.FindAsync(id);
            if (buy_items == null)
            {
                return HttpNotFound();
            }
            return View(buy_items);
        }

        // GET: buy_items/Create
        public ActionResult Create()
        {
            ViewBag.C_id = new SelectList(db.items, "C_id", "C_id");
            ViewBag.trn_code = new SelectList(db.transactions, "trn_code", "trn_code");
            return View();
        }

        // POST: buy_items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "trn_code,C_id,time")] buy_items buy_items)
        {
            var items = db.items.Where(x => x.C_id == buy_items.C_id).ToArray();
            var total_price = db.transactions.Where(x => x.trn_code == buy_items.trn_code).FirstOrDefault();
            double sum = (float)total_price.total_price;
            foreach (var item in items)
            {
                sum += item.price;
            }
            if(sum >= 500)
            {
                sum = sum * (1-1.25 * (float)total_price.customer.discount_code)/100;
            }
            if (ModelState.IsValid)
            {
                total_price.total_price = (decimal) sum;
                db.Entry(total_price).State = EntityState.Modified;
                db.buy_items.Add(buy_items);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.C_id = new SelectList(db.items, "C_id", "C_id", buy_items.C_id);
            ViewBag.trn_code = new SelectList(db.transactions, "trn_code", "trn_code", buy_items.trn_code);
            return View(buy_items);
        }

        // GET: buy_items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_items buy_items = await db.buy_items.FindAsync(id);
            if (buy_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_id = new SelectList(db.items, "C_id", "C_id", buy_items.C_id);
            ViewBag.trn_code = new SelectList(db.transactions, "trn_code", "date", buy_items.trn_code);
            return View(buy_items);
        }

        // POST: buy_items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "trn_code,C_id,time")] buy_items buy_items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy_items).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.C_id = new SelectList(db.items, "C_id", "C_id", buy_items.C_id);
            ViewBag.trn_code = new SelectList(db.transactions, "trn_code", "date", buy_items.trn_code);
            return View(buy_items);
        }

        // GET: buy_items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy_items buy_items = await db.buy_items.FindAsync(id);
            if (buy_items == null)
            {
                return HttpNotFound();
            }
            return View(buy_items);
        }

        // POST: buy_items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            buy_items buy_items = await db.buy_items.FindAsync(id);
            db.buy_items.Remove(buy_items);
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
