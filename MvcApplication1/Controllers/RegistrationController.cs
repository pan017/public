using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Registraton()
        {
            User user = new User();

            return View();
        }
    }
}
