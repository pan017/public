using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication29.Controllers
{
    public class FrendsController : Controller
    {
        //
        // GET: /Frends/

        public ActionResult Frends()
        {
            List<FrendsModel> TempList = new List<FrendsModel>();
            List<FrendsModel> FrendsList = new List<FrendsModel>();
            UsersContext db = new UsersContext();
            TempList = db.Frends.ToList();
            for (int i = 0; i < TempList.Count-1; i++)
            {
                if (TempList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId
                    || TempList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId)
                    FrendsList.Add(TempList[i]);
            }
            return View(FrendsList);
        }

    }
}
