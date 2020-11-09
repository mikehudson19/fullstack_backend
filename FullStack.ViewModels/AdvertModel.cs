using System;
namespace FullStack.ViewModels
{
    public class AdvertModel
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string AdvertDetails { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
