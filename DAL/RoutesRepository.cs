using System.Collections.Generic;
using DAL.Contracts;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class RoutesRepository : Repository<RouteEntry>, IRouteRepository
    {
        public RoutesRepository()
        {

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
