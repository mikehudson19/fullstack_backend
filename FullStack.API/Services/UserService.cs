using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FullStack.API.Helpers;
using FullStack.ViewModels;
using FullStack.Data.Entities;
using FullStack.Data;

namespace FullStack.API.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        User MapToUserEntity(UserForCreationModel user);
        User CreateUser(User user);
        UserModel MapToModel(User user);
    }

    public class UserService : IUserService
    {
        private IFullStackRepository _repo;
        private readonly AppSettings _appSettings;

        public UserService(IFullStackRepository repo, IOptions<AppSettings> appSettings)
        {
            this._repo = repo;
            this._appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //Get the user from the repository / database

            //*** Note about password. Never save clear text passwords in a database, for this test project it's ok, but change this before you show this project
            //to a potential employer ***

            var user = _repo.GetUsers().SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // map from DB entity to UserModel for the front-end
            var userModel = MapToModel(user);

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(userModel);

            // return the UserModel to the controller, NOT the entity
            return new AuthenticateResponse(userModel, token);
        }

        public IEnumerable<UserModel> GetAll()
        {
            //only use for testing
            var userList = _repo.GetUsers();
            return userList.Select(u => MapToModel(u));
        }

        public UserModel GetById(int id)
        {
            var userEntity = _repo.GetUser(id);
            if (userEntity == null) return null;

            return MapToModel(userEntity);
        }

        // helper methods
        public UserModel MapToModel(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Forenames = user.Forenames,
                Surname = user.Surname,
                Email = user.Email
            };
        }

        public User MapToUserEntity(UserForCreationModel user)
        {
            return new User
            {
                Id = user.Id,
                Forenames = user.Forenames,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                ConfirmPass = user.confirmPass
            };
        }

        public User CreateUser(User user)
        {
            var createdUser = _repo.CreateUser(user);
            return createdUser;

        }


        private string GenerateJwtToken(UserModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
