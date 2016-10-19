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
using EmpyreBookApp.GoogleREST;

namespace EmpyreBookApp.Controllers
{
    public class RequestController : Controller
    {
        private RequestContext db = new RequestContext();
        private BookSearcher bs = new BookSearcher();

        // GET: /Request/
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.BI).Include(r => r.User);
            return View(requests.ToList());
        }

        // GET: /Request/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: /Request/Create
        public ActionResult Create()
        {
            ViewBag.BIID = new SelectList(db.BIs, "BIID", "ISBN");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName");
            return View();
        }

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RequestID,BIID,UserID,Preferance,ISBN,RequestDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                bs.DbSearch(request.ISBN);
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BIID = new SelectList(db.BIs, "BIID", "ISBN", request.BIID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", request.UserID);
            return View(request);
        }

        // GET: /Request/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.BIID = new SelectList(db.BIs, "BIID", "ISBN", request.BIID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", request.UserID);
            return View(request);
        }

        // POST: /Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RequestID,BIID,UserID,Preferance,ISBN,RequestDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BIID = new SelectList(db.BIs, "BIID", "ISBN", request.BIID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", request.UserID);
            return View(request);
        }

        // GET: /Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: /Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
    }
}
