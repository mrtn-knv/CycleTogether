using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteFilter
    {
        List<RouteView> RemovePassedRoutes(List<RouteView> routes);
    }
}
