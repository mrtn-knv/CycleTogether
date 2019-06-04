using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteManager
    {
        Route Create(Route route, string id, string email);
        Route Update(Route route, string id);
        void Remove(Guid id, string userId);
        Route Get(Guid id);
        IEnumerable<Route> GetAll();
        bool Subscribe(string email, Route route);
        void Unsubscribe(string email, Route route);
    }
}
