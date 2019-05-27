using AutoMapper;
using CycleTogether.RoutesManager;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using WebModels;
using System.Linq;

namespace CycleTogether.Routes
{
    public class RouteManager : IRouteManager
    {
        private readonly IRouteRepository _routes;
        private readonly IMapper _mapper;
        private readonly IUserRepository _users;
        private readonly DifficultyCalculator _difficulty;

        public RouteManager(IRouteRepository routes, IMapper mapper, IUserRepository users, DifficultyCalculator difficulty)
        {
            _routes = routes;
            _mapper = mapper;
            _users = users;
            _difficulty = difficulty;
        }

        public RouteWeb Create(RouteWeb route, string email)
        {            
            return Save(route, email);
        }

        public IEnumerable<RouteWeb> GetAll()
        {
            var all = _routes.GetAll();
            var routes = MapAll(all);
            return routes;
        }


        public RouteWeb Get(Guid id)
        {
            var route = _routes.GetById(id);
            var found = _mapper.Map<RouteWeb>(route);
            return found;
        }

        public void Remove(Guid id)
        {

            _routes.Delete(id);
        }

        public bool Subscribe(string email, RouteWeb route)
        {
           return route.SubscribedMails.Contains(email) ? Unsubscribe(email, route) : AddToSubscribed(email, route);
        }
        public RouteWeb Update(RouteWeb route)
        {
            return SaveUpdated(route);
        }

        private bool AddToSubscribed(string email, RouteWeb route)
        {
            var subscribeTo = _mapper.Map<Route>(route);
            _routes.Subscribe(email, subscribeTo);
            return true;
        }
        private IEnumerable<RouteWeb> MapAll(IEnumerable<Route> routes)
        {   
            foreach (var route in routes)
            {               
                yield return _mapper.Map<RouteWeb>(route);
            }
        }

        private bool Unsubscribe(string email, RouteWeb route)
        {
            var routeToUnsubscribeFrom = _mapper.Map<Route>(route);
            _routes.Unsubscribe(email, routeToUnsubscribeFrom);
            return false;
        }

        private RouteWeb Save(RouteWeb route, string email)
        {
            route.Difficulty = _difficulty.DifficultyLevel(route);
            var routeNew = _mapper.Map<Route>(route);
            var currentUser = _users.GetByEmail(email);
            routeNew.CreatedBy = currentUser;
            routeNew.SubscribedMails.Add(currentUser.Email);
            currentUser.Routes.Add(routeNew);
            
            _routes.Create(routeNew);
            return _mapper.Map<RouteWeb>(routeNew);
        }

        private RouteWeb SaveUpdated(RouteWeb route)
        {
            var current = _mapper.Map<Route>(route);
            _routes.Edit(current);
            return _mapper.Map<RouteWeb>(current);
        }
    }
}
