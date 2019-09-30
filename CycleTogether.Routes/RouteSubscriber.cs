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
        private readonly IUserHistoryCache _historyCache;
        private readonly IUserSubscriptions _subscriptions;
        private readonly IUserOwnedRoutes _routesByUser;
        private readonly IRoutesCache _cache;
        private readonly IUnitOfWork _db;

        public RouteSubscriber(IMapper mapper,
                               ISubscription subscriber,
                               IUserSubscriptions subscriptions,
                               IUserHistoryCache historyCache,
                               IUnitOfWork db, 
                               IUserOwnedRoutes routesByUser,
                               IRoutesCache cache)
        {
            _db = db;
            _mapper = mapper;
            _subscriber = subscriber;
            _subscriptions = subscriptions;
            _historyCache = historyCache;
            _cache = cache;
            _routesByUser = routesByUser;
        }

        public bool Subscribe(Guid userId, Guid routeId)
        {
            if (_subscriber.Subscribe(userId, routeId))
            {
               
                var route = _subscriptions.Get(routeId.ToString());
                route.Subscribed.ToList().Add(new UserRoute { RouteId = routeId, UserId = userId });
                _cache.Update(route);
                _routesByUser.Update(route);
                _subscriptions.Update(route);
                return true;
            }

            return false;
        }
        public bool Unsubscribe(Guid userId, Guid routeId)
        {
            var current = _db.UserRoutes.GetAll().FirstOrDefault(ur => ur.RouteId == routeId && ur.UserId == userId);
            if (current != null)
            {
                var routeInCache = _subscriptions.Get(routeId.ToString());
                routeInCache.Subscribed = _db.UserRoutes
                    .GetAll()
                    .Where(ur => ur.RouteId == routeId)
                    .Select(_mapper.Map<UserRoute>)
                    .ToList();

                routeInCache.Subscribed
                .ToList()
                .Remove(new UserRoute { RouteId = routeId, UserId = userId });
                _subscriber.Unsubscribe(current);
                _routesByUser.Update(routeInCache);
                _subscriptions.Update(routeInCache);
                _cache.Update(routeInCache);

                return true;
            }
            return false;
        }

        public IEnumerable<RouteView> History(string userId)
        {
            return _historyCache.All(userId);
        }

        public IEnumerable<RouteView> GetUsersSubscriptions(string userId)
        {
            return _subscriptions.All();
        }

    }
}
