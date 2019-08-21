using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IDataRetriever
    {
        IEnumerable<RouteSearch> Find(string input);
    }
}
