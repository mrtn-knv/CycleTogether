using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IUserOwnedRoutes : ICache<Route>
    {
        IEnumerable<RouteView> All();
    }
}
