using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.RoutesDifficultyManager;
using CycleTogether.RoutesSubscriber;
using CycleTogether.Contracts;
using System.Linq;


namespace CycleTogether.Routes
{
    public class RouteManager : IRouteManager
    {
        private readonly IRouteRepository _routes;
        private readonly IMapper _mapper;
        private readonly IUserRouteRepository _subscriber;
        private readonly DifficultyCalculator _difficulty;
        private readonly Subscription _subscription;
        private readonly IRouteEquipmentRepositoy _routeEquipments;
        private readonly IRoutesCache _cache;

        public RouteManager(IRouteRepository routes,
                            IMapper mapper,
                            IUserRouteRepository userRoutes,
                            DifficultyCalculator difficulty,
                            Subscription subscription,
                            IRoutesCache cache,
                            IRouteEquipmentRepositoy routeEquipments)
        {
            _routes = routes;
            _mapper = mapper;
            _subscriber = userRoutes;
            _difficulty = difficulty;
            _subscription = subscription;
            _routeEquipments = routeEquipments;
            _cache = cache;

        }

        public Route Create(Route route, string userId)
        {
            var newRoute = Save(SetProperties(route, userId));
            SaveRouteEquipments(newRoute, route.EquipmentsIds);
            _subscriber.Create(new UserRouteEntry { RouteId = newRoute.Id, UserId = Guid.Parse(userId) });
            _cache.AddItem(newRoute);
            _cache.AddUserRoutes(new List<Route>() { newRoute }, userId);
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
                var userRoutes = _subscriber.GetAll().Where(ur => ur.UserId == Guid.Parse(userId));
                var routes = userRoutes.Select(userRoute => _mapper.Map<Route>(_routes.GetById(userRoute.RouteId)));
                _cache.AddUserSubsciptions(routes.ToList(), userId);
                return _cache.UserSubscriptions(userId);
            }
            return subscriptions;
        }

        public IEnumerable<Route> AllByUser(Guid userId)
        {
            var userRoutes = _cache.AllBy(userId.ToString());
            if (userRoutes == null)
            {
                var routes = _routes.AllByUser(userId).Select(route => _mapper.Map<Route>(route));
                _cache.AddUserRoutes(routes.ToList(), userId.ToString());
                return _cache.AllBy(userId.ToString());
            }
            return userRoutes;
        }

        private void SaveRouteEquipments(Route newRoute, IEnumerable<Guid> equipmentIds)
        {
            foreach (var equipmentId in equipmentIds)
            {
                _routeEquipments.Create(new RouteEquipmentEntry() { RouteId = newRoute.Id, EquipmentId = equipmentId });
            }

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
                var routes = _routes.GetAll().Select(route => _mapper.Map<Route>(route));
                _cache.AddAll(routes.ToList());
                return _cache.All();
            }
            return all;
        }

        private void FillAllEquipmentsForRoutes()
        {
            var allRoutes = _routes.GetAll();

            foreach (var route in allRoutes)
            {
                try
                {
                    route.RouteEquipments = _routeEquipments.GetAll()
                                       .Where(equipment => equipment.RouteId == route.Id)
                                       .ToList();
                }
                catch (NullReferenceException ex)
                {

                    //TODO: Add Logger.

                }

            }

        }

        public Route Get(Guid id)
        {
            try
            {
                _cache.GetItem(id.ToString());
                var route = _routes.GetById(id);
                route.UserRoutes = _subscriber.GetAll().Where(subscribed => subscribed.RouteId == route.Id).ToList();
                var found = _mapper.Map<Route>(route);
                found.Subscribed = route.UserRoutes.Select(ur => _mapper.Map<UserRoute>(ur)).ToList();

                found.Equipments = _routeEquipments.GetAll()
                                   .Where(re => re.RouteId == found.Id)
                                   .Select(re => _mapper.Map<RouteEquipments>(re)).ToList();

                return found;
            }
            catch (Exception ex)
            {
                //TODO: Add Logger.
                throw; 
            }
        }

        public bool Remove(Guid routeId, string userId)
        {
            var current = _routes.GetById(routeId);
            if (current.UserId.ToString() == userId)
            {
                _routes.Delete(routeId);
                _cache.RemoveItem(_mapper.Map<Route>(current));
                return true;
            }

            return false;
        }

        public Route Update(Route route, string currentUserId)
        {

            if (currentUserId == route.UserId.ToString())
            {
                return SaveUpdated(route);
            }

            return null;
        }

        private Route Save(RouteEntry route)
        {

            return _mapper.Map<Route>(_routes.Create(route));
        }

        private Route SaveUpdated(Route route)
        {
            var current = _mapper.Map<RouteEntry>(route);
            _routes.Edit(current);
            return _mapper.Map<Route>(current);
        }

        public bool Subscribe(Guid userId, Guid routeId)
        {
            return _subscription.Subscribe(userId, routeId);
        }
        public bool Unsubscribe(Guid userId, Guid routeId)
        {
            var current = _subscriber.GetAll().FirstOrDefault(ur => ur.RouteId == routeId && ur.UserId == userId);
            if (current != null)
            {
                return _subscription.Unsubscribe(current);
            }
            return false;
        }

    }
}
