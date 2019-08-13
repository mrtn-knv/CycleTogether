using DAL.Contracts;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DAL
{
    public class RoutesRepository : DbRepository<RouteEntry>, IRouteRepository
    {
        public RoutesRepository(CycleTogetherDbContext context) : base(context)
        {

        }

        public void AddPicture(Guid routeId, PictureEntry image)
        {
            
            GetAll().FirstOrDefault(r => r.Id == routeId).Pictures.Add(image);
        }

        public IEnumerable<RouteEntry> AllByUser(Guid userId)
        {
            return GetAll().Where(route => route.UserId == userId).ToList();
        }
    }
}
