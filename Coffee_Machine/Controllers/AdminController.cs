using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class AdminController : Controller
    {
        User admin = new User();

        protected override IActionInvoker CreateActionInvoker ()
        {
            string name = (string) Session ["name"];
            if (name != null)
                admin = new UserContext ().Users.Where (c => c.Login == name).FirstOrDefault ();
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
            adminAndUsers.Users = new UserContext ().Users.OrderBy (c => c.Login).ToList ();

            return View(adminAndUsers);
        }

        public ActionResult Change (int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            AdminAndChangedUser adminAndChangedUser = new AdminAndChangedUser ();
            adminAndChangedUser.User = admin;
            adminAndChangedUser.ChangedUser = new UserContext ().Users.Where (c => c.Id == id).FirstOrDefault ();

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
            var db = new UserContext ();

            User user = db.Users.Where (c => c.Id == id).FirstOrDefault ();
            if (user != null)
                user.Balance = balance;
            db.SaveChanges ();

            return RedirectToAction ("Users");
        }

        public ActionResult AddCoffeeCost(string cost) {
            decimal newCost;
            if (decimal.TryParse (cost, out newCost)) {
                newCost = decimal.Parse (cost);
            } else
                return RedirectToAction ("CoffeeCost");
            Console.Write (newCost);
            var coffeHistory = new CostHistoryContext ();
            var lastHistory = coffeHistory.History.Where (c => c.EndDate == Constants.ENDDATE).FirstOrDefault ();

            if (lastHistory == null)
                return RedirectToAction ("CoffeeCost");
            
            lastHistory.EndDate = DateTime.Now;
            var newCoffeeHistory = new CostHistory {Cost = newCost, BeginDate = lastHistory.EndDate, EndDate = Constants.ENDDATE};
            coffeHistory.History.Add (newCoffeeHistory);
            coffeHistory.SaveChanges ();

            return RedirectToAction("CoffeeCost");
        }

        public ActionResult CoffeeCost() {
            var costHistory = new CostHistoryContext ().History.OrderByDescending (c => c.BeginDate).ToList ();

            return View (new AdminAndCostHistory { User = admin, CostHistory = costHistory});
        }

        public ActionResult UserHistory(int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            UserAndPurchases userAndPerchases = new UserAndPurchases {
                User = new UserContext ().Users.Where (c => c.Id == id).FirstOrDefault (),
                Purchases = new PurchaseContext().Purchases.Where(c => c.User_id == id).OrderByDescending(c => c.Date).ToList()
            };

            return View (new AdminAndUserHistory{User = admin, UserAndPurchases = userAndPerchases});
        }
    }
}
