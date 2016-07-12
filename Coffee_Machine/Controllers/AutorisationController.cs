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
            UserContext db = new UserContext ();
            var user = db.Users.Where (c => c.Login == login).FirstOrDefault();
            if (user == null) {
                User newUser = new User (login);
                db.Users.Add (newUser);
                db.SaveChanges ();
            }
            return RedirectToAction ("Index", "Home");
        }

        public ActionResult Logout() {
            Session ["name"] = null;
            return RedirectToAction ("Index", "Home");
        }
    }
}
