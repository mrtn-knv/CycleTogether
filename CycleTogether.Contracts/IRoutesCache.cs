using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRoutesCache : IChache<Route>
    {
        void AddUserRoutes(List<Route> userRoutes, string userId);
        void AddUserSubsciptions(List<Route> subscribedRoutes, string userId);
        IEnumerable<Route> UserSubscriptions(string userId);
    }
}
