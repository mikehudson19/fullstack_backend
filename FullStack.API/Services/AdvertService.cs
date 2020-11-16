using System;
using FullStack.Data;
using FullStack.Data.Entities;
using FullStack.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FullStack.API.Services
{
    public interface IAdvertService
    {
        IEnumerable<AdvertModel> GetUserAdverts(int userId);
        IEnumerable<AdvertModel> GetAllAdverts();
        AdvertModel GetById(int id);
        void UpdateAdvert(AdvertForCreationModel advert, int userId);
        AdvertModel CreateAdvert(AdvertForCreationModel advert, int userId);
        Advert MapToAdvertEntity(AdvertForCreationModel advert, int userId, string status);
        AdvertModel MapToModel(Advert advert);
        AdvertForCreationModel MapToCreationModel(AdvertModel advert);
        Advert ShadowDeleteAdvert(AdvertModel advert, int userId);
    }

    public class AdvertService : IAdvertService
    {
        private readonly IFullStackRepository _repo;

        public AdvertService(IFullStackRepository repo)
        {
            this._repo = repo;
        }

        public IEnumerable<AdvertModel> GetUserAdverts(int userId)
        {
            var advertList = _repo.GetUserAdverts(userId);
            return advertList.Select(a => MapToModel(a));
        }

        public IEnumerable<AdvertModel> GetAllAdverts()
        {
            var advertList = _repo.GetAllAdverts();
            return advertList.Select(a => MapToModel(a));
        }

        public AdvertModel GetById(int id)
        {
            var advertEntity = _repo.GetAdvert(id);
            if (advertEntity == null) return null;

            return MapToModel(advertEntity);
        }

        public AdvertModel CreateAdvert(AdvertForCreationModel advert, int userId)
        {
            var mappedAdvert = MapToAdvertEntity(advert, userId, "Live");
            var createdAdvert = _repo.CreateAdvert(mappedAdvert);
            var advertToReturn = MapToModel(createdAdvert);
            return advertToReturn;
        }

        public void UpdateAdvert(AdvertForCreationModel advert, int userId)
        {
            var mappedAdvert = MapToAdvertEntity(advert, userId, advert.Status);
            _repo.UpdateAdvert(mappedAdvert);
        }

        public Advert MapToAdvertEntity(AdvertForCreationModel advert, int userId, string status)
        {
            return new Advert
            {
                Headline = advert.Headline,
                Province = advert.Province,
                City = advert.City,
                AdvertDetails = advert.AdvertDetails,
                Price = advert.Price,
                Status = status,
                UserId = userId,
                Id = advert.Id
            };
        }

        public AdvertModel MapToModel(Advert advert)
        {
            return new AdvertModel
            {
                Id = advert.Id,
                Headline = advert.Headline,
                Province = advert.Province,
                City = advert.City,
                AdvertDetails = advert.AdvertDetails,
                Price = advert.Price,
                Status = advert.Status
            };
        }

        public AdvertForCreationModel MapToCreationModel(AdvertModel advert)
        {
            return new AdvertForCreationModel
            {
                Headline = advert.Headline,
                Province = advert.Province,
                City = advert.City,
                AdvertDetails = advert.AdvertDetails,
                Price = advert.Price,
                Id = advert.Id
            };
        }

        public Advert ShadowDeleteAdvert(AdvertModel advert, int userId)
        {
            var advertForDeletion = MapToCreationModel(advert);
            var mappedAdvert = MapToAdvertEntity(advertForDeletion, userId, "Deleted");
            var shadowDeletedAdvert = _repo.ShadowDeleteAdvert(mappedAdvert);
            return shadowDeletedAdvert;
        }
    }
}   
