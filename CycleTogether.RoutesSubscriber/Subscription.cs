using System;
using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using WebModels;
using System.Collections.Generic;
using CycleTogether.Enums;
using System.Linq;

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
        public void AddMail(string email, RouteWeb route)
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
            var currentRoute = _mapper.Map<Route>(route);
            if (RequirementsHasMatch(user, currentRoute))
            {
                return true;
            }

            return false;
        }

        private bool RequirementsHasMatch(User user, Route route)
        {
            RequirementsMatcher userRequirements = new RequirementsMatcher(user.Terrain, user.Difficulty, user.Endurance, user.Equipments);
            RequirementsMatcher routeReruirements = new RequirementsMatcher(route.Terrain, route.Difficulty, route.Endurance, route.Equipments);

            if (userRequirements.Equals(routeReruirements))
            {
                return true;
            }

            return false;
        }


    }
}
