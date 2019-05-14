using DAL;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebModels;

namespace CycleTogether.Authentication
{
    public static class TokenGenerator
    {
        private static readonly AppSettings _appSettings;
        private static readonly UsersRepository _users;


        public static User IsValid(string email)
        {
           var user =  _users.GetByEmail(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public static SecurityToken Generate(string email)
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
            return token;
        }
    }
}
