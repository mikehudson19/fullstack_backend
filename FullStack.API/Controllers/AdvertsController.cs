using System;
using FullStack.API.Services;
using FullStack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertsController(IAdvertService advertService)
        {
            this._advertService = advertService;
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

        [HttpPost]
        public ActionResult<AdvertModel> CreateAdvert(AdvertForCreationModel advert)
        {
            // Get the authroised user
            var authUser = this.HttpContext.Items["User"] as UserModel;
            var userId = 1;

            var createdAdvert = _advertService.CreateAdvert(advert, userId);

            return CreatedAtRoute("GetAdvert", new { id = createdAdvert.Id }, createdAdvert);

        }
    }
}
