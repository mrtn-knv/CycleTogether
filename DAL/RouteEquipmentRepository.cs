using DAL.Data;
using DAL.Models;
using DAL.Contracts;


namespace DAL
{
    public class RouteEquipmentRepository : DbRepository<RouteEquipmentEntry>, IRouteEquipmentRepositoy
    {
        public RouteEquipmentRepository(CycleTogetherDbContext context) : base(context)
        {

        }
    }
}
