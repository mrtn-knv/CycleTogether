using AutoMapper;
using CycleTogether.RoutesManager;
using DAL.Contracts;
using DAL.Models;
using System;
using WebModels;

namespace CycleTogether.Routes
{
    public class RouteManager : IRouteManager
    {
        private readonly IRouteRepository _routes;
        private readonly IMapper _mapper;
        private readonly IUserRepository _users;

        public RouteManager(IRouteRepository routes, IMapper mapper, IUserRepository users)
        {
            _routes = routes;
            _mapper = mapper;
            _users = users;
        }

        public RouteWeb Create(RouteWeb route, string email)
        {            
            return Save(route, email);
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

        public RouteWeb Update(RouteWeb route)
        {
            return SaveUpdated(route);
        }
        private RouteWeb Save(RouteWeb route, string email)
        {           
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
