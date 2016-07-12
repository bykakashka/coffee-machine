using System;
using System.Data.Entity;

namespace Coffee_Machine
{
    public class PurchaseContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public PurchaseContext () : base("CoffeeMachine")
        {
        }
    }

}

