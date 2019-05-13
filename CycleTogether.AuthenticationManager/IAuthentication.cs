using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.AuthenticationManager
{
    public interface IAuthentication
    {
        User Authenticate(string email, string password);
        User Register(User user);
    }
}
