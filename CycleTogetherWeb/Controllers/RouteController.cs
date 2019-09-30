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
        public IEnumerable<RouteView> GetAll()
        {
            return _routes.GetAll();
        }

        [HttpGet("all/mytrips")]
        public IEnumerable<RouteView> GetAllByUser()
        {
            return _routes.AllByUser();
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
                return Ok(_routes.Create(route));
            }
            return Ok(state.Errors);            
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
        public void Delete(string id)
        {
            _routes.Remove(id);
        }
    }
}
