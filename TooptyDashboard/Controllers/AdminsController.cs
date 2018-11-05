using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TooptyDashboard.Models;

namespace TooptyDashboard.Controllers
{
    public class AdminsController : Controller
    {
        private TooptyDBEntities db = new TooptyDBEntities();

        // GET: Admins1
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            return View(db.Admin.ToList());
        }

        // GET: Admins1/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins1/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            return View();
        }

        // POST: Admins1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAdmin,Name,Password,E_mail,ConfirmPassword")] Admin admin)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAdmin,Name,Password,E_mail,ConfirmPassword")] Admin admin)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
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
