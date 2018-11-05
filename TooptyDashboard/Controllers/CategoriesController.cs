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
    public class CategoriesController : Controller
    {
        private TooptyDBEntities db = new TooptyDBEntities();

        // GET: Categories
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            return View(db.Categorie.ToList());
        }

        // GET: Categories/Details/5
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
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategorie,NomCategorie")] Categorie categorie)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Categorie.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorie);
        }

        // GET: Categories/Edit/5
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
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategorie,NomCategorie")] Categorie categorie)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        // GET: Categories/Delete/5
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
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            Categorie categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);
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
