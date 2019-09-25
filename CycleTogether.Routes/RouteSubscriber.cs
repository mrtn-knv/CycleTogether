using AutoMapper;
using Cache;
using CycleTogether.Contracts;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels;

namespace CycleTogether.Routes
{
    public class RouteSubscriber : IRouteSubscriber
    {
        private readonly IMapper _mapper;
        private readonly ISubscription _subscriber;
        private readonly IRoutesCache _cache;
        private readonly IUnitOfWork _db;

        public RouteSubscriber(IMapper mapper,
                               ISubscription subscriber,
                               IRoutesCache cache,
                               IUnitOfWork db)
        {
            _mapper = mapper;
            _subscriber = subscriber;
            _cache = cache;
            _db = db;
        }

        public bool Subscribe(Guid userId, Guid routeId)
        {
            if (_subscriber.Subscribe(userId, routeId))
            {
                var route = _cache.Get(routeId.ToString());
                route.Subscribed.ToList().Add(new UserRoute { RouteId = routeId, UserId = userId });
                _cache.Edit(route);
            }

            return false;
        }
        public bool Unsubscribe(Guid userId, Guid routeId)
        {
            var current = _db.UserRoutes.GetAll().FirstOrDefault(ur => ur.RouteId == routeId && ur.UserId == userId);
            if (current != null)
            {
                //todo try catch.
                var routeInCache = _cache.GetItem(routeId.ToString());
                routeInCache.Subscribed = _db.UserRoutes
                    .GetAll()
                    .Where(ur => ur.RouteId == routeId)
                    .Select(_mapper.Map<UserRoute>)
                    .ToList();

                routeInCache.Subscribed
                .ToList()
                .Remove(new UserRoute { RouteId = routeId, UserId = userId });
                _cache.Edit(routeInCache);

                return _subscriber.Unsubscribe(current);
            }
            return false;
        }

        public IEnumerable<Route> History(string userId)
        {
            var subscriptions = _cache.UserSubscriptions(userId);
            if (subscriptions == null)
            {
                subscriptions = _db.UserRoutes.GetAll()
                    .Where(ur => ur.UserId == Guid.Parse(userId))
                    .Select(ur => ur.Route)
                    .Select(_mapper.Map<Route>);
                _cache.AddUserSubsciptions(subscriptions.ToList(), userId);
            }
            return subscriptions.Where(route => DateTime.Compare(DateTime.UtcNow, route.StartTime) > 0);
        }

        public IEnumerable<Route> GetUsersSubscriptions(string userId)
        {
            return UsersSubscribtions(userId);
        }

        private IEnumerable<Route> UsersSubscribtions(string userId)
        {
            var subscriptions = _cache.UserSubscriptions(userId);
            if (subscriptions == null)
            {
                var userRoutes = _db.UserRoutes.GetAll().Where(ur => ur.UserId == Guid.Parse(userId));
                subscriptions = userRoutes.Select(userRoute => _mapper.Map<Route>(_db.Routes.GetById(userRoute.RouteId)));
                _cache.AddUserSubsciptions(subscriptions.ToList(), userId);
            }
            return subscriptions.Where(route => DateTime.Compare(DateTime.UtcNow, route.StartTime) <= 0);
        }
    }
}
