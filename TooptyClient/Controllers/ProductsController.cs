//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
////using PagedList.Mvc;
////using PagedList;
//using TooptyClient.Models;

//namespace TooptyClient.Controllers
//{
//    public class ProductsController : Controller
//    {
//        private TooptyClientDBEntities db = new TooptyClientDBEntities();

//        // GET: Items
//        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
//        {
//            ViewBag.CurrentSort = sortOrder;
//            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
//            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

//            if (searchString != null)
//            {
//                page = 1;
//            }
//            else
//            {
//                searchString = currentFilter;
//            }

//            ViewBag.CurrentFilter = searchString;

//            var items = from i in db.Product
//                           select i;
//            if (!String.IsNullOrEmpty(searchString))
//            {
//                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
//                                       || s.Catagorie.NomCategorie.ToUpper().Contains(searchString.ToUpper()));
//            }
//            switch (sortOrder)
//            {
//                case "name_desc":
//                    items = items.OrderByDescending(s => s.Name);
//                    break;
//                case "Price":
//                    items = items.OrderBy(s => s.Price);
//                    break;
//                case "price_desc":
//                    items = items.OrderByDescending(s => s.Price);
//                    break;
//                default:  // Name ascending 
//                    items = items.OrderBy(s => s.Name);
//                    break;
//            }

//            int pageSize = 3;
//            int pageNumber = (page ?? 1);
//            //return View( items.ToPagedList(pageNumber, pageSize));


//            //var item = db.Product.Include(i => i.Catagorie);
//            return View(db.Product.ToList());
//        }

//        // GET: Items/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Product item = await db.Product.FindAsync(id);
//            if (item == null)
//            {
//                return HttpNotFound();
//            }
//            return View(item);
//        }

//        // GET: Items/Create
//        [Authorize(Roles = "Admin")]
//        public ActionResult Create()
//        {
//            ViewBag.CatagorieId = new SelectList(db.Categorie, "ID", "Name");
//            return View();
//        }

//        // POST: Items/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<ActionResult> Create(Product item)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Product.Add(item);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CatagorieId = new SelectList(db.Categorie, "ID", "Name", item.CatagorieId);
//            return View(item);
//        }

//        // GET: Items/Edit/5
//         [Authorize(Roles = "Admin")]
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Product item = await db.Product.FindAsync(id);
//            if (item == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CatagorieId = new SelectList(db.Categorie, "ID", "Name", item.CatagorieId);
//            return View(item);
//        }

//        // POST: Items/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<ActionResult> Edit(Product item)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(item).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.CatagorieId = new SelectList(db.Categorie, "ID", "Name", item.CatagorieId);
//            return View(item);
//        }

//        // GET: Items/Delete/5
//         [Authorize(Roles = "Admin")]
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Product item = await db.Product.FindAsync(id);
//            if (item == null)
//            {
//                return HttpNotFound();
//            }
//            return View(item);
//        }

//        // POST: Items/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "Admin")]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Product item = await db.Product.FindAsync(id);
//            db.Product.Remove(item);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        //public async Task<ActionResult> RenderImage(int id)
//        //{
//        //    Product item = await db.Product.FindAsync(id);

//        //    //byte[] photoBack = item.InternalImage;

//        //    return File(photoBack, "image/png");
//        //}

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TooptyClient.Models;
using System.IO;
using PagedList;

namespace TooptyClient.Controllers
{
    public class ProductsController : Controller
    {
        private TooptyClientDBEntities db = new TooptyClientDBEntities();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, Product product)
        {
          

            ViewBag.CatagorieId = new SelectList(db.Categorie, "IdCategorie", "NomCategorie", product.CatagorieId);
            ViewBag.IdMarque = new SelectList(db.Marque, "IdMarque", "Marque1", product.IdMarque);


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

            var items = from i in db.Product
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
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
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
        


        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CatagorieId,Name,Price,ItemPictureUrl,Qte,IdMarque")]Product product)
        {


            if (ModelState.IsValid)
            {



                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extention = Path.GetExtension(product.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                product.ItemPictureUrl = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                product.ImageFile.SaveAs(fileName);

                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
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
                
                
                
                
                
                //string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                //string extention = Path.GetExtension(product.ImageFile.FileName);
                //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                //if (fileName != null)
                //{
                //    product.ItemPictureUrl = "~/Images/" + fileName;
                //    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                //    product.ImageFile.SaveAs(fileName);
                //}
                product.ImageFile.SaveAs("im");

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
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
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
