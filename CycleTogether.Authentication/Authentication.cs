using AutoMapper;
using CycleTogether.AuthenticationManager;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using WebModels;

namespace CycleTogether.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly TokenGenerator _tokenGenerator;

        public Authentication(IMapper mapper, IUserRepository users, TokenGenerator tokenGenerator, IOptions<AppSettings> appSetings)
        {
            _users = users;
            _mapper = mapper;
            _appSettings = appSetings.Value;
            _tokenGenerator = tokenGenerator;

        }

        public UserWeb Register(UserWeb user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            User userNew = _mapper.Map<User>(user);           
            userNew = _users.Create(userNew);

            var toReturn = _mapper.Map<UserWeb>(userNew);
            return toReturn;

        }
        //TODO implement method
        public string Authenticate(string email, string password)
        {

            var validUser = _tokenGenerator.IsValid(email);
            if (validUser != null)
            {
                var isValidPassword = BCrypt.Net.BCrypt.Verify(password, validUser.Password);
                if (isValidPassword)
                {
                    var token = _tokenGenerator.Generate(email);
                    return token;
                }
            }
            return null;
        }
    }
}
