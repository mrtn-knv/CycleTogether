﻿using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using WebModels;
using CycleTogether.RoutesDifficultyManager;
using CycleTogether.RoutesSubscriber;
using CycleTogether.Contracts;

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

        public RouteWeb Create(RouteWeb route, string email)
        {
            return Save(route, email);
        }

        public IEnumerable<RouteWeb> GetAll()
        {
            var all = _routes.GetAll();
            var routes = MapAll(all);
            return routes;
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
            if (_subscription.IsSuitable(email, route)) 
            {
               _subscription.AddMail(email,route);
                return true;
            }
            return false;
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

        private RouteWeb Save(RouteWeb route, string email)
        {
            route.Difficulty = _difficulty.DifficultyLevel(route);
            var routeNew = _mapper.Map<Route>(route);
            var currentUser = _users.GetByEmail(email);
            routeNew.CreatedBy = currentUser.Id;
            routeNew.Equipments = route.Equipments;
            routeNew.SubscribedMails.Add(currentUser.Email);
            currentUser.Routes.Add(routeNew);            
            _routes.Create(routeNew);
            return _mapper.Map<RouteWeb>(routeNew);
        }

        private RouteWeb SaveUpdated(RouteWeb route)
        {
            var current = _mapper.Map<Route>(route);
            _routes.Edit(current);
            return _mapper.Map<RouteWeb>(current);
        }

        public void Unsubscribe(string email, RouteWeb route)
        {
            if (route.SubscribedMails.Contains(email))
            {
                _subscription.Unsubscribe(email, route);
            }
        }

    }
}
