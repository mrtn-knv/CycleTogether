using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRoutesCache /*: IChache<Route>*/
    {
        void Add(Route route);
        void AddAll();
        Route Update(Route route);
        void Remove(string routeId);
        Route Get(string routeId);
        IEnumerable<RouteView> All();
    }
}
