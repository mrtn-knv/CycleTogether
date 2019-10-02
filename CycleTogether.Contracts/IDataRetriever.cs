using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IDataRetriever
    {
        IEnumerable<RouteView> Find(string input);
    }
}
