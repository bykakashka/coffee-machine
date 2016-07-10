using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Coffee_Machine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index ()
        {
            UserContext db = new UserContext ();
            List<User> users = db.Users.ToList ();

            return View (users);
        }
    }
}

