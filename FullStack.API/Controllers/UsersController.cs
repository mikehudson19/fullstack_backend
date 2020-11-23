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

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null) return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

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

        [HttpGet("auth")]
        public IActionResult GetAuthUser()
        {
            var authUser = this.HttpContext.Items["User"] as UserModel;
            return Ok(authUser);
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
            var createdUser = _userService.CreateUser(user);

            return CreatedAtRoute("GetUser",
                                  new
                                  {
                                      id = createdUser.Id
                                  },
                                  createdUser);
        }

        [Authorize]
        [HttpPut]
        public ActionResult UpdateUser(UserForCreationModel user)
        {
            var authUser = this.HttpContext.Items["User"] as UserModel;

            _userService.UpdateUser(user, authUser.Id);

            return NoContent();
        }

        [Authorize]
        [HttpPut]
        [Route("password")]
        public ActionResult UpdateUserPassword(PasswordModel passwords)
        {
            var authUser = this.HttpContext.Items["User"] as UserModel;

            var updatedUser =  _userService.UpdateUserPassword(passwords, authUser.Id);

            if (updatedUser == null) return BadRequest(new { message = "Current password is incorrect and your change was not saved." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
