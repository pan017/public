using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication29.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        // Другой профиль
        public ActionResult Index(int id) 
        {
            UserData model = new UserData();
            List<UserData> TempList = new List<UserData>();
            UsersContext db = new UsersContext();
            TempList = db.UsersData.ToList();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (id == TempList[i].UserProfile.UserId)
                {
                    model = TempList[i];
                    break;
                }
            }
            UserData currentUser = new UserData();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (WebSecurity.CurrentUserId == TempList[i].UserProfile.UserId)
                {
                    currentUser = TempList[i];
                    break;
                }
            }
            ViewBag.CurrentUser = currentUser;
            return View(model);
        }

    }
}
