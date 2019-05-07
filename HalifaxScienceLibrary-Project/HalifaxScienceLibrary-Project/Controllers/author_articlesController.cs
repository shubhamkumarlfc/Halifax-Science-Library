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
    public class author_articlesController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: author_articles
        public async Task<ActionResult> Index()
        {
            var author_articles = db.author_articles.Include(a => a.article).Include(a => a.author).Include(a=> a.article.volume);
            return View(await author_articles.ToListAsync());
        }

        // GET: author_articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author_articles author_articles = await db.author_articles.FindAsync(id);
            if (author_articles == null)
            {
                return HttpNotFound();
            }
            return View(author_articles);
        }

        // GET: author_articles/Create
        public ActionResult Create()
        {
            ViewBag.article_id = new SelectList(db.articles, "article_id", "article_id");
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname");
            return View();
        }

        // POST: author_articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "C_id,article_id,time")] author_articles author_articles)
        {
            int author_id = author_articles.C_id;
            int article_id = author_articles.article_id;

            var author_article = db.author_articles.Where(m => m.article_id == article_id && m.C_id == author_id).FirstOrDefault();
            if (author_article != null) { 
                ViewBag.ErrorMessage = "Author already exists for same article ";
                var data = db.author_articles.Include(a => a.article).Include(a => a.author);
                return View("Index",await data.ToListAsync());
            }
             else if (ModelState.IsValid)
            {
                db.author_articles.Add(author_articles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.article_id = new SelectList(db.articles, "article_id", "article_id", author_articles.article_id);
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", author_articles.C_id);
            return View(author_articles);
        }

        // GET: author_articles/Edit/5
        public async Task<ActionResult> Edit(int? id,int? article_id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author_articles author_articles = await db.author_articles.FindAsync(id);
            if (author_articles == null)
            {
                return HttpNotFound();
            }
            ViewBag.article_id = new SelectList(db.articles, "article_id", "article_id", author_articles.article_id);
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", author_articles.C_id);
            return View(author_articles);
        }

        // POST: author_articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "C_id,article_id,time")] author_articles author_articles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author_articles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.article_id = new SelectList(db.articles, "article_id", "article_id", author_articles.article_id);
            ViewBag.C_id = new SelectList(db.authors, "C_id", "lname", author_articles.C_id);
            return View(author_articles);
        }

        // GET: author_articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author_articles author_articles = await db.author_articles.FindAsync(id);
            if (author_articles == null)
            {
                return HttpNotFound();
            }
            return View(author_articles);
        }

        // POST: author_articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            author_articles author_articles = await db.author_articles.FindAsync(id);
            db.author_articles.Remove(author_articles);
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
