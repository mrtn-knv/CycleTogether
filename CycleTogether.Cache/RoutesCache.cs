using CycleTogether.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using WebModels;
using System.Linq;
using DAL.Contracts;
using AutoMapper;

namespace CycleTogether.Cache
{
    public class RoutesCache : IRoutesCache
    {
        private const string key = "routes";
        private readonly IDatabase _redis;
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;


        public RoutesCache(IDatabase redis, IUnitOfWork db, IMapper mapper)
        {
            _redis = redis;
            _db = db;
            _mapper = mapper;
        }


        public void Add(Route route)
        {
            try
            {
                AddRouteToCache(route);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                AddRouteToCache(route);
            }
        }

        public void AddAll()
        {
            var routes = _db.Routes.GetAll().Select(route => _mapper.Map<Route>(route));
            _redis.StringSet(key, JsonConvert.SerializeObject(routes));
        }

        public IEnumerable<RouteView> All()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key))
                                  .Select(route => _mapper.Map<RouteView>(route));
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                return JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key))
                       .Select(route => _mapper.Map<RouteView>(route));
            }
        }

        public Route Get(string routeId)
        {
            try
            {
                return GetRoute(routeId);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                return GetRoute(routeId);
            }
        }       

        public void Remove(string routeId)
        {
            var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key));
            if (routes.Any(route => route.Id == Guid.Parse(routeId)))
            {
                var toDelete = routes.FirstOrDefault(r => r.Id == Guid.Parse(routeId));
                routes.Remove(toDelete);
                _redis.StringSet(key, JsonConvert.SerializeObject(routes));
            }
            else
                //Todo: add error message.
                throw new InvalidOperationException();
        }

        public Route Update(Route route)
        {
            var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key));
            if (routes.Any(r => r.Id == route.Id))
            {
                var current = routes.FirstOrDefault(r => r.Id == route.Id);
                routes.Remove(current);
                routes.Add(route);
                _redis.StringSet(key, JsonConvert.SerializeObject(routes));
                return route;
            }
            else
                //Todo: Add error message.
                throw new InvalidOperationException();
        }

        private Route GetRoute(string routeId)
        {
            var route = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key))
                                        .FirstOrDefault(r => r.Id == Guid.Parse(routeId));
            if (route != null)
                return route;
            else
                //TODO: add error message
                throw new InvalidOperationException();
        }

        private void AddRouteToCache(Route route)
        {
            var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key));
            routes.Add(route);
            _redis.StringSet(key, JsonConvert.SerializeObject(routes));
        }
    }
}
