using DAL.Contracts;
using DAL.Models;
using System;
using System.Linq;


namespace DAL
{
    public class RoutesRepository : Repository<RouteEntry>, IRouteRepository
    {
        public RoutesRepository()
        {

        }

        public void AddPicture(Guid routeId, PictureEntry image)
        {
            context.FirstOrDefault(r => r.Id == routeId).Images.Add(image);
        }

        public void Subscribe(string email, RouteEntry route)
        {
            route.SubscribedMails.Add(email);
        }

        public void Unsubscribe(string email, RouteEntry route)
        {
            route.SubscribedMails.Remove(email);
        }
    }
}
