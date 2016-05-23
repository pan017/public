using MvcApplication29.Filters;
using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication29.Controllers
{
    [InitializeSimpleMembership]
    public class WallController : Controller
    {
        //
        // GET: /Wall/

        public ActionResult Index()
        {
            //UsersContext db = new UsersContext();
            //List<Wall> TempList = new List<Wall>();
            //List<Wall> CurrentUserWall = new List<Wall>();
            //TempList = db.Walls.ToList();
            //for (int i = 0; i < TempList.Count; i++)
            //{
            //    if (TempList[i].ThisUser.UserId == WebSecurity.CurrentUserId )
            //    {

            //    }
            //}
            return PartialView();
        }

    }
}
