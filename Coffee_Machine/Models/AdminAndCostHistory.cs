using System;
using System.Collections.Generic;

namespace Coffee_Machine
{
    public class AdminAndCostHistory
    {
        public User User { get; set; }
        public IEnumerable<CostHistory> CostHistory { get; set; }
        public AdminAndCostHistory ()
        {
        }
    }
}

