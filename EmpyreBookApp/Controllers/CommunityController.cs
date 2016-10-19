using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmpyreBookApp.Models;
using EmpyreBookApp.DAL;


/* Picture Urk for Community
 
 //https://lh6.googleusercontent.com/-XRbJXaG2Tpc/U18Y_cCxMlI/AAAAAAAAABg/Th28GavqqOA/w612-h473-no/OSU.PNG
 //https://lh6.googleusercontent.com/-XV4pI1s8QcQ/U18ZBPfcw2I/AAAAAAAAABo/V8XT-jWAwyA/w788-h277-no/WOU.PNG
 //https://lh4.googleusercontent.com/-9jVL12weVMs/U1_5JrAKaOI/AAAAAAAAACY/Trnu-czMOvQ/w409-h123-no/ChePNG.PNG
 */


namespace EmpyreBookApp.Controllers
{
    public class CommunityController : Controller
    {
        private RequestContext db = new RequestContext();

        // GET: /Community/
        public ActionResult Index()
        {
            return View(db.Communities.ToList());
        }

        // GET: /Community/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // GET: /Community/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Community/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CommunityID,CName,CShortName,CUrl,BackgroundColor,SecondaryBackgroundColor,Picture")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Communities.Add(community);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(community);
        }

        // GET: /Community/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: /Community/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CommunityID,CName,CShortName,CUrl,BackgroundColor,SecondaryBackgroundColor,Picture")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(community);
        }

        // GET: /Community/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: /Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Community community = db.Communities.Find(id);
            db.Communities.Remove(community);
            db.SaveChanges();
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

        // GET: /Community/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Community/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id, [Bind(Include = "GenreID,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genre.CommunityID = db.Communities.Find(id).CommunityID;
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }
    }
}
