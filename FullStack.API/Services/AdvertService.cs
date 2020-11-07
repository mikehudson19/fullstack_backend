﻿using System;
using FullStack.Data;
using FullStack.Data.Entities;
using FullStack.ViewModels;

namespace FullStack.API.Services
{
    public interface IAdvertService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        //IEnumerable<AdvertModel> GetAll();
        AdvertModel GetById(int id);
        Advert MapToAdvertEntity(AdvertForCreationModel advert, int userId);
        AdvertModel CreateAdvert(AdvertForCreationModel advert, int userId);
        AdvertModel MapToModel(Advert advert);
        //void DeleteAdvert(int id);
    }

    public class AdvertService : IAdvertService
    {
        private readonly IFullStackRepository _repo;

        public AdvertService(IFullStackRepository repo)
        {
            this._repo = repo;
        }

        public AdvertModel GetById(int id)
        {
            var advertEntity = _repo.GetAdvert(id);
            if (advertEntity == null) return null;

            return MapToModel(advertEntity);
        }

        public AdvertModel CreateAdvert(AdvertForCreationModel advert, int userId)
        {
            var mappedAdvert = MapToAdvertEntity(advert, userId);
            var createdAdvert = _repo.CreateAdvert(mappedAdvert);
            var advertToReturn = MapToModel(createdAdvert);
            return advertToReturn;
        }

        public Advert MapToAdvertEntity(AdvertForCreationModel advert, int userId)
        {
            return new Advert
            {
                Headline = advert.Headline,
                Province = advert.Province,
                City = advert.City,
                AdvertDetails = advert.AdvertDetails,
                Price = advert.Price,
                Status = "Live",
                UserId = userId
            };
        }

        public AdvertModel MapToModel(Advert advert)
        {
            return new AdvertModel
            {
                Headline = advert.Headline,
                Province = advert.Province,
                City = advert.City,
                AdvertDetails = advert.AdvertDetails,
                Price = advert.Price
            };
        }
    }
}