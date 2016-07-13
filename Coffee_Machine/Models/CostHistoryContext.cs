using System;
using System.Data.Entity;

namespace Coffee_Machine
{
    public class CostHistoryContext : DbContext
    {
        public DbSet<CostHistory> History { get; set; }
        public CostHistoryContext () : base("CoffeeMachine")
        {
        }
    }
}

