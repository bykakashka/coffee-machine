using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext () : base("CoffeeMachine")
        {
        }
    }
}

