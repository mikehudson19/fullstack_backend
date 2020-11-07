using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FullStack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Data
{
    public interface IFullStackRepository
    {
        User GetUser(int id);
        List<User> GetUsers();
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);

        Advert GetAdvert(int id);
        List<Advert> GetAdverts();
        Advert CreateAdvert(Advert advert);
        Advert UpdateAdvert(Advert advert);
        void DeleteAdvert(int id);
    }

    public class FullStackRepository : IFullStackRepository
    {
        private FullStackDbContext _ctx;
        public FullStackRepository(FullStackDbContext ctx)
        {
            _ctx = ctx;
        }

        #region User CRUD Methods
        public List<User> GetUsers()
        {
            //throw new NotImplementedException();
            return _ctx.Users.ToList();
        }

        public User GetUser(int id)
        {
            //throw new NotImplementedException();
            return _ctx.Users.Find(id);
        }

        public User CreateUser(User user)
        {
            //throw new NotImplementedException();

            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();

            //var existing = _ctx.Users.SingleOrDefault(em => em.Id == e.Id);
            //if (existing == null) return null;

            //_ctx.Entry(existing).State = EntityState.Detached;
            //_ctx.Users.Attach(e);
            //_ctx.Entry(e).State = EntityState.Modified;
            //_ctx.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            //throw new NotImplementedException();

            var entity = _ctx.Users.Find(id);
            _ctx.Users.Remove(entity); //CAREFULL!! here when you copy and paste, change _ctx.Users to the new DBSet
            _ctx.SaveChanges();
        }
        #endregion


        #region Advert CRUD Methods
        public List<Advert> GetAdverts()
        {
            //throw new NotImplementedException();
            return _ctx.Adverts.ToList();
        }

        public Advert GetAdvert(int id)
        {
            //throw new NotImplementedException();
            return _ctx.Adverts.Find(id);
        }

        public Advert CreateAdvert(Advert advert)
        {
            //throw new NotImplementedException();

            _ctx.Adverts.Add(advert);
            _ctx.SaveChanges();
            return advert;
        }

        public Advert UpdateAdvert(Advert advert)
        {
            throw new NotImplementedException();

            //var existing = _ctx.Users.SingleOrDefault(em => em.Id == e.Id);
            //if (existing == null) return null;

            //_ctx.Entry(existing).State = EntityState.Detached;
            //_ctx.Users.Attach(e);
            //_ctx.Entry(e).State = EntityState.Modified;
            //_ctx.SaveChanges();
        }

        public void DeleteAdvert(int id)
        {
            //throw new NotImplementedException();

            var entity = _ctx.Adverts.Find(id);
            _ctx.Adverts.Remove(entity); //CAREFULL!! here when you copy and paste, change _ctx.Users to the new DBSet
            _ctx.SaveChanges();
        }
        #endregion



    }
}