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
    public class UserOwnedRoutes : IUserOwnedRoutes
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
                var userOwnRoutes = new List<Route>
                {
                    route
                };
                _redis.StringSet(key + _claims.Id(), JsonConvert.SerializeObject(userOwnRoutes));
                AddToCache(route);
            }
        }

        public IEnumerable<RouteView> All()
        {
            var userId = _claims.Id();
            try
            {
                var userRoutes = JsonConvert.DeserializeObject<List<RouteView>>(_redis.StringGet(key+userId));
                return userRoutes;
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
                if (_db.Routes.AllByUser(Guid.Parse(userId)).Any(ur => ur.Id == Guid.Parse(routeId)))
                {
                    var route = _db.Routes.AllByUser(Guid.Parse(userId)).Where(r => r.Id == Guid.Parse(routeId));
                    var userOwnRoutes = new List<Route>
                    {
                        _mapper.Map<Route>(route)
                    };
                    _redis.StringSet(key + userId, JsonConvert.SerializeObject(userOwnRoutes));
                    return _mapper.Map<Route>(route);
                }
                else
                    throw new InvalidOperationException();
            }
        }

        public void Remove(string routeId)
        {
            var userId = _claims.Id();
            try
            {
                var routesByUser = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
                if (routesByUser.Any(r => r.Id == Guid.Parse(routeId)))
                {
                    var routeToDelete = routesByUser.FirstOrDefault(route => route.Id == Guid.Parse(routeId));
                    routesByUser.Remove(routeToDelete);
                    _redis.StringSet(key + userId, JsonConvert.SerializeObject(routesByUser));
                }
                else
                    //Todo: Add error message.
                    throw new InvalidOperationException();
            }
            catch (ArgumentNullException)
            {

            }
        }

        public Route Update(Route route)
        {
            var userId = _claims.Id();
            try
            {
                return UpdateCache(route);
            }
            catch (ArgumentNullException)
            {
                var routesByUser = _db.Routes.AllByUser(Guid.Parse(userId))
                                   .Select(r => _mapper.Map<Route>(r));
                if (routesByUser.Any(r => r.Id == route.Id))
                {
                    var toUpdate = routesByUser.FirstOrDefault(r => r.Id == route.Id);
                    _redis.StringSet(key + userId, JsonConvert.SerializeObject(new List<Route>() { route }));
                    return route;
                }
                else
                    //Todo: add error message
                    throw new ArgumentException();
                
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

        private void AddAll()
        {
            var userId = _claims.Id();
            var routesByUser = _db.Routes
                               .AllByUser(Guid.Parse(userId))
                               .Select(route => _mapper.Map<Route>(route));
            _redis.StringSet(key + userId, JsonConvert.SerializeObject(routesByUser));
        }
    }
}
