using System;
using System.Collections.Generic;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using FluentValidation;
using System.Linq;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteManager _routes;
        private readonly IClaimsRetriever _claims;
        private readonly IValidator<Route> _validator;
        
        public RouteController(IRouteManager routes, IClaimsRetriever claimsManager, IValidator<Route> validator)
        {
            _routes = routes;
            _claims = claimsManager;
            _validator = validator;
        }

        [HttpGet("all")]
        public IEnumerable<Route> GetAll()
        {
            return _routes.GetAll();
        }

        [HttpGet("subscribed")]
        public IEnumerable<Route> GetUserSubscribed()
        {
            var userId = _claims.Id();
            return _routes.GetUsersSubscriptions(userId);
        }

        [HttpGet("all/mytrips")]
        public IEnumerable<Route> GetAllByUser()
        {
            return _routes.AllByUser(Guid.Parse(_claims.Id()));
        }

        // GET: /Route/5
        [HttpGet("{id}", Name = "id")]
        public Route Get(Guid id)
        {            
            return _routes.Get(id);
        }
        

        // POST: /Route/new
        [HttpPost("new")]
        public IActionResult Create([FromBody] Route route)
        {
            var state = _validator.Validate(route, ruleSet: "all");
            if (state.IsValid)
            {
                var id = _claims.Id();
                return Ok(_routes.Create(route, id));
            }

            return Ok(state.Errors.FirstOrDefault().ErrorMessage);
            
        }

        // POST: /Route/subscribe
        [HttpPost("subscribe")]
        public bool Subscribe([FromBody]Route route)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            if (_routes.Subscribe(currentUserId, route.Id))
                return true;
            
            return false;          
        }

        // POST: /Route/unsubscribe
        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe([FromBody]Route route)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            if (_routes.Unsubscribe(currentUserId, route.Id))
            {
                return Ok(true);
            }

            return Ok(false);
        }

        // POST: /Route/edit
        [HttpPost("edit")]
        public Route Update([FromBody]Route route)
        {
            var currentUserId = _claims.Id();
            return _routes.Update(route, currentUserId);
        }

        // DELETE: /ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var userId = _claims.Id();
            if (_routes.Remove(id, userId))
            {
                return Ok(true);
            }

            return Ok(false);
        }
    }
}
