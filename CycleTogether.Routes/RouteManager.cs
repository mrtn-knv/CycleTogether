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
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IDifficultyCalculator _difficulty;
        private readonly ISearchManager _search;
        private readonly RouteCacheEvents _eventManager;
        private readonly IRoutesCache _cache;
        private readonly IUserOwnedRoutes _usersRoutes;
        private readonly IUserSubscriptions _subscriptions;
        private readonly IClaimsRetriever _claims;

        public RouteManager(IUnitOfWork db,
                            IMapper mapper,
                            IDifficultyCalculator difficulty,
                            IRoutesCache cache,
                            ISearchManager search,
                            IUserOwnedRoutes usersRoutes,
                            IUserSubscriptions subscriptions,
                            IClaimsRetriever claimsManager)
        {
            _eventManager = new RouteCacheEvents();
            _claims = claimsManager;           
            _db = db;
            _mapper = mapper;
            _difficulty = difficulty;
            _cache = cache;
            _search = search;
            _usersRoutes = usersRoutes;
            _subscriptions = subscriptions;
        }

        public Route Create(Route route)
        {
            var newRoute = Save(SetProperties(route));

            if (route.EquipmentsIds != null)
                SaveRouteEquipments(newRoute, route.EquipmentsIds.ToList());

            _db.UserRoutes.Create(new UserRouteEntry { RouteId = newRoute.Id, UserId = route.UserId });
            _db.SaveChanges();
            _search.AddRouteToIndex(newRoute);

            _eventManager.AddedToCache += _cache.Add;
            _eventManager.AddedToCache += _usersRoutes.Add;
            _eventManager.AddedToCache += _subscriptions.Add;
            _eventManager.OnAdd(newRoute);

            return newRoute;
        }

        public IEnumerable<RouteView> AllByUser()
        {
            return _usersRoutes.All();
        }

        public IEnumerable<RouteView> GetAll()
        {
            return _cache.All();
        }

        public Route Get(Guid id)
        {
            try
            {
                var route = _cache.Get(id.ToString());
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

        public void Remove(string routeId)
        {
            var current = _db.Routes.GetById(Guid.Parse(routeId));
            var currentUserId = _claims.Id();
            if (currentUserId == current.UserId.ToString())
            {
                _db.Routes.Delete(Guid.Parse(routeId));
                _db.SaveChanges();
                _cache.Remove(routeId.ToString());
                _search.RemoveFromIndex(_mapper.Map<Route>(current));
                _eventManager.RemovedFromCache += _cache.Remove;
                _eventManager.RemovedFromCache += _usersRoutes.Remove;
                _eventManager.RemovedFromCache += _subscriptions.Remove;
                _eventManager.OnRemove(routeId.ToString());
            }
        }

        public Route Update(Route route)
        {
            var currentUserId = _claims.Id();
            if (currentUserId == route.UserId.ToString())
            {
                var updated = SaveUpdated(route);
                _search.UpdateIndex();
                return updated;
            }

            throw new ArgumentException();
        }


        private void SaveRouteEquipments(Route newRoute, List<Guid> equipmentIds)
        {
            foreach (var equipmentId in equipmentIds)
            {
                _db.RouteEquipments.Create(new RouteEquipmentEntry() { RouteId = newRoute.Id, EquipmentId = equipmentId });
            }
            _db.SaveChanges();
        }

        private RouteEntry SetProperties(Route route)
        {

            var newRoute = _mapper.Map<RouteEntry>(route);
            newRoute.Difficulty = _difficulty.DifficultyLevel(route);
            //newRoute.UserId = Guid.Parse(userId);
            return newRoute;
        }
        //private void FillAllEquipmentsForRoutes()
        //{
        //    var allRoutes = _db.Routes.GetAll();

        //    foreach (var route in allRoutes)
        //    {
        //        try
        //        {
        //            route.RouteEquipments = _db.RouteEquipments.GetAll()
        //                               .Where(equipment => equipment.RouteId == route.Id)
        //                               .ToList();
        //        }
        //        catch (NullReferenceException ex)
        //        {
        //            Log.Error("{0} Exception was thrown: {1}", DateTime.Now, ex);
        //        }

        //    }

        //}

        private Route Save(RouteEntry route)
        {

            return _mapper.Map<Route>(_db.Routes.Create(route));
        }

        private Route SaveUpdated(Route route)
        {
            _db.Routes.Edit(_mapper.Map<RouteEntry>(route));
            _db.SaveChanges();
            _eventManager.UpdatedCache += _cache.Update;
            _eventManager.UpdatedCache += _usersRoutes.Update;
            _eventManager.UpdatedCache += _subscriptions.Update;
            return _eventManager.OnUpdate(route);
        }


    }
}
