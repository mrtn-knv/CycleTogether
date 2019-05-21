using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CycleTogetherWeb
{
    public class ClaimsManager
    {
        public string GetEmail(ClaimsIdentity identity)
        {
            var claims = identity.Claims;
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
        }
    }
}
