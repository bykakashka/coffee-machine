using System;
using System.Collections.Generic;

namespace Coffee_Machine
{
    public class AdminAndCostHistory
    {
        public User User { get; set; }
        public List<CostHistory> CostHistory { get; set; }
        public AdminAndCostHistory ()
        {
        }
    }
}

