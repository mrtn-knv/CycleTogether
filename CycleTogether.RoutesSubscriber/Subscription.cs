﻿using DAL.Contracts;
using DAL.Models;
using WebModels;


namespace CycleTogether.RoutesSubscriber
{
    public class Subscription
    {
        private readonly IRouteRepository _routes;
        private readonly IUserRepository _users;
        public Subscription(IRouteRepository routes, IUserRepository users)
        {
            _routes = routes;
            _users = users;
        }
        public bool AddMail(string email, Route route)
        {
            if (IsSuitable(email, route))
            {
                var current = _routes.GetById(route.Id);
                _routes.Subscribe(email, current);
                return true;
            }
            return false;
        }

        public bool Unsubscribe(string email, RouteEntry route)
        {
            _routes.Unsubscribe(email, route);
            return false;
        }

        public bool IsSuitable(string email, Route route)
        {
            var user = _users.GetByEmail(email);
            var currentRoute = _routes.GetById(route.Id);
            if (RequirementsHasMatch(user, currentRoute))
            {
                return true;
            }

            return false;
        }

        private bool RequirementsHasMatch(UserEntry user, RouteEntry route)
        {
            RequirementsMatcher userRequirements = new RequirementsMatcher(user.Terrain, user.Difficulty, user.Endurance, user.Equipments);
            RequirementsMatcher routeReruirements = new RequirementsMatcher(route.Terrain, route.Difficulty, route.Endurance, route.Equipments);

            return userRequirements.Equals(routeReruirements);           
        }


    }
}
