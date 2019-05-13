using CycleTogether.AuthenticationManager;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly Repository<User> _users;
        
        public UserWeb Register(UserWeb user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            var userNew = new User() { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password = passwordHash };
            _users.Create(userNew);
            return user;
            
        }
        //TODO implement method
        UserWeb IAuthentication.Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
