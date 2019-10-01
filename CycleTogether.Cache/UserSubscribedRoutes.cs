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
    public class UserSubscribedRoutes : IUserSubscriptions
    {
        private const string key = "subscriptions_";
        private readonly IDatabase _redis;
        private readonly IMapper _mapper;
        private readonly IClaimsRetriever _claims;
        private readonly IUnitOfWork _db;
        private readonly IRouteFilter _filter;


        public UserSubscribedRoutes(IDatabase redis, IMapper mapper, IClaimsRetriever claims, IUnitOfWork db, IRouteFilter filter)
        {
            _redis = redis;
            _mapper = mapper;
            _claims = claims;
            _db = db;
            _filter = filter;
        }

        public void Add(Route route)
        {
            var userId = _claims.Id();
            try
            {
                var routes = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
                routes.Add(route);
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(routes));
            }
            catch (ArgumentNullException)
            {
                var userSubscriptions = new List<Route>
                {
                    route
                };
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(userSubscriptions));
            }
        }



        public IEnumerable<RouteView> All()
        {
            var userId = _claims.Id();
            try
            {
                var subscriptions = JsonConvert.DeserializeObject<List<RouteView>>(_redis.StringGet(key + userId));
                return _filter.RemovePassedRoutes(subscriptions);
            }
            catch (ArgumentNullException)
            {
                this.AddAll();
                var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId))
                                    .Select(route => _mapper.Map<RouteView>(route)).ToList();
                return _filter.RemovePassedRoutes(subscriptions);
            }
        }

        public Route Get(string routeId)
        {
            var userId = _claims.Id();
            try
            {
                var subscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
                var route = subscriptions.FirstOrDefault(r => r.Id.ToString() == routeId);
                if (route == null)
                    return GetFromDatabase(routeId, userId);
                else
                    return route;


            }
            catch (ArgumentNullException)
            {
                return GetFromDatabase(routeId, userId);
            }
        }



        public void Remove(string routeId)
        {
            try
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
            catch (ArgumentNullException)
            {

            }
        }

        public Route Update(Route route)
        {
            var userId = _claims.Id();
            try
            {
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
            catch (ArgumentNullException)
            {
                if (_db.Routes.GetAll().All(r => r.Id == route.Id))
                {
                    var userSubscriptions = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(key + userId));
                    userSubscriptions.Add(route);
                    _redis.StringSet(key + userId, JsonConvert.SerializeObject(userSubscriptions));
                    return route;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private void AddAll()
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

        private Route GetFromDatabase(string routeId, string userId)
        {
            var route = _db.Routes.GetById(Guid.Parse(routeId));
            var userRoutes = new List<Route>();
            if (route != null)
            {
                userRoutes.Add(_mapper.Map<Route>(route));
                _redis.StringSet(key + userId, JsonConvert.SerializeObject(userRoutes));
                return _mapper.Map<Route>(route);
            }
            else
            {
                //Todo: add error message.
                throw new ArgumentException();
            }
        }
    }
}
