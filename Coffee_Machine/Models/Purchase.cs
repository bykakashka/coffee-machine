using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    [Table("history", Schema="public")]
    public class Purchase
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int User_id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("enable")]
        public bool Enable { get; set; }

        public Purchase ()
        {
        }
    }
}

