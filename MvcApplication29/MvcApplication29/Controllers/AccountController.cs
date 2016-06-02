using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MvcApplication29.Filters;
using MvcApplication29.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;

namespace MvcApplication29.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [HttpPost]
        public ActionResult Settings(LocalPasswordModel model)
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
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
            if (String.IsNullOrEmpty(model.ConfirmPassword) || String.IsNullOrEmpty(model.OldPassword)
                || String.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("PasswordMessage", "Ошибка! Заполните все поля!");
                return View(model);
            }
                    if(model.ConfirmPassword != model.NewPassword)
                    {
                        ModelState.AddModelError("PasswordMessage", "Ошибка! Пароли не совпадают");
                        return View(model);
                    }
              bool changePasswordSucceeded;
              changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
              if (!changePasswordSucceeded)
              {
                  ModelState.AddModelError("PasswordMessage", "Ошибка! Данные введены не корректно");
                  return View(model);
              }
              ModelState.AddModelError("PasswordMessage", "Пароль успешно изменен!");
            return View();
        }
        public ActionResult Settings()
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
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

            return View();
        }
        [Authorize]
        public ActionResult AddData()
        {
                        ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
            UsersContext db = new UsersContext();
            string a = WebSecurity.CurrentUserName;
            UserData model = new UserData();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            for (int i = 0; i < TempList.Count ; i++)
			{
			    if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {
                    model = TempList[i];
                    ViewBag.currentUser = TempList[i];
                    break;
                }
			}
            db.Dispose();
            
            return View("AddData",model);
        }

        [HttpPost]
        public ActionResult AddData(UserData model, HttpPostedFileBase upload)
        {
            ViewBag.NotRead = HomeController.GetNotReadMessagesCount();
            UsersContext db = new UsersContext();
            List<UserData> TempList = new List<UserData>();
            TempList = db.UsersData.ToList();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                {

                    ViewBag.currentUser = TempList[i];
                    break;
                }
            }
            var EditUser = db.UsersData
               .Where(c => c.UserProfile.UserId == WebSecurity.CurrentUserId)
               .FirstOrDefault();
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                string Extention = System.IO.Path.GetExtension(upload.FileName);
                string NewFileName = Crypto.Hash(fileName);
                NewFileName = NewFileName.Remove(0, 20);
                NewFileName = NewFileName.ToLower();
                NewFileName += Extention;
                upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));

               
                //System.IO.File.Delete(Server.MapPath(EditUser.AvatarUrl));
                EditUser.AvatarUrl = "/Avatars/" + NewFileName;
                db.SaveChanges();
                upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));
            }

            if (model.Name == null)
            {
                ModelState.AddModelError("Name", "Поле Имя обязательно к заполнению");
        
               
                return View("AddData", model);
            }
            if (model.LastName == null)
            {
                ModelState.AddModelError("LastName", "Поле Фамилия обязательно к заполнению");
                        
                return View("AddData", model);
            }
            EditUser.BrithDay = model.BrithDay;
            EditUser.About = model.About;
            EditUser.City = model.City;
            EditUser.College = model.College;
            EditUser.Entertainment = model.Entertainment;
            EditUser.FavoriteBook = model.FavoriteBook;
            EditUser.FavoriteGames = model.FavoriteGames;
            EditUser.FavoriteKino = model.FavoriteKino;
            EditUser.FavoriteMusik = model.FavoriteMusik;
            EditUser.HPhone = model.HPhone;
            EditUser.Instagram = model.Instagram;
            EditUser.Institute = model.Institute;
            EditUser.Interesses = model.Interesses;
            EditUser.Job = model.Job;
            EditUser.LastName = model.LastName;
            EditUser.Name = model.Name;
            EditUser.Phone = model.Phone;
            EditUser.School = model.School;
            EditUser.Sex = model.Sex;
            EditUser.Skype = model.Skype;
            EditUser.Twitter = model.Twitter;
            EditUser.WebSite = model.WebSite;
            ViewBag.currentUser = EditUser;
            if (ModelState.IsValid)
                db.SaveChanges();
            else
            {
                ModelState.AddModelError("BrithDay", "Введите дату корректно");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            UsersContext db = new UsersContext();
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                string Extention = System.IO.Path.GetExtension(upload.FileName);
                string NewFileName = Crypto.Hash(fileName);
                NewFileName = NewFileName.Remove(0,20);
                NewFileName = NewFileName.ToLower();
                NewFileName +=Extention;
                upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));
                
                var EditUser = db.UsersData
                .Where(c => c.UserProfile.UserId == WebSecurity.CurrentUserId)
                .FirstOrDefault();
                //System.IO.File.Delete(Server.MapPath(EditUser.AvatarUrl));
                EditUser.AvatarUrl = "/Avatars/" + NewFileName;
                db.SaveChanges();
                upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));
            }
            return RedirectToAction("Index", "Home");
            
        }

        #region Regiser\Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (model.Password == null || model.UserName == null)
            {
                ModelState.AddModelError("MyError", "Ошибка. Заполните все поля");
                return View(returnUrl);
            }
            bool logged = WebSecurity.Login(model.UserName, model.Password);

            if (logged)
            {
                //set auth cookie
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("MyError", "Ошибка! Имя пользователя или пароль неверны");
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff

        
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

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (model.ConfirmPassword == null || model.EmailAdres == null || model.Password == null || model.UserName == null)
            {
                ModelState.AddModelError("MyError", "Ошибка! Заполните все поля!");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    if (model.EmailAdres.IndexOf('@') < 1)
                    {
                        ModelState.AddModelError("EmailAdres", "E-mail адрес введен не корректно");
                        return View(model);
                    }
                    List<int> qwe = new List<int>();
                    for (int i = 0; i < model.UserName.Length; i++)
                    {
                        qwe.Add((char)model.UserName[i]);
                    }
                    bool LoginValid = false;
                    for (int i = 0; i < model.UserName.Length; i++)
                    {
                        if (((int)model.UserName[i] > 47 && (int)model.UserName[i] < 58) ||
                            ((int)model.UserName[i] > 64 && (int)model.UserName[i] < 91) ||
                            ((int)model.UserName[i] > 96 && (int)model.UserName[i] < 123))
                            LoginValid = true;
                        else
                        {
                            ModelState.AddModelError("UserName", "Имя пользователя должно состоять из следущих символо: A-Z и 0-9");
                            return View(model);
                        }
                    }
                    if (LoginValid)
                    {
                        model.UserName.ToLower();
                        model.EmailAdres.ToLower();
                        if (WebSecurity.UserExists(model.UserName))
                        {
                            ModelState.AddModelError("UserName", "Логин занят");
                            return View(model);
                        }
                        UsersContext db = new UsersContext();
                        List<EmailModel> emailCheck = new List<EmailModel>();
                        emailCheck = db.EmailModels.ToList();
                        for (int i = 0; i < emailCheck.Count; i++)
                        {
                            if (emailCheck[i].Email == model.EmailAdres)
                            {
                                ModelState.AddModelError("UserName", "Пользователь с таким e-mail уже существует");
                                return View(model);
                            }
                        }
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                        bool logged = WebSecurity.Login(model.UserName, model.Password);

                        if (logged)
                        {
                            //set auth cookie
                            FormsAuthentication.SetAuthCookie(model.UserName, false);
                        }
                        
                        UserProfile TempProfile = db.UserProfiles.Find(WebSecurity.GetUserId(model.UserName));
                        UserData CurrentUserDataModel = new UserData(TempProfile);
                        db.UsersData.Add(CurrentUserDataModel);
                        db.SaveChanges();
                        EmailModel e = new EmailModel();
                        e.Email = model.EmailAdres;
                        e.IsConfirm = false;
                        e.Key = Crypto.SHA256(model.EmailAdres);
                        e.PasswordRecoverKey = Crypto.SHA256(model.EmailAdres + model.Password + model.UserName);
                        e.UserProfile = TempProfile;
                        db.EmailModels.Add(e);
                        db.SaveChanges();

                        SendMail("smtp.mail.ru", "pan-i@mail.ru", "7632bxr29zx6", e.Email , "public", "Welcom to public " +
                            "Пожалуйста, активируйте Ваш аккаунт, перейдя по этой ссылке http://localhost:2520/Account/ActivationAccount/" + e.Key, null);
                        ViewBag.currentUser = CurrentUserDataModel;
                        return View("AddData", CurrentUserDataModel);
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            else
            {
                for (int i = 0; i < model.UserName.Length; i++)
                {
                    if ((int)model.UserName[i] < 48 || (int)model.UserName[i] > 57 || (int)model.UserName[i] < 65 || (int)model.UserName[i] > 90 || (int)model.UserName[i] < 97 || (int)model.UserName[i] > 122 )
                        ModelState.AddModelError("UserName", "Имя пользователя должно состоять из следущих символо: A-Z и 0-9");
                }
                if (model.UserName.Length < 4)
                    ModelState.AddModelError("UserName", "Имя пользователя должно быть не менее трех символов");
                if (model.Password != model.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "Оба пароля должны быть идентичны");
                if (model.EmailAdres.IndexOf('@') < 1)
                    ModelState.AddModelError("EmailAdres", "E-mail адрес введен не корректно");
                
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Отправка письма на почтовый ящик C# mail send
        /// </summary>
        /// <param name="smtpServer">Имя SMTP-сервера</param>
        /// <param name="from">Адрес отправителя</param>
        /// <param name="password">пароль к почтовому ящику отправителя</param>
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public static void SendMail(string smtpServer, string from, string password,
string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        public ActionResult ActivationAccount(string ActivatonCode)
        {
            UsersContext db = new UsersContext();
            EmailModel userEmail = new EmailModel();
            List<EmailModel> TempList = new List<EmailModel>();
            TempList = db.EmailModels.ToList();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (ActivatonCode == TempList[i].Key && WebSecurity.CurrentUserId == TempList[i].UserProfile.UserId)
                {
                    userEmail = TempList[i];
                    var EditUser = db.EmailModels
                    .Where(c => c.Id == userEmail.Id)
                    .FirstOrDefault();
                    EditUser.IsConfirm = true;
                    db.SaveChanges();
                    break;
                }
            }
            ViewBag.EmailError = null;
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult RecoverPassword()
        {
            return View();
        }
    [HttpPost]
    [AllowAnonymous]
        public ActionResult RecoverPassword(PasswordRecoverModel Model)
        {
            if (Model.Email == null || Model.Name == null)
            {
                ModelState.AddModelError("MyObject", "Заполните все поля");
                    return View();
            }
            UsersContext db = new UsersContext();
            EmailModel FindUser = new EmailModel();
            List<EmailModel> _tempList = new List<EmailModel>();
            _tempList = db.EmailModels.ToList();
            for (int i = 0; i <_tempList.Count; i++)
			 if (_tempList[i].Email.Trim().ToLower() == Model.Email.Trim().ToLower() &&
                 _tempList[i].UserProfile.UserName.Trim().ToLower() == Model.Name.Trim().ToLower())
             {
                 FindUser = _tempList[i];
                 break;
             }
        if (FindUser.Email == null)
        {
            ModelState.AddModelError("MyObject", "Данные введены не корректно");
            return View();
        }

            string Key = WebSecurity.GeneratePasswordResetToken(FindUser.UserProfile.UserName);
            SendMail("smtp.mail.ru", "pan-i@mail.ru", "7632bxr29zx6", FindUser.Email, "public", "Welcom to public " +
                           "Для восстановления пароля перейдите по этой ссылке http://localhost:2520/Account/RecoverPasswordPage?Key=" + Key, null);
                        
            return View();
        }
        [AllowAnonymous]
        public ActionResult RecoverPasswordPage(string Key)
        {
            LocalPasswordModel model = new LocalPasswordModel();
            model.OldPassword = Key;
            ViewBag.Key = Key;
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult RecoverPasswordPage(LocalPasswordModel model)
        
        {
            if (model.NewPassword == null || model.ConfirmPassword == null)
            {
                ModelState.AddModelError("NewPassword", "Ошибка! Заполните все поля");
                return View(model);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("NewPassword", "Ошибка! Пароли должны совпадать");
                return View(model);
            }
            int s = WebSecurity.GetUserIdFromPasswordResetToken(model.OldPassword);
            string FindUser = "";
            WebSecurity.ResetPassword(model.OldPassword, model.NewPassword);
            UsersContext db = new UsersContext();
            List<UserProfile> _userProfileList = db.UserProfiles.ToList();
            for (int i = 0; i < _userProfileList.Count; i++)
            {
                if (_userProfileList[i].UserId == s)
                {
                    FindUser = _userProfileList[i].UserName;
                    break;
                }
            }
            bool logget = WebSecurity.Login(FindUser, model.NewPassword);
            if (logget)
                return RedirectToAction("Index", "Home");
            else 
                return View();
            
        }
        //
        // POST: /Account/Disassociate

        [HttpPost]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");

            return View();
        }

        //
        // POST: /Account/Manage
        public void GetCurrentUser()
        {
            UsersContext db = new UsersContext();
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

        [HttpPost]
        public ActionResult Manage(LocalPasswordModel model)
        {
            GetCurrentUser();
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        UsersContext db;
                        db = new UsersContext();
                        List<UserData> TempList = new List<UserData>();
                        TempList = db.UsersData.ToList();
                        ViewBag.Users = TempList;
                        UserData _model = new UserData();
                        for (int i = 0; i < TempList.Count; i++)
                        {
                            if (TempList[i].UserProfile.UserId == WebSecurity.CurrentUserId)
                            {
                                _model = TempList[i];
                                break;
                            }
                        }
                        ViewBag.currentUser = _model;
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }
        #endregion
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

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
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
