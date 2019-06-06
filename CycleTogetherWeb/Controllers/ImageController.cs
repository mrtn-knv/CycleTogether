using CycleTogether.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebModels;


namespace CycleTogetherWeb.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IPicture _images;
        private readonly ClaimsRetriever _claims;
        public ImageController(IPicture images, ClaimsRetriever claims)
        {
            _images = images;
            _claims = claims;
        }
        [HttpGet("All/{routeId}")]
        public List<Picture> GetAll(Guid routeId)
        {
           return _images.GetAll(routeId.ToString());
        }

        [HttpGet("{imageId}")]
        public Picture GetImage(Guid imageId)
        {             
           return _images.Get(imageId.ToString());
        }

        [HttpPost("{routeId}")]
        public Picture Upload([FromBody]string imagePath, string routeId)
        {
            return _images.Upload(imagePath, routeId);
        } 

        [HttpDelete("{routeId}")]
        public void Delete(string publicId, string routeId)
        {
            var currentUser = _claims.Id();
            _images.Delete(publicId, currentUser, routeId);
        }
    }
}