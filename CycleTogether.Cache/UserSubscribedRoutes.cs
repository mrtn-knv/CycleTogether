using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels;

namespace Cache
{
    public class UserSubscribedRoutes : IRoutesCache
    {
        private const string key = "subscriptions_";
        private readonly IDatabase _redis;
        private readonly IMapper _mapper;
        private readonly IClaimsRetriever _claims;
        private readonly IUnitOfWork _db;


        public UserSubscribedRoutes(IDatabase redis, IMapper mapper, IClaimsRetriever claims, IUnitOfWork db)
        {
            _redis = redis;
            _mapper = mapper;
            _claims = claims;
            _db = db;
        }

        public void Add(Route route)
        {
            var userId = _claims.Id();
            var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
            routes.Add(route);
            _redis.StringSet(key + userId, JsonConvert.SerializeObject(routes));
        }

        public void AddAll()
        {
            var userId = _claims.Id();
            var routes = _db.UserRoutes
                            .GetAll()
                            .Where(ur => ur.UserId == Guid.Parse(userId))
                            .ToList()
                            .Select(ur => _db.Routes.GetById(ur.RouteId))
                            .ToList();

            _redis.StringSet(key + userId, JsonConvert.SerializeObject(routes.Select(route => _mapper.Map<Route>(route))));
        }

        public IEnumerable<RouteView> All()
        {
            var userId = _claims.Id();
            try
            {
                var subscriptions = JsonConvert.DeserializeObject<IEnumerable<Route>>(_redis.StringGet(key + userId));
                return subscriptions.Select(route => _mapper.Map<RouteView>(route));
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
                return subscriptions.Select(route => _mapper.Map<RouteView>(route));
            }
        }

        public Route Get(string routeId)
        {
            var userId = _claims.Id();
            try
            {
               var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
               return subscriptions.FirstOrDefault(route => route.Id.ToString() == routeId);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                return JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                       .FirstOrDefault(route => route.Id.ToString() == routeId);
            }
        }

        public void Remove(string routeId)
        {
            var userId = _claims.Id();
            var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
            var toDelete = subscriptions.FirstOrDefault(route => route.Id.ToString() == routeId);
            if (toDelete != null)
            {
                subscriptions.Remove(toDelete);
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(subscriptions));
            }
        }

        public Route Update(Route route)
        {
            var userId = _claims.Id();
            var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
            var toUpdate = subscriptions.FirstOrDefault(r => r.Id == route.Id);
            if (toUpdate != null)
            {
                subscriptions.Remove(toUpdate);
                subscriptions.Add(route);
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(subscriptions));
                return route;
            }
            else
            {
                //todo: Add error messages and add correct one to error message.
                throw new InvalidOperationException();
            }
        }
    }
}
