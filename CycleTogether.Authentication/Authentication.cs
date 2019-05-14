using AutoMapper;
using CycleTogether.AuthenticationManager;
using DAL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebModels;

namespace CycleTogether.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly IRepository<User> _users;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Authentication(IMapper mapper, IRepository<User> users, IOptions<AppSettings> appSetings)
        {
            _users = users;
            _mapper = mapper;
            _appSettings = appSetings.Value;

        }

        public UserWeb Register(UserWeb user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            var userNew = _mapper.Map<User>(user);           
            _users.Create(userNew);            
            return user;

        }
        //TODO implement method
        public SecurityToken Authenticate(string email, string password)
        {

            var validUser = TokenGenerator.IsValid(email);
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, validUser.Password);
            if (isValidPassword)
            {
                var token = TokenGenerator.Generate(email);
                return token;
            }

            return null;
        }
    }
}
