using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteManager
    {
        Route Create(Route route);
        Route Update(Route route);
        void Remove(string id);
        Route Get(Guid id);
        IEnumerable<RouteView> GetAll();
        IEnumerable<RouteView> AllByUser();
    }
}
