using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebModels;

namespace CycleTogether.RoutesManager
{
    public interface IRouteManager
    {
        RouteWeb Create(RouteWeb route, string email);
        RouteWeb Update(RouteWeb route);
        void Remove(Guid id);
        RouteWeb Get(Guid id);        
    }
}
