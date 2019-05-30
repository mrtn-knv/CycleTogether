using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IAuthentication
    {
        string Authenticate(string email, string password);
        UserWeb Register(UserWeb user);
    }
}
