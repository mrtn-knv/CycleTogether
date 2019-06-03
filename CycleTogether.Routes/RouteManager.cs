using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.RoutesDifficultyManager;
using CycleTogether.RoutesSubscriber;
using CycleTogether.Contracts;
using System.Linq;

namespace CycleTogether.Routes
{
    public class RouteManager : IRouteManager
    {
        private readonly IRouteRepository _routes;
        private readonly IMapper _mapper;
        private readonly IUserRepository _users;
        private readonly DifficultyCalculator _difficulty;
        private readonly Subscription _subscription;
        private readonly IEquipmentsRepository _equipments;
        

        public RouteManager(IRouteRepository routes,
                            IMapper mapper, 
                            IUserRepository users,
                            DifficultyCalculator difficulty,
                            Subscription subscription,
                            IEquipmentsRepository equipments)
        {
            _routes = routes;
            _mapper = mapper;
            _users = users;
            _difficulty = difficulty;
            _subscription = subscription;
            _equipments = equipments;
        }

        public RouteWeb Create(RouteWeb route, string userId, string email)
        {
            return Save(SetProperties(route, userId, email), userId);
        }

        private Route SetProperties(RouteWeb route, string id, string email)
        {
            var newRoute = _mapper.Map<Route>(route);
            newRoute.Difficulty = _difficulty.DifficultyLevel(route);
            newRoute.CreatedBy = Guid.Parse(id);
            newRoute.Equipments = route.Equipments;
            newRoute.SubscribedMails.Add(email);
            var currentUser = _users.GetById(Guid.Parse(id));
            currentUser.Routes.Add(newRoute);
            return newRoute;
        }

        public IEnumerable<RouteWeb> GetAll()
        {
            return _routes.GetAll().Select(route => _mapper.Map<RouteWeb>(route));            
        }


        public RouteWeb Get(Guid id)
        {
            var route = _routes.GetById(id);
            var found = _mapper.Map<RouteWeb>(route);
            return found;
        }

        public void Remove(Guid id, string userId)
        {
            var current = _routes.GetById(id);
            if (current.CreatedBy.ToString() == userId)
            {
                _routes.Delete(id);
            }            
        }

        public bool Subscribe(string email, RouteWeb route)
        {
            
              return _subscription.AddMail(email, route);

        }
        public RouteWeb Update(RouteWeb route, string id)
        {
            if (id == route.CreatedBy.ToString())
            {
                return SaveUpdated(route);
            }

            return null;
        }
        private IEnumerable<RouteWeb> MapAll(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                yield return _mapper.Map<RouteWeb>(route);
            }
        }

        private RouteWeb Save(Route route, string email)
        {                        
            _routes.Create(route);
            return _mapper.Map<RouteWeb>(route);
        }

        private RouteWeb SaveUpdated(RouteWeb route)
        {
            var current = _mapper.Map<Route>(route);
            _routes.Edit(current);
            return _mapper.Map<RouteWeb>(current);
        }

        public void Unsubscribe(string email, RouteWeb route)
        {
            var current = _routes.GetById(route.Id);
            if (current.SubscribedMails.Contains(email))
            {
                _subscription.Unsubscribe(email, current);
            }
        }

    }
}
