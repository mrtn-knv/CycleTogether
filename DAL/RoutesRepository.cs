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
       
    }
}
