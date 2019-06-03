using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteManager
    {
        RouteWeb Create(RouteWeb route, string id, string email);
        RouteWeb Update(RouteWeb route, string id);
        void Remove(Guid id, string userId);
        RouteWeb Get(Guid id);
        IEnumerable<RouteWeb> GetAll();
        bool Subscribe(string email, RouteWeb route);
        void Unsubscribe(string email, RouteWeb route);
    }
}
