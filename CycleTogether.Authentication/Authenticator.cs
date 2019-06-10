using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using WebModels;
using CycleTogether.BindingModels;

namespace CycleTogether.Authentication
{
    public class Authenticator : IAuthentication
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly TokenGenerator _tokenGenerator;

        public Authenticator(IMapper mapper, IUserRepository users, TokenGenerator tokenGenerator, IOptions<AppSettings> appSetings)
        {
            _users = users;
            _mapper = mapper;
            _appSettings = appSetings.Value;
            _tokenGenerator = tokenGenerator;
        }

        public User Register(User user)
        {
            user.Password = this._tokenGenerator.HashPassword(user.Password);
            return SaveUser(user);
        }

        public string Authenticate(string email, string password)
        {
            return _tokenGenerator.Generate(email, password);
        }

        private User SaveUser(User user)
        {
            UserEntry entityUser = _mapper.Map<UserEntry>(user);
            entityUser = _users.Create(entityUser);
            return _mapper.Map<User>(entityUser);
        }
    }
}
