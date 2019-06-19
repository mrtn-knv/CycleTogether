﻿using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRouteManager
    {
        Route Create(Route route, string id);
        Route Update(Route route, string id);
        void Remove(Guid id, string userId);
        Route Get(Guid id);
        IEnumerable<Route> GetAll();
        bool Subscribe(Guid userId, Guid routeId);
        void Unsubscribe(Guid userId, Guid routeId);
    }
}
