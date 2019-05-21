using System.Collections.Generic;
using DAL.Contracts;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class RoutesRepository : Repository<Route>, IRouteRepository
    {
        public RoutesRepository()
        {

        }

        public void Subscribe(string email, Route route)
        {
            route.SubscribedMails.Add(email);
        }

        public void Unsubscribe(string email, Route route)
        {
            route.SubscribedMails.Remove(email);
        }
    }
}
