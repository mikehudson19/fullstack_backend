using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FullStack.API.Services;
using FullStack.ViewModels;

namespace FullStack.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("authenticate")]
        //public IActionResult Authenticate(AuthenticateRequest model)
        //{
        //    var response = _userService.Authenticate(model);

        //    if (response == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(response);
        //}

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        
        [HttpGet("unsecure")]
        public IActionResult GetAllUnsecure()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserModel> CreateUser(UserForCreationModel user)
        {

            var mappedUser = _userService.MapToUserEntity(user);

            var createdUser = _userService.CreateUser(mappedUser);

            var userToReturn = _userService.MapToModel(mappedUser);

            return CreatedAtRoute("GetUser",
                                  new
                                  {
                                      id = createdUser.Id
                                  },
                                  userToReturn);
        }
    }
}
