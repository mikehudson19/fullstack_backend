using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.ViewModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserModel user, string token)
        {
            Id = user.Id;
            Forenames = user.Forenames;
            Surname = user.Surname;
            Email = user.Email;
            Token = token;
        }
    }
}
