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
    public class book_authorController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: book_author
        public async Task<ActionResult> Index()
        {
            var book_author = db.book_author.Include(b => b.author).Include(b => b.book);
            return View(await book_author.ToListAsync());
        }

        // GET: book_author/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book_author book_author = await db.book_author.FindAsync(id);
            if (book_author == null)
            {
                return HttpNotFound();
            }
            return View(book_author);
        }

        // GET: book_author/Create
        public ActionResult Create()
        {
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname");
            ViewBag.book_id = new SelectList(db.books, "book_id", "name");
            return View();
        }

        // POST: book_author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "book_id,C_id,time")] book_author book_author)
        {
            if (ModelState.IsValid)
            {
                db.book_author.Add(book_author);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", book_author.C_id);
            ViewBag.book_id = new SelectList(db.books, "book_id", "name", book_author.book_id);
            return View(book_author);
        }

        // GET: book_author/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book_author book_author = await db.book_author.FindAsync(id);
            if (book_author == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", book_author.C_id);
            ViewBag.book_id = new SelectList(db.books, "book_id", "name", book_author.book_id);
            return View(book_author);
        }

        // POST: book_author/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "book_id,C_id,time")] book_author book_author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", book_author.C_id);
            ViewBag.book_id = new SelectList(db.books, "book_id", "name", book_author.book_id);
            return View(book_author);
        }

        // GET: book_author/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book_author book_author = await db.book_author.FindAsync(id);
            if (book_author == null)
            {
                return HttpNotFound();
            }
            return View(book_author);
        }

        // POST: book_author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            book_author book_author = await db.book_author.FindAsync(id);
            db.book_author.Remove(book_author);
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
