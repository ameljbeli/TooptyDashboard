using TooptyClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Controllers
{
    public class StoreController : Controller
    {
        TooptyClientDBEntities storeDB = new TooptyClientDBEntities();

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var catagories = storeDB.Categorie.ToList();

            return View(catagories);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string catagorie)
        {
            // Retrieve Genre and its Associated Items from database
            var catagorieModel = storeDB.Categorie.Include("Items")
                .Single(g => g.NomCategorie == catagorie);

            return View(catagorieModel);
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var item = storeDB.Product.Find(id);

            return View(item);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult CatagorieMenu()
        {
            var categorie = storeDB.Categorie.ToList();

            return PartialView(categorie);
        }
    }
}