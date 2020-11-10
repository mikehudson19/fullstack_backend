using System;
using System.Collections.Generic;

namespace FullStack.ViewModels
{
    public class LocationModel
    {
        public string Province { get; set; }
        public List<CityModel> Cities { get; set; }
    }
}
