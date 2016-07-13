using System;
using System.Collections.Generic;

namespace Coffee_Machine
{
    public class UserAndPurchases
    {
        public User User { get; set; }
        public List<Purchase> Purchases { get; set; }
        public UserAndPurchases ()
        {
            User = new User ();
            Purchases = new List<Purchase> ();
        }
    }
}

