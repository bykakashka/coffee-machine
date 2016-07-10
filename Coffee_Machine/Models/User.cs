using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Machine
{
    [Table("Users", Schema="public")]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public User ()
        {
        }
    }
}

