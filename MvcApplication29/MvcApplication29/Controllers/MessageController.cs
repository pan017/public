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
        public ActionResult PostMessage()
        {
            Message Message = new Message();
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
            Message.UserPost = PostUser.UserProfile;
            return View(Message);
        }
        [HttpPost]
        public ActionResult PostMessage(Message model)
        {
            if (!WebSecurity.UserExists(model.UserGet.UserName))
            {
                ModelState.AddModelError("", "The user name is incorrect.");
                return View(model);
            }
            
            UsersContext db = new UsersContext();
            UserProfile tempUser = db.UserProfiles.Find(WebSecurity.GetUserId(model.UserGet.UserName));

            model.UserGet = tempUser;
            tempUser = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            model.UserPost = tempUser;
            model.Time = DateTime.Now;
            model.IsRead = false;
            //db.Messages.Create();
            db.Messages.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
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
            ViewBag.CurrentUserMessages = CurrentUserMessages;
            return View();
        }
    }
}
