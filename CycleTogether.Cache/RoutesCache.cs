using CycleTogether.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using WebModels;
using System.Linq;

namespace CycleTogether.Cache
{
    public class RoutesCache : IRoutesCache
    {
        private readonly IDatabase _redis;
        private const string key = "routes";


        public RoutesCache(IDatabase redis)
        {
            _redis = redis;
            
        }

        public void AddItem(Route route)
        {
            if (route != null)
            {
                var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key));
                routes.Add(route);
                _redis.StringSet(key, JsonConvert.SerializeObject(routes));
            }
        }

        public IEnumerable<Route> All()
        {
            try
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(key));
                return result;
            }
            catch (ArgumentNullException ex)
            {
                //TODO: Add logger for exception.
                return null;
            }

        }

        public IEnumerable<Route> AllBy(string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(userId+key));
            }
            catch (ArgumentNullException ex)
            {
                //TODO: Add logger for exception.
                return null;
            }
        }

        public void RemoveItem(Route route)
        {
            var all = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key));
            var current = all.FirstOrDefault(r => r.Id == route.Id);
            if (current != null)
            {
                all.RemoveAll(r => r.Id == current.Id);
                _redis.StringSet(key, JsonConvert.SerializeObject(all));
            }
        }

        public void AddAll(List<Route> routes)
        {
            if (routes != null && routes.Count > 0)
            _redis.StringSet(key, JsonConvert.SerializeObject(routes));
        }

        public void AddUserRoutes(List<Route> userRoutes, string userId)
        {
            try
            {
                if (userRoutes != null && userRoutes.Count > 0)
                {
                    var usersRoutes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(userId + key));
                    userRoutes.ForEach(route => usersRoutes.Add(route));
                    _redis.StringSet(userId + key, JsonConvert.SerializeObject(usersRoutes));
                }
            }
            catch (Exception ex)
            {
                _redis.StringSet(userId + key, JsonConvert.SerializeObject(userRoutes));
                //TODO add logger
            }
                    
                     
                
        }

        public void AddUserSubsciptions(List<Route> subscribedRoutes, string userId)
        {
            if (subscribedRoutes != null && subscribedRoutes.Count > 0)
                _redis.StringSet(userId, JsonConvert.SerializeObject(subscribedRoutes));
        }

        public IEnumerable<Route> UserSubscriptions(string userId)
        {              
            try
            {
               return JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(userId));
            }
            catch (ArgumentNullException ex)
            {

                return null;
            }
        }


        public Route GetItem(string id)
        {
            var all = this.All();
            return all.FirstOrDefault(r => r.Id == Guid.Parse(id));
           
        }
    }
}
