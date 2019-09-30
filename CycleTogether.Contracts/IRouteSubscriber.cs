using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteSubscriber
    {
        bool Subscribe(Guid userId, Guid routeId);
        bool Unsubscribe(Guid userId, Guid routeId);
        IEnumerable<RouteView> GetUsersSubscriptions(string userId);
        IEnumerable<RouteView> History(string userId);
    }
}
