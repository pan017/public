using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication29.Models;
using MvcApplication29.Filters;
using WebMatrix.WebData;

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
            UserData model = new UserData();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    model = TempList[i];
                    break;
                }
            }
            return View(model);
        }

        public ActionResult AvatarChanget()
        {
            UsersContext db = new UsersContext();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            ViewBag.Users = TempList;
            UserData model = new UserData();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    model = TempList[i];
                    break;
                }
            }
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
