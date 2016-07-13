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

        public ActionResult Index()
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
                return RedirectToAction ("Index");
            }
            var db = new UserContext ();

            User user = db.Users.Where (c => c.Id == id).FirstOrDefault ();
            if (user != null)
                user.Balance = balance;
            db.SaveChanges ();

            return RedirectToAction ("Index");
        }

        public ActionResult UserHistory(int id) {
            if (admin == null)
                return RedirectToAction ("Index", "Home");
            
            User changedUser = new UserContext ().Users.Where (c => c.Id == id).FirstOrDefault ();
            UserAndPurchases userAndPerchases = new UserAndPurchases {
                User = new UserContext ().Users.Where (c => c.Id == id).FirstOrDefault (),
                Purchases = new PurchaseContext().Purchases.Where(c => c.User_id == id).OrderByDescending(c => c.Date).ToList()
            };

            return View (new AdminAndUserHistory{User = admin, UserAndPurchases = userAndPerchases});
        }
    }
}
