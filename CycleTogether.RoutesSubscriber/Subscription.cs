using DAL.Contracts;
using DAL.Models;
using System;
using System.Linq;

namespace CycleTogether.RoutesSubscriber
{
    public class Subscription
    {
        private readonly IRouteRepository _routes;
        private readonly IUserRepository _users;
        private readonly IUserRouteRepository _subscriber;

        public Subscription(IRouteRepository routes, IUserRepository users, IUserRouteRepository subscriber)
        {
            _routes = routes;
            _users = users;
            _subscriber = subscriber;
        }
        public bool Subscribe(Guid userId, Guid routeId)
        {
            var subscribed = new UserRouteEntry { RouteId = routeId, UserId = userId };
            if (Requirements.Match(_users.GetById(userId), _routes.GetById(routeId)) &&
                                   _subscriber.Exists(subscribed) == false)
            {
                _subscriber.Create(subscribed);
                return true;
            }
            return false;
        }

        public bool Unsubscribe(UserRouteEntry userFromRoute)
        {
            if (IsSubscribed(userFromRoute))
            {
                _subscriber.Delete(userFromRoute.Id);
                return true;
            }
            return false;
        }

        private bool IsSubscribed(UserRouteEntry userFromRoute)
        {
            if (_subscriber.GetAll()
                .FirstOrDefault(ur => ur.RouteId == userFromRoute.RouteId
                && ur.UserId == userFromRoute.UserId)
                != null)
            {
                return true;
            }
            return false;
        }

    }
}
