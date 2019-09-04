using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Http;

namespace CycleTogether.Claims
{
    public class ClaimsRetriever : IClaimsRetriever
    {
        private readonly IHttpContextAccessor _accessor;
        public ClaimsRetriever(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string Email()
        {
            var claims = GetUserClaims();
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
        }

        public string FullName()
        {
            var claims = GetUserClaims();
            var firstName = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
            var lastName = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Surname)).Value;
            return firstName + " " + lastName;
        }

        public string Id()
        {
            var claims = GetUserClaims();
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
        }
        private IEnumerable<Claim> GetUserClaims()
        {
            var identity = _accessor.HttpContext.User.Identity as ClaimsIdentity;
            return identity.Claims;
        }
    }
}
