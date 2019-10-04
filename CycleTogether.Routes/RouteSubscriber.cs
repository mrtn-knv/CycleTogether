using AutoMapper;
using Cache;
using CycleTogether.Contracts;
using DAL.Contracts;
using DAL.Models;
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
        private readonly RouteCacheEvents _eventManager;

        public RouteSubscriber(IMapper mapper,
                               ISubscription subscriber,
                               IUserSubscriptions subscriptions,
                               IUserHistoryCache historyCache,
                               IUnitOfWork db, 
                               IUserOwnedRoutes routesByUser,
                               IRoutesCache cache,
                               RouteCacheEvents eventManager)
        {
            _db = db;
            _mapper = mapper;
            _subscriber = subscriber;
            _subscriptions = subscriptions;
            _historyCache = historyCache;
            _cache = cache;
            _routesByUser = routesByUser;
            _eventManager = eventManager;
        }

        public bool Subscribe(Guid userId, Guid routeId)
        {
            if (_subscriber.Subscribe(userId, routeId))
            {

                var route = _subscriptions.Get(routeId.ToString());
                route.Subscribed = _db.UserRoutes.GetAll()
                                   .Where(ur => ur.RouteId == routeId)
                                   .Select(r => _mapper.Map<UserRoute>(r));
                route.Subscribed.ToList().Add(new UserRoute { RouteId = routeId, UserId = userId });
                _eventManager.AddedToCache += _subscriptions.Add;
                _eventManager.OnAdd(route);
                UpdateCache(route);
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
                .Remove(_mapper.Map<UserRoute>(current));
                _subscriber.Unsubscribe(current);
                _db.SaveChanges();
                _eventManager.RemovedFromCache += _subscriptions.Remove;
                _eventManager.OnRemove(routeId.ToString());
                UpdateCache(routeInCache);

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

        private void UpdateCache(Route route)
        {
            _eventManager.UpdatedCache += _cache.Update;
            _eventManager.UpdatedCache += _routesByUser.Update;
            _eventManager.OnUpdate(route);
        }

    }
}
