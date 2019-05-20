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
        public SignInController(IAuthentication authenticator)
        {
            _authenticator = authenticator;  
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public string Authenticate([FromBody]UserWeb userParams)
        {
            
            var authenticated = _authenticator.Authenticate(userParams.Email, userParams.Password);
            if (!string.IsNullOrWhiteSpace(authenticated))
            {
                return authenticated;
            }

            return "Incorrect username or password.";
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