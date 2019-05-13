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
        
        public WebModels.User Register(WebModels.User user)
        {
            throw new NotImplementedException();
        }

        WebModels.User IAuthentication.Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
