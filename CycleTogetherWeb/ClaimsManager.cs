using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
