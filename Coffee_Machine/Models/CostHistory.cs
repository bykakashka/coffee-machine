using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    [Table("coffeecost", Schema="public")]
    public class CostHistory
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("begindate")]
        public DateTime BeginDate { get; set; }

        [Column("enddate")]
        public DateTime? EndDate { get; set; }
            
        public CostHistory ()
        {
        }
    }
}

