﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using CycleTogether.Contracts;
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
        private readonly ClaimsManager _claimsManager;
        public RouteController(IRouteManager routes, ClaimsManager claimsManager)
        {
            _routes = routes;
            _claimsManager = claimsManager;
        }

        [HttpGet("All")]
        public IEnumerable<RouteWeb> GetAll()
        {
            return _routes.GetAll();
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
            var mail = _claimsManager.GetEmail();
            return _routes.Create(route, mail);
        }

        // POST: api/Route/subscribe
        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody]RouteWeb route)
        {
            var mail = _claimsManager.GetEmail();
            if (_routes.Subscribe(mail, route))
                return Ok();
            
            return Content("You can't subscribe for this trip.");          
        }

        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe([FromBody]RouteWeb route)
        {
            var mail = _claimsManager.GetEmail();
            _routes.Unsubscribe(mail, route);
            return Ok();
        }

        // POST: api/Route/edit
        [HttpPost("edit")]
        public RouteWeb Update([FromBody]RouteWeb route)
        {
            var id = _claimsManager.GetId();
            return _routes.Update(route, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var userId = _claimsManager.GetId();
            _routes.Remove(id, userId);
        }
    }
}
