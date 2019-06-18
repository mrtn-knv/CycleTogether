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
        private readonly IUserRepository _users;
        private readonly DifficultyCalculator _difficulty;
        private readonly Subscription _subscription;
        private readonly IUserRouteRepository _subscribtion;
        

        public RouteManager(IRouteRepository routes,
                            IMapper mapper, 
                            IUserRepository users,
                            DifficultyCalculator difficulty,
                            Subscription subscription)
        {
            _routes = routes;
            _mapper = mapper;
            _users = users;
            _difficulty = difficulty;
            _subscription = subscription;
        }

        public Route Create(Route route, string userId)
        {
            return Save(SetProperties(route, userId));
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
            return _routes.GetAll().Select(route => _mapper.Map<Route>(route));            
        }


        public Route Get(Guid id)
        {
            var route = _routes.GetById(id);
            var found = _mapper.Map<Route>(route);
            return found;
        }

        public void Remove(Guid routeId, string userId)
        {
            var current = _routes.GetById(routeId);
            if (current.UserId.ToString() == userId)
            {
                _routes.Delete(routeId);
            }            
        }

        public bool Subscribe(string id, Route route)
        {
            
              return _subscription.AddMail(id, route);

        }
        public Route Update(Route route, string currentUserId)
        {   
            
            if (currentUserId == route.CreatedBy.ToString())
            {
                return SaveUpdated(route);
            }

            return null;
        }

        private Route Save(RouteEntry route)
        {                        
            _routes.Create(route);
            return _mapper.Map<Route>(route);
        }

        private Route SaveUpdated(Route route)
        {
            var current = _mapper.Map<RouteEntry>(route);
            _routes.Edit(current);
            return _mapper.Map<Route>(current);
        }

        public void Unsubscribe(string id, Route route)
        {
            var userFromRoute = new UserRoute {RouteId = route.Id, UserId = Guid.Parse(id) };
            
                //_subscription.Unsubscribe(id, current);
            
        }

    }
}
