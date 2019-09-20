using DAL.Contracts;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CycleTogetherDbContext _context;

        public UnitOfWork(IEquipmentsRepository equipments,
                          IImageRepository images,
                          IRouteEquipmentRepositoy routeEquipments,
                          IRouteRepository routes,
                          IUserEquipmentRepository userEquipments,
                          IUserRepository users,
                          IUserRouteRepository userRoutes,
                          CycleTogetherDbContext context)
        {
            Equipments = equipments;
            Images = images;
            RouteEquipments = routeEquipments;
            Routes = routes;
            UserEquipments = userEquipments;
            Users = users;
            UserRoutes = userRoutes;
            _context = context;
        }

        public IEquipmentsRepository Equipments { get; }

        public IImageRepository Images { get; }

        public IRouteEquipmentRepositoy RouteEquipments { get; }

        public IRouteRepository Routes { get; }

        public IUserEquipmentRepository UserEquipments { get; }

        public IUserRepository Users { get; }

        public IUserRouteRepository UserRoutes { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
