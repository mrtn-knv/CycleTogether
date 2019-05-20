using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/Route
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Route/5
        [HttpGet("{id}", Name = "Get")]
        public RouteWeb Get(Guid id)
        {
            var current = _routes.Get(id);
            return current;

        }

        // POST: api/Route/new
        [HttpPost("new")]
        public RouteWeb Create([FromBody] RouteWeb route)
        {
            return _routes.Create(route);
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
