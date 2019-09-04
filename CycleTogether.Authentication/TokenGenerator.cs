using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CycleTogether.BindingModels;
using CycleTogether.Contracts;

namespace CycleTogether.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly byte[] _secretTokenKey;
        private readonly int _tokenExpirationInMinutes;

        private readonly IUserRepository users;

        public TokenGenerator(IOptions<AppSettings> appSettings, IUserRepository users)
        {
            this._secretTokenKey = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
            this._tokenExpirationInMinutes = 30;

            this.users = users;
        }

        public string Generate(string email, string password)
        {
            var user = users.GetByEmail(email);
            return user != null ? GenerateToken(password, user) : string.Empty;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private SecurityTokenDescriptor Token(UserEntry user)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {   
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(_secretTokenKey),
                    algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private string GenerateToken(string enteredPassword, UserEntry user)
        {
            if (ArePasswordsMatching(enteredPassword, user.Password))
                return CreateToken(Token(user));
            else
                return string.Empty;
        }

        private bool ArePasswordsMatching(string enteredPassword, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, userPassword);
        }

        private string CreateToken(SecurityTokenDescriptor token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(token);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
