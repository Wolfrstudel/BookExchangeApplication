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

namespace EmpyreBookApp.Controllers
{
    public class TradeController : Controller
    {
        private RequestContext db = new RequestContext();

        // GET: /Trade/
        public ActionResult Index()
        {
            var tradets = db.Tradets.Include(t => t.User);
            return View(tradets.ToList());
        }

        // GET: /Trade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Tradets.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }

        // GET: /Trade/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName");
            return View();
        }

        // POST: /Trade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TradeID,SellerID,UserID,Review,Credit")] Trade trade)
        {
            if (ModelState.IsValid)
            {
                db.Tradets.Add(trade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", trade.UserID);
            return View(trade);
        }

        // GET: /Trade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Tradets.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", trade.UserID);
            return View(trade);
        }

        // POST: /Trade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TradeID,SellerID,UserID,Review,Credit")] Trade trade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "LastName", trade.UserID);
            return View(trade);
        }

        // GET: /Trade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trade trade = db.Tradets.Find(id);
            if (trade == null)
            {
                return HttpNotFound();
            }
            return View(trade);
        }

        // POST: /Trade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trade trade = db.Tradets.Find(id);
            db.Tradets.Remove(trade);
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
