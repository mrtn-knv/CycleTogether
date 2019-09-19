using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteManager
    {
        Route Create(Route route, string id);
        Route Update(Route route, string id);
        bool Remove(Guid id, string userId);
        Route Get(Guid id);
        IEnumerable<Route> GetAll();
        bool Subscribe(Guid userId, Guid routeId);
        bool Unsubscribe(Guid userId, Guid routeId);
        IEnumerable<Route> AllByUser(Guid userId);
        IEnumerable<Route> GetUsersSubscriptions(string userId);
        IEnumerable<Route> History(string userId);
    }
}
