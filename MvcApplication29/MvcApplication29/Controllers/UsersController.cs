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
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        // Другой профиль
        public ActionResult Index(int id) 
        {
            if (id == WebSecurity.CurrentUserId)
            {
                return RedirectToAction("Index", "Home");
            }
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

            /*
             * Проверяем на "Друзей"
             * Если находим какоето совпадение в БД проверяем был ли запрос подтвержден или нет
             */
            List<FrendsModel> FrendsList = new List<FrendsModel>();
            FrendsList = db.Frends.ToList();
            for (int i = 0; i < FrendsList.Count; i++)
            {
                if ((FrendsList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId && FrendsList[i].UserB.UserProfile.UserId == id)
                    || (FrendsList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId && FrendsList[i].UserA.UserProfile.UserId == id))
                {
                    if (FrendsList[i].IsConfirm)
                    {
                        ViewBag.FrendStatus = model.Name + " у вас в друзьях";
                        break;
                    }
                    else
                    {
                        ViewBag.FrendStatus = "Запрос в друзья отправлен";
                        break;
                    }
                }
                else 
                    ViewBag.FrendStatus = "<button type=\"button\" class=\"btn btn-default\" onclick=\"location.href='/Frends/AddFrends/"+ id+"'\">Добавить в друзья <div class=\"glyphicon glyphicon-user\"></div></button>";
            }

            ViewBag.CurrentUser = currentUser;
            return View(model);
        }

    }
}
