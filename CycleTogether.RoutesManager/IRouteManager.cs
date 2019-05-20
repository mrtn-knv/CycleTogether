using System;
using WebModels;

namespace CycleTogether.RoutesManager
{
    public interface IRouteManager
    {
        RouteWeb Create(RouteWeb route);
        RouteWeb Update(RouteWeb route);
        void Remove(Guid id);
        RouteWeb Get(Guid id);
    }
}
