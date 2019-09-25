using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebModels;

namespace Cache
{
    public class UserOwnedRoutes : IRoutesCache
    {
        private const string key = "ownedtrips_";
        private readonly IDatabase _redis;
        private readonly IMapper _mapper;
        private readonly IClaimsRetriever _claims;
        private readonly IUnitOfWork _db;

        public UserOwnedRoutes(IDatabase redis, IMapper mapper, IClaimsRetriever claims, IUnitOfWork db)
        {
            _redis = redis;
            _mapper = mapper;
            _claims = claims;
            _db = db;
        }

        public void Add(Route route)
        {
            try
            {
                AddToCache(route);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                AddToCache(route);
            }
        }

        public void AddAll()
        {
            var userId = _claims.Id();
            var routesByUser = _db.Routes
                               .AllByUser(Guid.Parse(userId))
                               .Select(route => _mapper.Map<Route>(route));
            _redis.StringSet(key + userId, JsonConvert.SerializeObject(routesByUser));
        }

        public IEnumerable<RouteView> All()
        {
            var userId = _claims.Id();
            try
            {
                return JsonConvert
                       .DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                       .Select(route => _mapper.Map<RouteView>(route));
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                return JsonConvert
                       .DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                       .Select(route => _mapper.Map<RouteView>(route));
            }
        }

        public Route Get(string routeId)
        {
            var userId = _claims.Id();
            try
            {
                var route = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                            .FirstOrDefault(r => r.Id == Guid.Parse(routeId));
                if (route != null)
                    return route;
                else
                    //Todo: Add error message.
                    throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                this.AddAll();
                var route = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                            .FirstOrDefault(r => r.Id == Guid.Parse(routeId));
                if (route != null)
                    return route;
                else
                    throw new InvalidOperationException();
            }
        }

        public void Remove(string routeId)
        {
            var userId = _claims.Id();
            var routesByUser = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key+userId));
            var routeToDelete = routesByUser.FirstOrDefault(route => route.Id == Guid.Parse(routeId));
            if (routeToDelete != null)
            {
                routesByUser.Remove(routeToDelete);
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(routesByUser));
            }
            else
            {
                //Todo: Add error message.
                throw new InvalidOperationException();
            }
        }

        public Route Update(Route route)
        {
            try
            {
                return UpdateCache(route);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                return this.UpdateCache(route);

            }
        }

        private Route UpdateCache(Route route)
        {
            var routesByUser = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + route.UserId.ToString()));
            var routeToUpdate = routesByUser.FirstOrDefault(r => r.Id == route.Id);
            if (routeToUpdate != null)
            {
                routesByUser.Remove(routeToUpdate);
                routesByUser.Add(route);
                _redis.StringSet(key + route.UserId.ToString(), JsonConvert.SerializeObject(routesByUser));
                return route;
            }
            else
                //TODO: add error message
                throw new InvalidOperationException();
        }

        private void AddToCache(Route route)
        {
            var routesByUser = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + route.UserId.ToString()));
            routesByUser.Add(route);
            _redis.StringSet(key + route.UserId.ToString(), JsonConvert.SerializeObject(routesByUser));
        }
    }
}
