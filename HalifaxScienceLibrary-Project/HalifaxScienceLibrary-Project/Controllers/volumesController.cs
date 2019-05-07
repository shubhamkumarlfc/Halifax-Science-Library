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
    public class volumesController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: volumes
        public async Task<ActionResult> Index()
        {
            var volumes = db.volumes.Include(v => v.magazine);
            return View(await volumes.ToListAsync());
        }

        // GET: volumes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volume volume = await db.volumes.FindAsync(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            return View(volume);
        }

        // GET: volumes/Create
        public ActionResult Create()
        {
            ViewBag.magazine_id = new SelectList(db.magazines, "C_id", "name");
            return View();
        }

        // POST: volumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "year,magazine_id")] volume volume)
        {
            if (ModelState.IsValid)
            {
                db.volumes.Add(volume);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.magazine_id = new SelectList(db.magazines, "C_id", "name", volume.magazine_id);
            return View(volume);
        }

        // GET: volumes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volume volume = await db.volumes.FindAsync(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            ViewBag.magazine_id = new SelectList(db.magazines, "C_id", "name", volume.magazine_id);
            return View(volume);
        }

        // POST: volumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "volume_id,year,magazine_id")] volume volume)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volume).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.magazine_id = new SelectList(db.magazines, "C_id", "name", volume.magazine_id);
            return View(volume);
        }

        // GET: volumes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volume volume = await db.volumes.FindAsync(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            return View(volume);
        }

        // POST: volumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            volume volume = await db.volumes.FindAsync(id);
            db.volumes.Remove(volume);
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
