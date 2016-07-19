using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class AdminController : Controller
    {
        private DataBaseContext db = new DataBaseContext ();
        private User admin = new User();

        protected override IActionInvoker CreateActionInvoker ()
        {
            string name = (string) Session ["name"];
            if (name != null)
                admin = db.Users.Where (c => c.Login == name).FirstOrDefault ();
            if (!admin.IsRoot) {
                admin = null;
            }
            return base.CreateActionInvoker ();
        }

        public ActionResult Users()
        {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            AdminAndUsers adminAndUsers = new AdminAndUsers();
            adminAndUsers.User = admin;
            adminAndUsers.Users = db.Users.OrderBy (c => c.Login).ToList ();

            return View(adminAndUsers);
        }

        public ActionResult Change (int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            AdminAndChangedUser adminAndChangedUser = new AdminAndChangedUser ();
            adminAndChangedUser.User = admin;
            adminAndChangedUser.ChangedUser = db.Users.Where (c => c.Id == id).FirstOrDefault ();

            return View (adminAndChangedUser);
        }

        public ActionResult ChangeBalance (string newBalance, int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            decimal balance;
            if (decimal.TryParse (newBalance, out balance)) {
                balance = decimal.Parse (newBalance);
            } else {
                return RedirectToAction ("Users");
            }

            User user = db.Users.Where (c => c.Id == id).FirstOrDefault ();
            if (user != null)
                user.Balance = balance;
            db.SaveChanges ();

            return RedirectToAction ("Users");
        }

        public ActionResult AddCoffeeCost(string cost) {
            decimal newCost;

            if (decimal.TryParse (cost, out newCost)) newCost = decimal.Parse (cost);
            else return RedirectToAction ("CoffeeCost");
            
            Console.Write (newCost);

            var lastHistory = db.History.Where (c => ((c.EndDate == null) || (c.EndDate > DateTime.Now) )).FirstOrDefault ();

            if (lastHistory == null) {
                Console.Write ("null");
                lastHistory = new CostHistory ();
                //return RedirectToAction ("CoffeeCost");
            }

            var timeNow = DateTime.Now;
            lastHistory.EndDate = timeNow;
            var newCoffeeHistory = new CostHistory {Cost = newCost, BeginDate = timeNow, EndDate = null};
            db.History.Add (newCoffeeHistory);
            db.SaveChanges ();

            return RedirectToAction("CoffeeCost");
        }

        public ActionResult CoffeeCost() {
            var costHistory = db.History.OrderByDescending (c => c.BeginDate).ToList ();

            return View (new AdminAndCostHistory { User = admin, CostHistory = costHistory});
        }

        public ActionResult UserHistory(int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            UserAndPurchases userAndPerchases = new UserAndPurchases {
                User = db.Users.Where (c => c.Id == id).FirstOrDefault (),
                Purchases = db.Purchases.Where(c => c.User_id == id).OrderByDescending(c => c.Date).ToList()
            };

            return View (new AdminAndUserHistory{User = admin, UserAndPurchases = userAndPerchases});
        }
    }
}
