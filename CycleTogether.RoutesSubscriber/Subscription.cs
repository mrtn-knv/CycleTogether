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
        private readonly IUnitOfWork _db;
        
        public Subscription(IUnitOfWork db)
        {
            _db = db;
        }
        public bool Subscribe(Guid userId, Guid routeId)
        {
            var subscribed = new UserRouteEntry { RouteId = routeId, UserId = userId };
            if (Requirements.Match(_db.Users.GetById(userId), _db.Routes.GetById(routeId)) &&
                                   _db.UserRoutes.Exists(subscribed) == false)
            {
                _db.UserRoutes.Create(subscribed);
                return true;
            }
            return false;
        }

        public bool Unsubscribe(UserRouteEntry userFromRoute)
        {
            if (IsSubscribed(userFromRoute))
            {
                _db.UserRoutes.Delete(userFromRoute.Id);
                return true;
            }
            return false;
        }

        public List<string> SubscribedEmails(string routeId)
        {
            var route = _db.Routes.GetById(Guid.Parse(routeId));
            return route.UserRoutes.Select(ur => _db.Users.GetById(ur.UserId).Email).ToList();

        }

        private bool IsSubscribed(UserRouteEntry userFromRoute)
        {
            if (_db.UserRoutes.GetAll()
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
