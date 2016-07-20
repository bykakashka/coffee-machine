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
        private DataBaseContext db = new DataBaseContext();
        public ActionResult Index ()
        {
            string name = (string) Session ["name"];
            if (name == null)
                return View(new UserAndTodayCost ());

            var p = new CostHistory ();
            p.EndDate = null;
            return View(new UserAndTodayCost{ 
                 User = db.Users.Where(c => c.Login == name).FirstOrDefault(),
                 Cost = db.History.Where(c => ( (c.EndDate == null) || (c.BeginDate <= DateTime.Now && c.EndDate > DateTime.Now) ) ).FirstOrDefault().Cost                   
            });
        }
    }
}