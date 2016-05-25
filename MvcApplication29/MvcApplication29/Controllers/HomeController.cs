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
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {

        UsersContext db;
        
        public void GetCurrentUser()
        {
            db = new UsersContext();
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
            ViewBag.currentUser = model;

        }
        [Authorize]
        
        public ActionResult Index()
        {

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            GetCurrentUser();
            var EditUser = db.EmailModels
            .Where(c => c.UserProfile.UserId == WebSecurity.CurrentUserId)
            .FirstOrDefault();

            ViewBag.EmailError = false;
            //if (!EditUser.IsConfirm)
            // ViewBag.EmailError = true;
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
            ViewBag.currentUser = model;
            
            return View(model);
        }

        [HttpPost]

        public ActionResult AvatarChanget()
        {
            GetCurrentUser();
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
        public ActionResult FindUser()
        {
            ViewBag.Message = "Your contact page.";
            GetCurrentUser();
            List<UserData> model = new List<UserData>();
            model.Add(new UserData());
            return View(model);
        }
         [HttpPost]
        public ActionResult FindUser(List<UserData> model)
        {
            GetCurrentUser();
            List<UserData> _userList = db.UsersData.ToList();
            string find = model[0].Name.Trim().ToLower();
            List<UserData> _model = new List<UserData>();
            for (int i = 0; i < _userList.Count; i++)
            {
                if (_userList[i].Name.Trim().ToLower() == find || _userList[i].LastName.Trim().ToLower() == find ||
                    _userList[i].UserProfile.UserName.Trim().ToLower() == find)
                    model.Add(_userList[i]);
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
