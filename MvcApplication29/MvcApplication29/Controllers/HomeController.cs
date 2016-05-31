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
  
        public HomeController()
        {
           
        }
        public void GetCurrentUser()
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
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

        public static int GetNotReadMessagesCount()
        {

            UsersContext db = new UsersContext();
            List<Message> TempList = new List<Message>();
            TempList = db.Messages.ToList();
            List<Message> CurrentUserMessages = new List<Message>();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserGet.UserId == WebSecurity.CurrentUserId)
                    CurrentUserMessages.Add(TempList[i]);
            }
            List<UserData> TempUserList = new List<UserData>();
            TempUserList = db.UsersData.ToList();
            UserData currentUser = new UserData();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (WebSecurity.CurrentUserId == TempUserList[i].UserProfile.UserId)
                {
                    currentUser = TempUserList[i];
                    break;
                }
            }


            List<Message> DialogList = new List<Message>();
            // Получаем id пользователей с которыми у нас были диалоги
            List<int> MessageUsersFirstList = new List<int>();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserGet.UserId == WebSecurity.CurrentUserId)
                    MessageUsersFirstList.Add(TempList[i].UserPost.UserId);
                if (TempList[i].UserPost.UserId == WebSecurity.CurrentUserId)
                    MessageUsersFirstList.Add(TempList[i].UserGet.UserId);
            }
            // удаляем повторяющиеся
            List<int> MessageUsersSecondList = new List<int>(MessageUsersFirstList.Distinct());
            // Находим последниии сообщения диалогов
            // формируем список диалогов
            for (int i = 0; i < MessageUsersSecondList.Count; i++)
            {
                for (int j = TempList.Count - 1; j > 0; j--)
                {
                    if ((MessageUsersSecondList[i] == TempList[j].UserGet.UserId && TempList[j].UserPost.UserId == WebSecurity.CurrentUserId)
                        || (MessageUsersSecondList[i] == TempList[j].UserPost.UserId && TempList[j].UserGet.UserId == WebSecurity.CurrentUserId))
                    {
                        DialogList.Add(TempList[j]);
                        break;
                    }

                }
            }

            // ViewBag.DialogList = DialogList;
            List<MessageModel> NewDialogList = new List<MessageModel>();
            for (int i = 0; i < DialogList.Count; i++)
            {
                UserData UserInfo = new UserData();
                MessageModel tempMessageModel = new MessageModel();
                tempMessageModel.Message = DialogList[i];
                if (DialogList[i].UserGet.UserId == WebSecurity.CurrentUserId)
                {
                    int FindUserId = (int)DialogList[i].UserPost.UserId;
                    UserInfo = db.UsersData
                        .Where(c => c.UserProfile.UserId == FindUserId)
                        .FirstOrDefault();
                    tempMessageModel.UserData = UserInfo;
                }
                if (DialogList[i].UserPost.UserId == WebSecurity.CurrentUserId)
                {
                    int FindUserId = (int)DialogList[i].UserGet.UserId;
                    UserInfo = db.UsersData
                        .Where(c => c.UserProfile.UserId == FindUserId)
                        .FirstOrDefault();
                    tempMessageModel.UserData = UserInfo;
                }
                NewDialogList.Add(tempMessageModel);
            }
            int NotReadMessagesCount = 0;
            for (int i = 0; i < NewDialogList.Count; i++)
            {
                if (NewDialogList[i].Message.IsRead == false && NewDialogList[i].Message.UserGet.UserId == WebSecurity.CurrentUserId)
                    NotReadMessagesCount++; 
                }
            return NotReadMessagesCount;

        }
        public ActionResult FindUser(string Model)
        {

            List<UserData> model = new List<UserData>();
           
                 ViewBag.Message = "Your contact page.";
                 GetCurrentUser();
                 
                 model.Add(new UserData());
                 if (Model == null)
                 {
                 return View(model);
             }
            GetCurrentUser();
            List<UserData> _userList = db.UsersData.ToList();
 

            string find = Model;
            if (find == null)
                return View(model);
            find = Model.Trim().ToLower();

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
