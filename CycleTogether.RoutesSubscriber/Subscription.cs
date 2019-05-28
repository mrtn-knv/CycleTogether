using System;
using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using WebModels;
using System.Collections.Generic;

namespace CycleTogether.RoutesSubscriber
{
    public class Subscription
    {
        private readonly IMapper _mapper;
        private readonly IRouteRepository _routes;
        private readonly IUserRepository _users;
        public Subscription(IRouteRepository routes, IMapper mapper, IUserRepository users)
        {
            _mapper = mapper;
            _routes = routes;
            _users = users;
        }
        public void AddToSubscribed(string email, RouteWeb route)
        {
            var subscribeTo = _mapper.Map<Route>(route);
            _routes.Subscribe(email, subscribeTo);
        }

        public bool Unsubscribe(string email, RouteWeb route)
        {
            var routeToUnsubscribeFrom = _mapper.Map<Route>(route);
            _routes.Unsubscribe(email, routeToUnsubscribeFrom);
            return false;
        }

        public bool IsSuitable(string email, RouteWeb route)
        {
            var user = _users.GetByEmail(email);
            if (RequirementsHasMatch(user, route))
            {
                return true;
            }

            return false;
        }

        private bool RequirementsHasMatch(User user, RouteWeb route)
        {
            if (user.Endurance == route.Endurance &&
                user.Terrain == route.Terrain &&
                user.Difficulty == route.Difficulty)
            {
                return true;
            }
            return false;
        }
    }
}
