using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IUserHistoryCache
    {
        IEnumerable<RouteView> All(string userId);
        void AddAll(string userId);
    }
}
