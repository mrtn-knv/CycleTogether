using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.Contracts;
using System.Linq;
using Serilog;

namespace CycleTogether.Routes
{
    public class RouteManager : IRouteManager
    {
        private readonly IMapper _mapper;
        private readonly IDifficultyCalculator _difficulty;
        private readonly ISubscription _subscriber;
        private readonly IRoutesCache _cache;
        private readonly ISearchManager _search;
        private readonly IUnitOfWork _db;

        public RouteManager(IUnitOfWork db,
                            IMapper mapper,
                            IDifficultyCalculator difficulty,
                            ISubscription subscription,
                            IRoutesCache cache,
                            ISearchManager search)
        {
            _db = db;
            _mapper = mapper;
            _difficulty = difficulty;
            _subscriber = subscription;
            _cache = cache;
            _search = search;

        }

        public Route Create(Route route, string userId)
        {
            var newRoute = Save(SetProperties(route, userId));

            if (route.EquipmentsIds != null)
                SaveRouteEquipments(newRoute, route.EquipmentsIds.ToList());
            _db.UserRoutes.Create(new UserRouteEntry { RouteId = newRoute.Id, UserId = Guid.Parse(userId) });
            _cache.AddItem(newRoute);
            _db.SaveChanges();
            _cache.AddUserRoutes(new List<Route>() { newRoute }, userId);
            _search.AddRouteToIndex(newRoute);
            return newRoute;
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

        public IEnumerable<Route> AllByUser(Guid userId)
        {
            var userRoutes = _cache.AllBy(userId.ToString());
            if (userRoutes == null)
            {
                var routes = _db.Routes.AllByUser(userId).Select(route => _mapper.Map<Route>(route));
                _cache.AddUserRoutes(routes.ToList(), userId.ToString());
                return _cache.AllBy(userId.ToString());
            }
            return userRoutes;
        }

        private void SaveRouteEquipments(Route newRoute, List<Guid> equipmentIds)
        {
            foreach (var equipmentId in equipmentIds)
            {
                _db.RouteEquipments.Create(new RouteEquipmentEntry() { RouteId = newRoute.Id, EquipmentId = equipmentId });
            }
            _db.SaveChanges();
        }

        private RouteEntry SetProperties(Route route, string userId)
        {

            var newRoute = _mapper.Map<RouteEntry>(route);
            newRoute.Difficulty = _difficulty.DifficultyLevel(route);
            newRoute.UserId = Guid.Parse(userId);
            return newRoute;
        }

        public IEnumerable<Route> GetAll()
        {
            var all = _cache.All();
            if (all == null)
            {
                FillAllEquipmentsForRoutes();
                var routes = _db.Routes.GetAll().Select(route => _mapper.Map<Route>(route));
                _cache.AddAll(routes.ToList());
                return _cache.All();
            }
            return all;
        }

        private void FillAllEquipmentsForRoutes()
        {
            var allRoutes = _db.Routes.GetAll();

            foreach (var route in allRoutes)
            {
                try
                {
                    route.RouteEquipments = _db.RouteEquipments.GetAll()
                                       .Where(equipment => equipment.RouteId == route.Id)
                                       .ToList();
                }
                catch (NullReferenceException ex)
                {
                    Log.Error("{0} Exception was thrown: {1}", DateTime.Now, ex);
                }

            }

        }

        public Route Get(Guid id)
        {
            try
            {
                var route = _cache.GetItem(id.ToString());
                if (route == null)
                {
                    route = _mapper.Map<Route>(_db.Routes.GetById(id));
                }

                route.Subscribed = _db.UserRoutes.GetAll()
                                   .Where(subscribed => subscribed.RouteId == route.Id)
                                   .ToList()
                                   .Select(ur => _mapper.Map<UserRoute>(ur));

                route.Equipments = _db.RouteEquipments.GetAll()
                                   .Where(re => re.RouteId == route.Id)
                                   .Select(re => _mapper.Map<RouteEquipments>(re)).ToList();
                return route;
            }
            catch (Exception ex)
            {
                Log.Error("{0} Exception was thrown: {1}", DateTime.Now, ex);
                return null;
            }
        }

        public bool Remove(Guid routeId, string userId)
        {
            var current = _db.Routes.GetById(routeId);
            if (current.UserId.ToString() == userId)
            {
                _db.Routes.Delete(routeId);
                _db.SaveChanges();
                _cache.RemoveItem(_mapper.Map<Route>(current));
                _search.RemoveFromIndex(_mapper.Map<Route>(current));
                return true;
            }

            return false;
        }

        public Route Update(Route route, string currentUserId)
        {
            //todo when edits, userId is not passed to the model
            if (currentUserId == route.UserId.ToString())
            {
                _search.UpdateIndex();
                return SaveUpdated(route);
            }

            return null;
        }

        private Route Save(RouteEntry route)
        {

            return _mapper.Map<Route>(_db.Routes.Create(route));
        }

        private Route SaveUpdated(Route route)
        {
            _cache.Edit(route);
            var current = _mapper.Map<RouteEntry>(route);
            _db.Routes.Edit(current);
            _db.SaveChanges();
            return _mapper.Map<Route>(current);
        }

        public bool Subscribe(Guid userId, Guid routeId)
        {
            if (_subscriber.Subscribe(userId, routeId))
            {
                var route = _cache.GetItem(routeId.ToString());
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
    }
}
