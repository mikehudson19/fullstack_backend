using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.ViewModels
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
