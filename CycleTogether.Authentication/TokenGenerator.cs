using DAL;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebModels;

namespace CycleTogether.Authentication
{
    public class TokenGenerator
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _users;

        public TokenGenerator(IOptions<AppSettings> appSettings, IUserRepository users)
        {
            _appSettings = appSettings.Value;
            _users = users;
        }

        public User IsValid(string email)
        {
           var user =  _users.GetByEmail(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public  string Generate(string email)
        {

            var user = _users.GetByEmail(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encoded = tokenHandler.WriteToken(token);
            return encoded;
        }
    }
}
