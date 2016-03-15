using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication29.Models;
using MvcApplication29.Filters;

namespace MvcApplication29.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            UsersContext db = new UsersContext();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            ViewBag.Users = TempList;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
