﻿using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IRoutesCache : ICache<Route>
    {        
        IEnumerable<RouteView> All();
    }
}
