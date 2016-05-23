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
    [Authorize]
    [InitializeSimpleMembership]
    public class MessageController : Controller
    {
        //
        // GET: /Message/
        public ActionResult PostMessage(int UserGetId)
        {
            Message model = new Message();
            UsersContext db = new UsersContext();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            UserData PostUser = new UserData();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    PostUser = TempList[i];
                    break;
                }
            }
            model.UserGet = db.UserProfiles.Find(UserGetId);
            model.UserPost = PostUser.UserProfile;
            return View(model);
        }
        [HttpPost]
        public ActionResult PostMessage(Message model, int UserGetId)
        {            
            UsersContext db = new UsersContext();
            model.UserGet = db.UserProfiles.Find(UserGetId);
            model.UserPost = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            model.Time = DateTime.Now;
            model.IsRead = false;
            // Обнуляем ID юзера, т.к. чудесным образом вместо модели получателя
            // в контроллер приходит ID получателя, причем он записывается в поле ID модели
            // чудеса да и только
            model.Id = 0;
            int CurrentUserPageId = model.UserGet.UserId;
            db.Messages.Add(model);
            db.SaveChanges();
            Uri MyUrl = Request.UrlReferrer;
            if (MyUrl.LocalPath == "/Message/Dialog")
                return RedirectToAction("Dialog", "Message", new { UserId = UserGetId });
            else
                return RedirectToAction("Index", "Users", new { id = CurrentUserPageId });
            
        }

        //public ActionResult Messages(int id)
        //{

        //    return View();
        //}
        public ActionResult Messages()
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
                for (int j = TempList.Count-1; j > 0; j--)
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
            ViewBag.DialogList = NewDialogList;
            ViewBag.currentUser = currentUser;
            ViewBag.CurrentUserMessages = CurrentUserMessages;
            return View();
        }
        public ActionResult Dialog (int UserId)
        {
            UsersContext db = new UsersContext();
            List<UserData> TempUserList = new List<UserData>();
            TempUserList = db.UsersData.ToList();
            for (int i = 0; i < TempUserList.Count; i++)
            {
                if (TempUserList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    ViewBag.currentUser = TempUserList[i];
                    break;
                }
            }

            List<Message> TempList = new List<Message>();
            TempList = db.Messages.ToList();
            List<Message> DialogList = new List<Message>();
            for (int i = TempList.Count -1 ; i >= 0; i--)
            {
                if ((TempList[i].UserGet.UserId == WebSecurity.CurrentUserId && TempList[i].UserPost.UserId == UserId) ||
                    (TempList[i].UserPost.UserId == WebSecurity.CurrentUserId && TempList[i].UserGet.UserId == UserId))
                    DialogList.Add(TempList[i]);
            }
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
                ViewBag.UserGet = UserId;
                NewDialogList.Add(tempMessageModel);
            }
            return View(NewDialogList);
        }
    }
    
    public class MessageModel
    {
        public UserData UserData { get; set; }
        public Message Message { get; set; }
    }
}
