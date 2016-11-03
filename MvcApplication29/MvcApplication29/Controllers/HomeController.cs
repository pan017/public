using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication29.Models;
using MvcApplication29.Filters;
using WebMatrix.WebData;
using System.IO;
using System.Web.Helpers;

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
            //WebSecurity.Logout();
            //return RedirectToAction("Login", "Account");
            //WebSecurity.Logout();
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
            //string model = "qwe";

            //string s;
            //List<string> Names = new List<string>();
            //StreamReader f = new StreamReader(@"E:\public\имя.txt");

            //Random r = new Random();
            //double sex = r.Next(10);

            //while ((s = f.ReadLine()) != null)
            //{
            //    Names.Add(s);
            //}
            //List<string> Mans = new List<string>();
            //f = new StreamReader(@"E:\public\м.txt");
            //while ((s = f.ReadLine()) != null)
            //{
            //    Mans.Add(s);
            //}
            //List<string> Womans = new List<string>();
            //f = new StreamReader(@"E:\public\ж.txt");
            //while ((s = f.ReadLine()) != null)
            //{
            //    Womans.Add(s);
            //}
            //List<string> Logins = new List<string>();
            //f = new StreamReader(@"E:\public\logins.txt");
            //while ((s = f.ReadLine()) != null)
            //{
            //    Logins.Add(s);
            //}
            //List<string> Jobs = new List<string>();
            //f = new StreamReader(@"E:\public\место работы.txt");
            //while ((s = f.ReadLine()) != null)
            //{
            //    Logins.Add(s);
            //}
            //List<string> Intst = new List<string>();
            //f = new StreamReader(@"E:\public\универы.txt");
            //while ((s = f.ReadLine()) != null)
            //{
            //    Logins.Add(s);
            //}
            //List<string> passwords = new List<string>();

            //    for (int j = 0; j < Logins.Count; j++)
            //    {
            //        if (WebSecurity.Login(TempList[55].UserProfile.UserName, Logins[j], false))
            //        {
            //            passwords.Add(Logins[j]);
            //        }
            //    }
            //StreamWriter t = new StreamWriter(@"C:\1.txt");
            //for (int k = 0; k < passwords.Count; k++)
            //{
            //    t.WriteLine(passwords[k]);
            //}
            //for (int i = 1; i < 34; i++)
            //{
            //    string Login = Logins[r.Next(Logins.Count)];
            //    string Password = Logins[r.Next(Logins.Count)];
            //    WebSecurity.CreateUserAndAccount(Login, Password);
            //    bool logged = WebSecurity.Login(Login, Password);

            //    if (logged)
            //    {
            //        //set auth cookie
            //        // FormsAuthentication.SetAuthCookie(model.UserName, false);
            //    }

            //    UserProfile TempProfile = db.UserProfiles.Find(WebSecurity.GetUserId(Login));
            //    UserData CurrentUserDataModel = new UserData(TempProfile);
            //    CurrentUserDataModel.City = "Минск";
            //    //if (sex % 2 == 0)
            //    // {
            //    CurrentUserDataModel.LastName = Names[r.Next(Names.Count)]+"ова";
            //    CurrentUserDataModel.Name = Womans[r.Next(Womans.Count)];
            //    // }
            //    //else
            //    //{
            //    //    CurrentUserDataModel.LastName = Names[r.Next(Names.Count)]+"ова";
            //    //    CurrentUserDataModel.Name = Womans[r.Next(Womans.Count)];
            //    //}
            //    CurrentUserDataModel.School = "СШ №" + r.Next(1, 228);
            //    DateTime q = new DateTime(r.Next(1976, 1998), r.Next(1, 12), r.Next(1, 27));
            //    CurrentUserDataModel.BrithDay = q;
            //    CurrentUserDataModel.Phone = "+37529" + r.Next(1000010, 9999999);

            //    // получаем имя файла
            //    string fileName = (@"E:\public\ж\" + i + "ж.jpg");
            //    // сохраняем файл в папку Files в проекте
            //    string Extention = System.IO.Path.GetExtension(fileName);
            //    string NewFileName = Crypto.Hash(fileName);
            //    NewFileName = NewFileName.Remove(0, 20);
            //    NewFileName = NewFileName.ToLower();
            //    NewFileName += Extention;
            //    try
            //    {
            //        System.IO.File.Copy(fileName, @"E:\public\MvcApplication29\MvcApplication29\Avatars\" + NewFileName);
            //    }
            //    catch
            //    {

            //        NewFileName = Crypto.Hash(fileName + DateTime.Now.ToLongDateString());
            //        NewFileName = NewFileName.Remove(0, 20);
            //        NewFileName = NewFileName.ToLower();
            //        NewFileName += Extention;
            //        System.IO.File.Copy(fileName, @"E:\public\MvcApplication29\MvcApplication29\Avatars\" + NewFileName);

            //    }
            //    //upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));


            //    //System.IO.File.Delete(Server.MapPath(EditUser.AvatarUrl));
            //    CurrentUserDataModel.AvatarUrl = "/Avatars/" + NewFileName;

            //    //upload.SaveAs(Server.MapPath("~/Avatars/" + NewFileName));


            //    CurrentUserDataModel.Skype = Logins[r.Next(Logins.Count)];
            //    db.UsersData.Add(CurrentUserDataModel);
            //    db.SaveChanges();
            //    EmailModel e = new EmailModel();
            //    e.Email = Logins[r.Next(Logins.Count)] + "@mail.ru";
            //    e.IsConfirm = false;
            //    e.Key = Crypto.SHA256(e.Email);
            //    e.PasswordRecoverKey = Crypto.SHA256(e.Email + Password + Login);
            //    e.UserProfile = TempProfile;
            //    db.EmailModels.Add(e);
            //    db.SaveChanges();

            // }

            //     //Man

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
                int q = WebSecurity.CurrentUserId;
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
                int q = WebSecurity.CurrentUserId;
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
            List<UserData> _userList = db.UsersData.ToList();
            model.Add(new UserData());
            if (Model == null)
            {

                Random r = new Random();
                List<int> numbers = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int q = r.Next(1, _userList.Count);
                    for (int j = 0; j < numbers.Count; j++)
                    {
                        if (numbers[j] == q)
                            i--;
                    }
                    numbers.Add(q);
                }
                List<int> newNumber = new List<int>(numbers.Distinct());
                for (int i = 0; i < newNumber.Count; i++)
                {
                    model.Add(_userList[newNumber[i]]);
                }
                return View(model);
            }
            GetCurrentUser();



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
