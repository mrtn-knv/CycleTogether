using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CycleTogether.Claims
{
    public class ClaimsRetriever
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
