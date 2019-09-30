using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Routes
{
    public class RouteCacheEvents
    {
        public delegate void AddInCache(Route route);
        public delegate Route UpdateCache(Route route);
        public delegate void RemoveFromCache(string id);

        public event AddInCache AddedToCache;
        public event UpdateCache UpdatedCache;
        public event RemoveFromCache RemovedFromCache;
        

        public virtual void OnAdd(Route route)
        {
            AddedToCache?.Invoke(route);
        }

        public virtual Route OnUpdate(Route route)
        {
            return UpdatedCache?.Invoke(route);
        }

        public virtual void OnRemove(string id)
        {
            RemovedFromCache?.Invoke(id);
        }
    }
}
