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
            user.Password = this._tokenGenerator.HashPassword(user.Password);
            return SaveUser(user);
        }

        public string Authenticate(string email, string password)
        {
            return _tokenGenerator.Generate(email, password);
        }

        private UserWeb SaveUser(UserWeb user)
        {
            User entityUser = _mapper.Map<User>(user);
            entityUser = _users.Create(entityUser);
            return _mapper.Map<UserWeb>(entityUser);
        }
    }
}
