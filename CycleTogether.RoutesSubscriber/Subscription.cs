using DAL.Contracts;
using DAL.Models;
using System;
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
        public bool AddMail(string id, Route route)
        {
            if (IsSuitable(id, route))
            {
                var current = _routes.GetById(route.Id);
                _routes.Subscribe(id, current);
                return true;
            }
            return false;
        }

        public bool Unsubscribe(string email, RouteEntry route)
        {
            _routes.Unsubscribe(email, route);
            return false;
        }

        public bool IsSuitable(string id, Route route)
        {
            var user = _users.GetById(Guid.Parse(id));
            var currentRoute = _routes.GetById(route.Id);
            if (RequirementsHasMatch(user, currentRoute))
            {
                return true;
            }

            return false;
        }

        private bool RequirementsHasMatch(UserEntry user, RouteEntry route)
        {
            //TODO: Modify requirementsMatcher
            RequirementsMatcher userRequirements = new RequirementsMatcher(user.Terrain, user.Difficulty, user.Endurance/*, user.UserEquipments*/);
            RequirementsMatcher routeReruirements = new RequirementsMatcher(route.Terrain, route.Difficulty, route.Endurance/*, route.RouteEquipments*/);

            return userRequirements.Equals(routeReruirements);           
        }


    }
}
