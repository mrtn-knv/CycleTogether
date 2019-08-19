using CycleTogether.Contracts;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CycleTogether.RoutesSubscriber
{
    public class Subscription : ISubscription
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

        public List<string> SubscribedEmails(string routeId)
        {
            var route = _routes.GetById(Guid.Parse(routeId));
            return route.UserRoutes.Select(ur => _users.GetById(ur.UserId).Email).ToList();

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
