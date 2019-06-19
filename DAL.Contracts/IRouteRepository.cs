using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IRouteRepository : IRepository<RouteEntry>
    {
        void AddPicture(Guid routeId, PictureEntry image);
    }
}
