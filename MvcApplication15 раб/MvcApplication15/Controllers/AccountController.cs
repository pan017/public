
using MvcApplication15.Filters;
using MvcApplication15.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MvcApplication15.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public UserData FindUserByName (string UserName)
        {
            UsersContext UserProfileDB= new UsersContext();
            UserProfile CurrentUser = new UserProfile();
            //CurrentUser = UserProfileDB.UserProfiles.Find(UserName);
            List<UserProfile> UserProfileList = new List<UserProfile>();
            UserProfileList = UserProfileDB.UserProfiles.ToList();
            for (int i = 0; i < UserProfileList.Count-1; i++)
            {
                if (UserProfileList[i].UserName == UserName)
                {
                    CurrentUser = UserProfileList[i];
                    break;
                }
            }
            return FindUserByProfilId(CurrentUser.UserId);
        }
        public UserData FindUserByProfilId(int CurrentId)
        {
            ProfileContext db = new ProfileContext();
            List<Communication> TempList = new List<Communication>();
            TempList = db.Communications.ToList();
            Communication q = new Communication();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (CurrentId == TempList[i].UserProfileId)
                {
                    q = TempList[i];
                    break;
                }
            }
            return db.UserDatas.Find(q.UserDataId);
        }
        //
        // POST: /Account/Login
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe);;
                UserData DataModel = FindUserByName(model.UserName);
                return View("UserPage", DataModel);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        //UsersContext db = new UsersContext();
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    //db.Users.Add(model);

                    WebSecurity.Login(model.UserName, model.Password);
                    int qwe = WebSecurity.CurrentUserId;
                    //.base.b.UsersData.Add(new UserData(WebSecurity.CurrentUserId));
                    // Создаем экземпляр
                    ProfileContext db = new ProfileContext();
                    //ПРисваеваем дату рождения юзера текущую
                    //хз зачем, но без этого прога выебывается дальше чем видит
                    UserData u = new UserData();
                    u.BrithDay = DateTime.Now;

                    //А тут велосипед
                    // Полуучаем ID последней добавленой записи в таблице UserProfile
                    // Т.к. записи разбросаны по базе данных хуй пойми в каком порядке
                    // нужно таблицу отСОРТИРовать по ID-шникам
                    UsersContext dd = new UsersContext();

                    List<UserProfile> temp = new List<UserProfile>(dd.UserProfiles.ToList());
                    Communication conect = new Communication();
                    UserProfile[] q = temp.ToArray();
                    Array.Sort(q, UserProfile.sortDataBaseRecords());
                    conect.UserProfileId = q[q.Length-1].UserId;

                    // Тут все просто. чекаем есть ли записи в таблице, если да,
                    // то берем последнюю и пилем ее ID + 1;
                    List<UserData> ff = new List<UserData>();
                    ff = db.UserDatas.ToList();
                    if (ff.Count < 1)
                        conect.UserDataId = 1;
                    else
                        conect.UserDataId = ff[ff.Count-1].UserId + 1;
                    u.AvatarUrl = @"/Content/images/None-Avatar.jpg";
                    db.UserDatas.Add(u);
                    db.Communications.Add(conect);
                    db.SaveChanges();
                    db.Dispose();
                    return RedirectToAction("AddData", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
 

        public ActionResult AddData ()
        {
            UserData model = FindUserByProfilId(WebSecurity.CurrentUserId);
            ViewBag.mod = model;
            return View();
        }
        [HttpPost]
        public ActionResult AddData(UserData model)
        {


            
            return RedirectToAction("Index", "Home");
        }
        //
        // POST: /Account/Disassociate

      
        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

      
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
