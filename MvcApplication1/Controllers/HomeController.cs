using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        UserContext db = new UserContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            user.Role = 1;
            string userPassword = user.Password;
            user.Password = Crypto.HashPassword(userPassword);
            db.Users.Add(user);
            db.SaveChanges();
            return View("AddData");
        }

        public ActionResult AddData()
        {
            return View();
        }
        public string AddData(Profile profile)
        {

            db.Profiles.Add(profile);
            db.SaveChanges();
            return "asdasd";
        }


    }
}
