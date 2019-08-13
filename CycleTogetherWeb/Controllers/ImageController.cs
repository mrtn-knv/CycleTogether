using CycleTogether.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.Claims;
using System.Web.Http.Cors;

namespace CycleTogetherWeb.Controllers
{
    //[Authorize]
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

        [EnableCors(origins:"*", headers:"*", methods: "*")]
        [HttpPost("{routeId}")]
        public Picture Upload([FromBody]object pic)
        {
            var imagePath = "asadasd";
            var routeId = "testtest";
            return _images.Upload(imagePath, routeId);
        } 

        [HttpDelete("{routeId}/{imageId}")]
        public void Delete(string imageId, string routeId)
        {
            var currentUser = _claims.Id();
            _images.Delete(imageId, currentUser, routeId);
        }
    }
}