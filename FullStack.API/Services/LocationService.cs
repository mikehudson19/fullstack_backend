using System;
using System.Collections.Generic;
using FullStack.Data;
using FullStack.Data.Entities;
using FullStack.ViewModels;

namespace FullStack.API.Services
{

    public interface ILocationService
    {
        IEnumerable<LocationModel> GetLocations();
    }

    public class LocationService : ILocationService
    {
        private readonly IFullStackRepository _repo;

        public LocationService(IFullStackRepository repo)
        {
            this._repo = repo;
        }

        public IEnumerable<LocationModel> GetLocations()
        {
            var locationsToReturn = new List<LocationModel>();

            var locations = this._repo.GetLocations();

            foreach (var location in locations)
            {
                locationsToReturn.Add(MapToModel(location));
            }

            return locationsToReturn;
        }

        public LocationModel MapToModel(Location location)
        {
            var cities = new List<CityModel>();

            foreach (var city in location.Cities)
            {
                cities.Add(new CityModel()
                {
                    CityName = city.CityName
                });
            }

            return new LocationModel
            {
                Province = location.Province,
                Cities = cities
            };
        }
    }
}
