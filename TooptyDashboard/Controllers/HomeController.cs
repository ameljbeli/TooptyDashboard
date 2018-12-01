﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TooptyDashboard.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["UserID"] ==null)
            {
                return RedirectToAction("SmartLogin", "Account");
            }
            else
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("SmartLogin", "Account");

            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}