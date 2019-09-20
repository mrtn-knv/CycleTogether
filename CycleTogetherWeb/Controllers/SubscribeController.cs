using System;
using System.Collections.Generic;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly IClaimsRetriever _claims;
        private readonly IRouteSubscriber _subscriber;

        public SubscribeController(IClaimsRetriever claims, IRouteSubscriber subscriber)
        {
            _claims = claims;
            _subscriber = subscriber;
        }

        // POST: /Route/
        [HttpPost("{id}")]
        public bool Subscribe(string id)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            return _subscriber.Subscribe(currentUserId, Guid.Parse(id));
        }

        // POST: /Route/unsubscribe
        [HttpPost("unsubscribe/{id}")]
        public IActionResult Unsubscribe(string id)
        {
            var currentUserId = Guid.Parse(_claims.Id());
            return Ok(_subscriber.Unsubscribe(currentUserId, Guid.Parse(id)));
        }

        [HttpGet("subscribed")]
        public IEnumerable<Route> GetUserSubscribed()
        {
            var userId = _claims.Id();
            return _subscriber.GetUsersSubscriptions(userId);
        }

        [HttpGet("history")]
        public IEnumerable<Route> History()
        {
            var userId = _claims.Id();
            return _subscriber.History(userId);
        }

    }
}