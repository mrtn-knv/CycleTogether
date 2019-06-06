﻿using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IRouteRepository : IRepository<RouteEntry>
    {
        void Subscribe(string email, RouteEntry route);
        void Unsubscribe(string email, RouteEntry route);
        void AddPicture(Guid routeId, PictureEntry image);
    }
}
