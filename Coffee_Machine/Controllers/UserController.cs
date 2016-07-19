using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class UserController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        const int MAXMINUTESTOREMOVE = 5;

        User loggedUser = new User();

        protected override IActionInvoker CreateActionInvoker ()
        {
            string name = (string)Session ["name"];

            loggedUser = db.Users.Where (c => c.Login == name).FirstOrDefault ();   
            return base.CreateActionInvoker ();
        }
            
        public ActionResult History()
        {
            var p = db.Purchases.Where(c => c.User_id == loggedUser.Id).OrderByDescending(c => c.Date).ToList();
            return View (new UserAndPurchases {User = loggedUser, Purchases = p});
            //return View (p);
        }

        public ActionResult AddPurchase() {
            CreatePurchase ();
            decimal cost = -CostOnDate (DateTime.Now);
            RecalculatedBalance (cost);

            return RedirectToAction ("Index", "Home");
        }

        public ActionResult DeletePurchase(int id) {
            Purchase purchase = db.Purchases.Where (c => c.Id == id).FirstOrDefault();

            if ( (purchase != null) && (DateTime.Now.Subtract(purchase.Date).TotalMinutes < MAXMINUTESTOREMOVE) ) {
                purchase.Enable = false;
                RecalculatedBalance(CostOnDate (purchase.Date));
                db.SaveChanges ();
            }

            return RedirectToAction ("History");
        }
            
        private decimal CostOnDate(DateTime date) {
            return db.History
                .Where (c => ((c.BeginDate <= date) && (c.EndDate == null || c.EndDate > date)))
                .FirstOrDefault().Cost;
        }

        private void CreatePurchase() {
            Purchase purchase = new Purchase ();
            purchase.Date = DateTime.Now;
            purchase.User_id = loggedUser.Id;
            purchase.Enable = true;

            db.Purchases.Add (purchase);
            db.SaveChanges ();
        }

        private void RecalculatedBalance(decimal cost) {
            loggedUser.Balance += cost;
            db.SaveChanges ();
        }
    }
}
