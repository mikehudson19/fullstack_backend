using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FullStack.ViewModels;
using FullStack.API.Services;


namespace FullStack.API.Controllers
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {

        private IAdvertService _advertService;

        public AdvertsController(IAdvertService advertService)
        {
            this._advertService = advertService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAdverts()
        {
            var authUser = this.HttpContext.Items["User"] as UserModel;
            var userId = authUser.Id;

            var adverts = _advertService.GetAll(userId);

            if (adverts == null)
            {
                return NotFound();
            }
            return Ok(adverts);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAdvert")]
        public IActionResult GetAdvert(int id)
        {
            var advert = _advertService.GetById(id);

            if (advert == null)
            {
                return NotFound();
            }

            return Ok(advert);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<AdvertModel> CreateAdvert(AdvertForCreationModel advert)
        {
            // Get the authorised user
            var authUser = this.HttpContext.Items["User"] as UserModel;
            var userId = authUser.Id;

            var createdAdvert = _advertService.CreateAdvert(advert, userId);

            return CreatedAtRoute("GetAdvert", new { id = createdAdvert.Id }, createdAdvert);

        }
    }
}
