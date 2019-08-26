using System;
using System.Collections.Generic;
using System.Text;

namespace CycleTogether.Contracts
{
    public interface ITokenGenerator
    {
        string HashPassword(string password);
        string Generate(string email, string password);
    }
}
