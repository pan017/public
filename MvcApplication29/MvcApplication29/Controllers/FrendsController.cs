using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MvcApplication29.Filters;
namespace MvcApplication29.Controllers
{
    [InitializeSimpleMembership]
    public class FrendsController : Controller
    {
        //
        // GET: /Frends/

        public ActionResult Frends()
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
            List<FrendsModel> TempList = new List<FrendsModel>();
            List<FrendsModel> FrendsList = new List<FrendsModel>();
            UsersContext db = new UsersContext();
            List<UserData> TempUserList = new List<UserData>();
            List<UserData> ConfirmFrends = new List<UserData>();
            List<UserData> NotConfirmFrends = new List<UserData>();
            int CountOfNotConfirmFrends = 0;

            TempList = db.Frends.ToList();
            // Получаем все заявки в друзья
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId
                    || TempList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId)
                    FrendsList.Add(TempList[i]);
            }
            // Получаем модель текущего пользоваеля

            TempUserList = db.UsersData.ToList();
            for (int i = 0; i < TempUserList.Count; i++)
            {
                if (TempUserList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    ViewBag.currentUser = TempUserList[i];
                    break;
                }
            }
            // Получаем потвержденные и не подтвержденные заявки в друзья
            for (int i = 0; i < FrendsList.Count; i++)
            {
                if (FrendsList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    if (FrendsList[i].IsConfirm)
                        ConfirmFrends.Add(FrendsList[i].UserB);
                    else { }
                }

                else
                {
                    if (FrendsList[i].IsConfirm)
                        ConfirmFrends.Add(FrendsList[i].UserA);
                    else
                        NotConfirmFrends.Add(FrendsList[i].UserA);
                }
            }
            ViewBag.FrendsCount = ConfirmFrends.Count;
            ViewBag.Frends = ConfirmFrends;

            // Считаем количество заявок в друзья, которые отправили нам

            // Если есть входящие заявки - генерируем кнопку
            if (NotConfirmFrends.Count != 0)
            {
                ViewBag.NotConfirm = "<button style=\"margin-left:15px;\" type=\"button\" class=\"btn\" onclick=\"location.href='/Frends/RequestFrends'\">Заявки в друзья(" + NotConfirmFrends.Count + ") </button> <br //>";
            }
            return View(ConfirmFrends);
        }
        public ActionResult AddFrends(int id)
        {
            UsersContext db = new UsersContext();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            FrendsModel frend = new FrendsModel();
            frend.IsConfirm = false;
            frend.Time = DateTime.Now;

            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    frend.UserA = TempList[i];
                    
                }
                if (TempList[i].UserProfile.UserId == id)
                {
                    frend.UserB = TempList[i];
                }
            }
    
            
            db.Frends.Add(frend);
            db.SaveChanges();
            int CurrentId = id;
            
           // Url.Action
            return RedirectToAction("Index", "Users", new { id = CurrentId });
            //return View();
        }
        public ActionResult RequestFrends()
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
            List<FrendsModel> TempList = new List<FrendsModel>();
            List<FrendsModel> FrendsList = new List<FrendsModel>();
            UsersContext db = new UsersContext();
            TempList = db.Frends.ToList();
            for (int i = 0; i < TempList.Count; i++)
            {
                if ((TempList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId && !TempList[i].IsConfirm)
                    || (TempList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId) && !TempList[i].IsConfirm)
                    FrendsList.Add(TempList[i]);
            }
            List<UserData> TempUserList = new List<UserData>();
            TempUserList = db.UsersData.ToList();
            for (int i = 0; i < TempUserList.Count ; i++)
            {
                if (TempUserList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    ViewBag.currentUser = TempUserList[i];
                    break;
                }
            }
            List<RequestFrendsModel> NotConfirmFrends = new List<RequestFrendsModel>();
            for (int i = 0; i < FrendsList.Count; i++)
            {
                if (FrendsList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId)
                    NotConfirmFrends.Add(new RequestFrendsModel(FrendsList[i].UserA, FrendsList[i].Id));
            }
            
            return View(NotConfirmFrends);
        }
        public ActionResult ConfirmRequest(int id)
        {
            UsersContext db = new UsersContext();
            var EditUser = db.Frends
            .Where(c => c.Id == id)
            .FirstOrDefault();
            EditUser.IsConfirm = true;
            db.SaveChanges();
            return RedirectToAction("RequestFrends");
        }
        public ActionResult Renouncement (int id)
        {
            UsersContext db = new UsersContext();
            //IQueryable<FrendsModel> ods = from o in db.Frends
            //                               where o.Id == id
            //                               select o;
            //db.DeleteObject(ods.First());
            FrendsModel RemoveFrendModel = db.Frends.Find(id);
            db.Frends.Remove(RemoveFrendModel);
            db.SaveChanges();
            return RedirectToAction("RequestFrends");
        }
        public ActionResult FindUser (string Model)
        {
            if (String.IsNullOrEmpty(Model))
                return RedirectToAction("Frends");
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
            List<FrendsModel> TempList = new List<FrendsModel>();
            List<FrendsModel> FrendsList = new List<FrendsModel>();
            UsersContext db = new UsersContext();
            List<UserData> TempUserList = new List<UserData>();
            List<UserData> ConfirmFrends = new List<UserData>();
            List<UserData> NotConfirmFrends = new List<UserData>();
          

            TempList = db.Frends.ToList();
            // Получаем все заявки в друзья
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId
                    || TempList[i].UserB.UserProfile.UserId == WebSecurity.CurrentUserId)
                    FrendsList.Add(TempList[i]);
            }
            // Получаем модель текущего пользоваеля

            TempUserList = db.UsersData.ToList();
            for (int i = 0; i < TempUserList.Count; i++)
            {
                if (TempUserList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    ViewBag.currentUser = TempUserList[i];
                    break;
                }
            }
            // Получаем потвержденные и не подтвержденные заявки в друзья
            for (int i = 0; i < FrendsList.Count; i++)
            {
                if (FrendsList[i].UserA.UserProfile.UserId == WebSecurity.CurrentUserId)
                    if (FrendsList[i].IsConfirm)
                        ConfirmFrends.Add(FrendsList[i].UserB);
                    else
                        NotConfirmFrends.Add(FrendsList[i].UserB);
                else
                    if (FrendsList[i].IsConfirm)
                        ConfirmFrends.Add(FrendsList[i].UserA);
                    else
                        NotConfirmFrends.Add(FrendsList[i].UserA);
            }
            
            string Find = Model.Trim().ToLower();
            List<UserData> FindUsersList = new List<UserData>();
            for (int i = 0; i < ConfirmFrends.Count; i++)
            {
                if (ConfirmFrends[i].Name.ToLower() == Find || ConfirmFrends[i].LastName.ToLower() == Find)
                    FindUsersList.Add(ConfirmFrends[i]);
            }
            ViewBag.FrendsCount = ConfirmFrends.Count;
            ViewBag.Frends = FindUsersList;
            return View("Frends", FindUsersList);
        }
    }
}
