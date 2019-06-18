using DAL.Contracts;
using DAL.Data;
using DAL.Models;
using System;
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

        public void Subscribe(string id, RouteEntry route)
        {
            //TODO
            route.UserRoutes.Add(new UserRoute {RouteId = route.Id, UserId = Guid.Parse(id) });
        }

        public void Unsubscribe(string id, RouteEntry route)
        {
            //TODO
            route.UserRoutes.Where(ur => ur.Id == route.Id && ur.UserId == Guid.Parse(id));
        }
    }
}
