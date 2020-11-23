using System;
namespace FullStack.ViewModels
{
    public class UserForCreationModel
    {
        public int Id { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string confirmPass { get; set; }
        public int? ContactNumber { get; set; }
    }
}
