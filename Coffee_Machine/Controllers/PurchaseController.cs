using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Machine.Controllers
{
    public class PurchaseController : Controller
    {
        const int MAXMINUTESTOREMOVE = 5;

        private PurchaseContext db = new PurchaseContext();

        public ActionResult History()
        {
            string name = (string) Session ["name"];

            var p = (from i in db.Purchases where i.Name == name orderby i.Date descending select i).ToList();
            
            return View (p);
        }

        public ActionResult AddPurchase() {
            Purchase purchase = new Purchase ();

            purchase.Date = DateTime.Now;
            purchase.Name = (string) Session["name"];
            purchase.Enable = true;

            db.Purchases.Add (purchase);
            db.SaveChanges ();

            return RedirectToAction ("Index", "Home");
        }

        public ActionResult DeletePurchase(int id) {
            Purchase purchase = db.Purchases.Where (c => c.Id == id).First();

            if ( (purchase != null) && (DateTime.Now.Subtract(purchase.Date).TotalMinutes < MAXMINUTESTOREMOVE) ) {
                purchase.Enable = false;
                db.SaveChanges ();
            }

            return RedirectToAction ("History", "Purchase");
        }
    }
}
