using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class AutorisationController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        public ActionResult Login (string login) {
            Session ["name"] = login;
            return RedirectToAction ("Index", "Home");
        }

        public ActionResult Logout() {
            Session ["name"] = null;
            return RedirectToAction ("Index", "Home");
        }
    }
}
