using MvcApplication15.Filters;
using MvcApplication15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication15.Controllers
{
     [InitializeSimpleMembership]
    public class HomeController : Controller
    {
         public UserData FindUserByProfilId(int CurrentId)
         {
             ProfileContext db = new ProfileContext();
             db.UserDatas.Create();
             db.Communications.Create();
             List<Communication> TempList = new List<Communication>();
             TempList = db.Communications.ToList();
             Communication q = new Communication();
             for (int i = 0; i < TempList.Count; i++)
             {
                 if (WebSecurity.CurrentUserId == TempList[i].UserProfileId)
                 {
                     q = TempList[i];
                     break;
                 }
             }
             return db.UserDatas.Find(q.UserDataId);
         }
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {   
                //db.UserDatas.Find();
                return View(@"../Account/UserPage", FindUserByProfilId(WebSecurity.CurrentUserId));
            }
            else
            {
                return View(@"../Account/Login");
            }
        }
    }
}
