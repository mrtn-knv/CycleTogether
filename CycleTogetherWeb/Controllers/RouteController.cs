using System;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using FluentValidation;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteManager _routes;
        private readonly IValidator<Route> _validator;
        
        public RouteController(IRouteManager routes,IValidator<Route> validator)
        {
            _routes = routes;
            _validator = validator;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_routes.GetAll());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpGet("all/mytrips")]
        public IActionResult GetAllByUser()
        {
            try
            {
                return Ok(_routes.AllByUser());
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }

        // GET: /Route/5
        [HttpGet("{id}", Name = "id")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_routes.Get(id));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
           
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
        public IActionResult Update([FromBody]Route route)
        {
            try
            {
                return Ok(_routes.Update(route));
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
            
        }

        // DELETE: /route/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _routes.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
        }
    }
}
