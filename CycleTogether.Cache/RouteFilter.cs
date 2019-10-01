using CycleTogether.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels;

namespace Cache
{
    public class RouteFilter : IRouteFilter
    {

        public List<RouteView> RemovePassedRoutes(List<RouteView> routes)
        {
            return routes.Where(r => DateTime.Compare(DateTime.Now, r.StartTime) <= 0).ToList();
        }
    }
}
