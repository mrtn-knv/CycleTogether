using CycleTogether.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.Claims;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IGallery _images;
        private readonly ClaimsRetriever _claims;
        public ImageController(IGallery images, ClaimsRetriever claims)
        {
            _images = images;
            _claims = claims;
        }
        [HttpGet("all/{routeId}")]
        public List<Picture> GetAll(Guid routeId)
        {
           return _images.GetAll(routeId.ToString());
        }

        [HttpGet("all/{routeId}/{imageId}")]
        public Picture GetImage(Guid imageId)
        {             
           return _images.Get(imageId.ToString());
        }

        
        [HttpPost("{routeId}")]
        public IActionResult Upload(string routeId, IFormFile pic)
        {
            if (pic != null)
            {
                return Ok(_images.Upload(pic, routeId));
            }

            return Content("You cannot upload empty file.");
        } 

        [HttpDelete("{routeId}/{imageId}")]
        public void Delete(string imageId, string routeId)
        {
            var currentUser = _claims.Id();
            _images.Delete(imageId, currentUser, routeId);
        }
    }
}