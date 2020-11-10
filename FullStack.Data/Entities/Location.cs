using System;
using System.Collections.Generic;

namespace FullStack.Data.Entities
{
    public class Location
    {
        public Location()
        {
            Cities = new List<City>(); 
        }

        public int Id { get; set; }
        public string Province { get; set; }
        public List<City> Cities { get; set; }
    }
}
