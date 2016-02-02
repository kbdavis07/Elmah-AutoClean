using AutoClean;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elmah_AutoClean.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //Exception ex = new Exception("Application has started");

            //ErrorLog.LogError(ex);

            //ErrorSignal.FromCurrentContext().Raise(ex);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            CleanUp.AutoClean();

            return View();
        }
    }
}