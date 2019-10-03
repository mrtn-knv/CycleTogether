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
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthentication _authenticator;
        private readonly IValidator<User> _validator;
        public AuthorizationController(IAuthentication authenticator, IValidator<User> validator)
        {
            _authenticator = authenticator;
            _validator = validator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]UserLogin userParams)
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
                var state = _validator.Validate(user, ruleSet: "all");
                if (state.IsValid) _authenticator.Register(user);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return Ok(true);
        }
    }
}