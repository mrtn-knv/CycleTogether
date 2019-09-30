using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IUserSubscriptions : ICache<Route>
    {
        IEnumerable<RouteView> All();
    }
}
