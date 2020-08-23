using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeesManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Title = "About Page";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Page";

            return View();
        }
    }
}
