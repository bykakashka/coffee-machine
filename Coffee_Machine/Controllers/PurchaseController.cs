using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class PurchaseController : Controller
    {
        private PurchaseContext db = new PurchaseContext();

        public ActionResult History(string name)
        {
            
            var p = (from i in db.Purchases where i.Name == name orderby i.Date select i).ToList();
            
            return View (p);
        }

        public ActionResult AddPurchase(string name) {
            Console.Write ("hi");
            Purchase purchase = new Purchase ();

            purchase.Date = DateTime.UtcNow;
            purchase.Name = name;

            db.Purchases.Add (purchase);
            db.SaveChanges ();

            return RedirectToAction ("Index", "Home");
        }
    }
}
