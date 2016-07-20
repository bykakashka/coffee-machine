using System;
using System.Collections.Generic;

namespace Coffee_Machine
{
    public class AdminAndUsers
    {
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
        public AdminAndUsers ()
        {
        }
    }
}

