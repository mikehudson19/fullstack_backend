using System;
namespace FullStack.Data.Entities
{
    public class Advert
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string AdvertDetails { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
