﻿using System;
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
            return View ();
        }

        public ActionResult Login (string login) {
            Session ["name"] = login;
            return View ();
        }
    }
}