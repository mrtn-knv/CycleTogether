using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IRouteRepository : IRepository<RouteEntry>
    {
        void Subscribe(string userId, RouteEntry route);
        void Unsubscribe(string userId, RouteEntry route);
        void AddPicture(Guid routeId, PictureEntry image);
    }
}
