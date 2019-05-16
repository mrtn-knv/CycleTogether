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

        public RouteManager(IRouteRepository routes, IMapper mapper)
        {
            _routes = routes;
            _mapper = mapper;
        }

        public RouteWeb Create(RouteWeb route)
        {
            var routeNew = _mapper.Map<Route>(route);
            _routes.Create(routeNew);
            return _mapper.Map<RouteWeb>(routeNew);

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
            var current = _mapper.Map<Route>(route);
            _routes.Edit(current);
            return _mapper.Map<RouteWeb>(current);
        }
    }
}
