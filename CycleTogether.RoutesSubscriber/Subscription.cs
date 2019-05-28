using System;
using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using WebModels;

namespace CycleTogether.RoutesSubscriber
{
    public class Subscription
    {
        private readonly IMapper _mapper;
        private readonly IRouteRepository _routes;
        public Subscription(IRouteRepository routes, IMapper mapper)
        {
            _mapper = mapper;
            _routes = routes;
        }
        public bool AddToSubscribed(string email, RouteWeb route)
        {
            var subscribeTo = _mapper.Map<Route>(route);
            _routes.Subscribe(email, subscribeTo);
            return true;
        }

        public bool Unsubscribe(string email, RouteWeb route)
        {
            var routeToUnsubscribeFrom = _mapper.Map<Route>(route);
            _routes.Unsubscribe(email, routeToUnsubscribeFrom);
            return false;
        }
    }
}
