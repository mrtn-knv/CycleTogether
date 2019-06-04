using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CycleTogetherWeb
{
    public class ClaimsManager
    {
        private readonly IHttpContextAccessor _accessor;
        public ClaimsManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string GetEmail()
        {
            var claims = GetUserClaims();
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
        }

        public string GetId()
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
