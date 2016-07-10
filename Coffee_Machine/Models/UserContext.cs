using System;
using System.Data.Entity;

namespace Coffee_Machine
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext ()
        {
        }
    }
}

