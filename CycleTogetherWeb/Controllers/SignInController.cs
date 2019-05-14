using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CycleTogether.AuthenticationManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using BCrypt;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using DAL;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IAuthentication _authenticator;        

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]UserWeb userParams)
        {
            var passwordHashed = BCrypt.Net.BCrypt.HashPassword(userParams.Password);
            var authenticated = _authenticator.Authenticate(userParams.Email, passwordHashed);
            if (authenticated != null)
            {
                return Ok(authenticated);
            }

            return BadRequest(new {message = "Username or password is not valid." });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public UserWeb Register([FromBody]UserWeb user)
        {           
            var registered = _authenticator.Register(user);
            return registered;
        }
    }
}