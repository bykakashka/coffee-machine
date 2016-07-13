using System;

namespace Coffee_Machine
{
    public class UserAndTodayCost
    {
        public User User { get; set; }
        public decimal Cost { get; set; }
        public UserAndTodayCost ()
        {
            User = new User();
        }
    }
}

