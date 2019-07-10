using System;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthentication _authenticator;
        public AuthorizationController(IAuthentication authenticator)
        {
            _authenticator = authenticator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]User userParams)
        {
            var authenticated = _authenticator.Authenticate(userParams.Email, userParams.Password);
            if (!string.IsNullOrWhiteSpace(authenticated))
            {
                return Ok(new { token = authenticated });
            }

            //TODO: Add validations and return validations errors.
            return Content("");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                _authenticator.Register(user);
            }
            catch (Exception ex)
            {

                return Content(ex.ToString());
            }

            return Ok(true);
        }
    }
}