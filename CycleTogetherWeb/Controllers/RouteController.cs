using System;
using System.Collections.Generic;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using CycleTogether.Claims;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteManager _routes;
        private readonly ClaimsRetriever _claims;
        public RouteController(IRouteManager routes, ClaimsRetriever claimsManager)
        {
            _routes = routes;
            _claims = claimsManager;
        }

        [HttpGet("All")]
        public IEnumerable<Route> GetAll()
        {
            return _routes.GetAll();
        }

        // GET: api/Route/5
        [HttpGet("{id}", Name = "Get")]
        public Route Get(Guid id)
        {            
            return _routes.Get(id);
        }

        // POST: api/Route/new
        [HttpPost("new")]
        public Route Create([FromBody] Route route)
        {            
            var id = _claims.Id();
            return _routes.Create(route, id);
        }

        // POST: api/Route/subscribe
        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody]Route route)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            if (_routes.Subscribe(currentUserId, route.Id))
                return Ok();
            
            return Content("You can't subscribe for this trip.");          
        }

        // POST: api/Route/unsubscribe
        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe([FromBody]Route route)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            _routes.Unsubscribe(currentUserId, route.Id);
            return Ok();
        }

        // POST: api/Route/edit
        [HttpPost("edit")]
        public Route Update([FromBody]Route route)
        {
            var currentUserId = _claims.Id();
            return _routes.Update(route, currentUserId);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var userId = _claims.Id();
            _routes.Remove(id, userId);
        }
    }
}
