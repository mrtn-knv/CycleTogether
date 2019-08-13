using DAL.Contracts;
using DAL.Data;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class UserRoutesRepository : DbRepository<UserRouteEntry>, IUserRouteRepository
    {
        public UserRoutesRepository(CycleTogetherDbContext context) : base(context)
        {

        }

        public bool Exists(UserRouteEntry userRoute)
        {
            return GetAll().Any(ur => ur.UserId == userRoute.UserId && ur.RouteId == userRoute.RouteId);           
        }
    }
}
