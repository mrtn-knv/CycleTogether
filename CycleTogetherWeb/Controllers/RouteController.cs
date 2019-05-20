using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CycleTogether.RoutesManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteManager _routes;
        public RouteController(IRouteManager routes)
        {
            _routes = routes;
        }

        // GET: api/Route/5
        [HttpGet("{id}", Name = "Get")]
        public RouteWeb Get(Guid id)
        {            
            return _routes.Get(id);
        }

        // POST: api/Route/new
        [HttpPost("new")]
        public RouteWeb Create([FromBody] RouteWeb route)
        {
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims;
            var mail = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;

            return _routes.Create(route, mail);
        }

        // PUT: api/Route/edit
        [HttpPost("edit")]
        public RouteWeb Update([FromBody]RouteWeb route)
        {
            return _routes.Update(route);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _routes.Remove(id);
        }

    }
}
