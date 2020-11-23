using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
        public int? ContactNumber { get; set; }
    }
}
