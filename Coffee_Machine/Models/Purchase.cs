using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    [Table("history", Schema="public")]
    public class Purchase
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        public Purchase ()
        {
        }
    }
}

