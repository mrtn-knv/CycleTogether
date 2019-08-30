using CycleTogether.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using WebModels;
using System.Linq;
using Serilog;

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
                result = RemoveInvalidRoutes(result);
                return result;
            }
            catch (ArgumentNullException ex)
            {
                Log.Information("{0} Exception was thrown: {1}", DateTime.Now, ex);
                return null;
            }
        }

        private IEnumerable<Route> RemoveInvalidRoutes(IEnumerable<Route> result)
        {
            var trips = new List<Route>();
            foreach (var route in result)
            {
                if (DateTime.Compare(DateTime.Now, route.StartTime) <= 0)
                {
                    trips.Add(route);
                }
            }
            return trips;
        }

        public IEnumerable<Route> AllBy(string userId)
        {
            try
            {
                return this.RemoveInvalidRoutes(JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(userId + key)));
            }
            catch (ArgumentNullException ex)
            {
                Log.Information("{0} Exception was thrown: {1}", DateTime.Now, ex);
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
                Log.Information("{0} Exception was thrown: {1}", DateTime.Now, ex);
                _redis.StringSet(userId + key, JsonConvert.SerializeObject(userRoutes));
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
                return this.RemoveInvalidRoutes(JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(userId)));
            }
            catch (ArgumentNullException ex)
            {
                Log.Information("{0} Exception was thrown: {1}", DateTime.Now, ex);
                return null;
            }
        }


        public Route GetItem(string id)
        {
            var all = this.All();
            return all.FirstOrDefault(r => r.Id == Guid.Parse(id));
        }

        /// <summary>
        /// Edit in both user's routes cache and all routes cache.
        /// </summary>
        /// <param name="route"></param>
        public void Edit(Route route)
        {
            EditInRoutesCache(route);
            EditInUserRoutesCache(route);
        }

        /// <summary>
        /// Edits route in user's routes cache.
        /// </summary>
        /// <param name="route"></param>
        private void EditInUserRoutesCache(Route route)
        {
            var routes = this.AllBy(route.UserId.ToString()).ToList();
            var toUpdate = routes.FirstOrDefault(r => r.Id == route.Id);
            if (toUpdate != null)
            {
                routes.Remove(toUpdate);
                routes.Add(route);
                _redis.StringSet(route.UserId.ToString() + key, JsonConvert.SerializeObject(routes));
            }
        }

        /// <summary>
        /// Edits route from Routes cache.
        /// </summary>
        /// <param name="route"></param>
        private void EditInRoutesCache(Route route)
        {
            var routes = this.All().ToList();
            var toUpdate = routes.FirstOrDefault(r => r.Id == route.Id);
            if (toUpdate != null)
            {
                routes.Remove(toUpdate);
                routes.Add(route);
                _redis.StringSet(key, JsonConvert.SerializeObject(routes));
            }
            
        }       
    }
}
