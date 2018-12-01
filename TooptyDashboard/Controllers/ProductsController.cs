using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using TooptyDashboard.Models;
using System.IO;

namespace TooptyDashboard.Controllers
{
    public class ProductsController : Controller
    {
        private TooptyDBEntities db = new TooptyDBEntities();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,Product product)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }

            ViewBag.CatagorieId = new SelectList(db.Categories, "IdCategorie", "NomCategorie", product.CatagorieId);
            ViewBag.IdMarque = new SelectList(db.Marques, "IdMarque", "Marque1", product.IdMarque);


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products
                        select i;
           
            if (!String.IsNullOrEmpty(searchString))
            {
                    //|| s.Catagorie.NomCategorie.ToUpper().Contains(searchString.ToUpper())
                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                   );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));


            //var items = db.Items.Include(i => i.Catagorie);
            //return View(await items.ToListAsync());
           
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {             
            ViewBag.CatagorieId = new SelectList(db.Categories, "IdCategorie", "NomCategorie");
            ViewBag.IdMarque = new SelectList(db.Marques, "IdMarque", "Marque1");
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            Categorie categorie = db.Categories.Find(product.CatagorieId);
            Marque marque = db.Marques.Find(product.IdMarque);

            HttpPostedFileBase file = Request.Files["ItemPictureUrl"];
            if (ModelState.IsValid)
            {
               
                try
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(file.FileName));
                        string pathclient = Path.Combine(Server.MapPath("D:/3eme ing/dotNet/TooptyDashboard/TooptyClient/Images/"), Path.GetFileName(file.FileName));
                        file.SaveAs(pathclient);

                        product.ItemPictureUrl = "~/Images/" + Path.GetFileName(file.FileName);
                        product.Catagorie = categorie;
                        product.Marque = marque;
                        file.SaveAs(path);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";

                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }
               
            }
            ViewBag.CatagorieId = new SelectList(db.Categories, "IdCategorie", "NomCategorie", product.CatagorieId);
            ViewBag.IdMarque = new SelectList(db.Marques, "IdMarque", "Marque1", product.IdMarque);

            return View(product);
        }

      
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMarque = new SelectList(db.Marques, "IdMarque", "Marque1", product.IdMarque);
            ViewBag.CatagorieId = new SelectList(db.Categories, "IdCategorie", "NomCategorie", product.CatagorieId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduct,Matricule,Nom,Price,Qte,IdMarque,IdCategorie")] Product product)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extention = Path.GetExtension(product.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                if (fileName != null)
                {
                    product.ItemPictureUrl = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    product.ImageFile.SaveAs(fileName);
                }
               

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMarque = new SelectList(db.Marques, "IdMarque", "Marque1", product.IdMarque);
            ViewBag.CatagorieId = new SelectList(db.Categories, "IdCategorie", "NomCategorie", product.CatagorieId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Product item = await db.Products.FindAsync(id);

            byte[] photoBack = item.InternalImage;

            return File(photoBack, "image/png");
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
