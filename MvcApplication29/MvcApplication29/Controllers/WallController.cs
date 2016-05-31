using MvcApplication29.Filters;
using MvcApplication29.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication29.Controllers
{
    [InitializeSimpleMembership]
    public class WallController : Controller
    {
        //
        // GET: /Wall/
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
        public ActionResult ViewWallUser(int UserId)
        {
            UsersContext db = new UsersContext();
            GetCurrentUser();
            List<Wall> TempList = new List<Wall>();
            List<Wall> CurrentUserWall = new List<Wall>();

            TempList = db.Walls.ToList();
            for (int i = TempList.Count-1; i > -1 ; i--)
            {
                if (TempList[i].ThisUser.UserId == UserId)
                {
                    CurrentUserWall.Add(TempList[i]);
                }
            }
            return View(CurrentUserWall);
        }
        public ActionResult Index()
        {
            UsersContext db = new UsersContext();
            List<Wall> TempList = new List<Wall>();
            List<Wall> CurrentUserWall = new List<Wall>();
            
            TempList = db.Walls.ToList();
            for (int i = 0; i < TempList.Count; i++)
            {
                if (TempList[i].ThisUser.UserId == WebSecurity.CurrentUserId)
                {
                    CurrentUserWall.Add(TempList[i]);
                }
            }
            return PartialView(CurrentUserWall);
        }
        public ActionResult AddWall()
        {
            return View();
        }
        public ActionResult DelWall(int wallId)
        {
            UsersContext db = new UsersContext();
            Wall firstOrder = db.Walls
        .Where(o => o.Id == wallId)
        .FirstOrDefault();
            if (firstOrder != null)
                db.Walls.Remove(firstOrder);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddWall( Wall model,  HttpPostedFileBase upload)
        {
            UsersContext db = new UsersContext();
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                string Extention = System.IO.Path.GetExtension(upload.FileName);
                string NewFileName = Crypto.HashPassword(fileName);
                NewFileName = NewFileName.Remove(0, 20);
                NewFileName += Extention;
                NewFileName = NewFileName.Replace('/', 'w');
                NewFileName = NewFileName.Replace('\\', 'a');
                NewFileName = NewFileName.Replace('+', 'q');
                upload.SaveAs(Server.MapPath("~/WallsContent/" + NewFileName));
                model.ContentUrl = "/WallsContent/" + NewFileName;
            }
            model.ThisUser = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            model.CreationDate = DateTime.Now;
            model.PostUser = db.UserProfiles.Find(WebSecurity.CurrentUserId);

            db.Walls.Add(model);
            
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
