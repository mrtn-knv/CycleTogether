using System;
using System.Collections.Generic;
using System.Text;

namespace CycleTogether.Contracts
{
    public interface IClaimsRetriever
    {
        string Email();
        string FullName();
        string Id();
    }
}
