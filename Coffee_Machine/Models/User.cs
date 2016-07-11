using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    [Table("users", Schema="public")]
    public class User
    {
        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("id")]
        public int Id { get; set; }

        [Column("balance")]
        public decimal Balance { get; set; }

        public User ()
        {
        }
    }
}

