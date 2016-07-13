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
            string name = (string) Session ["name"];
            if (name == null)
                return View(new UserAndPurchases ());
            else
                return View(new UserAndPurchases{ User = new UserContext().Users.Where(c => c.Login == name).FirstOrDefault() });
        }
    }
}