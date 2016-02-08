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
        public string Registration(User user)
        {
            user.Role = 1;
            user.Password = Crypto.HashPassword(user.Password);
            db.Users.Add(user);
            db.SaveChanges();
            return "Registration complecte";
        }


    }
}
