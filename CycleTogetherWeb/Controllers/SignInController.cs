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

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private IAuthentication _authenticator;

        [AllowAnonymous]
        [HttpPost]
        public UserWeb Authenticate([FromBody]UserWeb userParams)
        {
            var ecryptedPassword = BCrypt.Net.BCrypt.HashPassword(userParams.Password);
            return _authenticator.Authenticate(userParams.Email, ecryptedPassword);
        }

        [AllowAnonymous]
        public UserWeb Register([FromBody]UserWeb userParams)
        {
            return _authenticator.Register(userParams);
        }
    }
}