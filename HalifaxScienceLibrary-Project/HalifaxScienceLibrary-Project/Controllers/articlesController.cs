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
    public class articlesController : Controller
    {
        private HSLEntities db = new HSLEntities();

        // GET: articles1
        public async Task<ActionResult> Index()
        {
            var articles = db.articles.Include(a => a.magazine).Include(a => a.volume);
            return View(await articles.ToListAsync());
        }

        // GET: articles1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = await db.articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: articles1/Create
        public ActionResult Create()
        {
            ViewBag.C_id = new SelectList(db.magazines, "C_id", "name");
            ViewBag.volume_id = new SelectList(db.volumes, "volume_id", "volume_id");
            return View();
        }




        // POST: articles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "article_id,page,title,volume_id,year,C_id")] article article)
        {
            int a = article.C_id;
            int b= article.article_id;

            var author_article = db.articles.Where(m => m.article_id == b).FirstOrDefault();
            if (author_article != null)
            {
                ViewBag.ErrorMessage = "Article already exists !!";
                var articles = db.articles.Include(m => m.magazine).Include(m => m.volume);
                return View("Index",await articles.ToListAsync());
            }
            else if (ModelState.IsValid)
            {
                db.articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.C_id = new SelectList(db.magazines, "C_id", "name", article.C_id);
            ViewBag.volume_id = new SelectList(db.volumes, "volume_id", "year", article.volume_id);
            return View(article);
        }

        // GET: articles1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = await db.articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_id = new SelectList(db.magazines, "C_id", "name", article.C_id);
            ViewBag.volume_id = new SelectList(db.volumes, "volume_id", "year", article.volume_id);
            return View(article);
        }

        // POST: articles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "article_id,page,title,volume_id,year,C_id")] article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.C_id = new SelectList(db.magazines, "C_id", "name", article.C_id);
            ViewBag.volume_id = new SelectList(db.volumes, "volume_id", "year", article.volume_id);
            return View(article);
        }

        // GET: articles1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = await db.articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: articles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            article article = await db.articles.FindAsync(id);
            db.articles.Remove(article);
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
