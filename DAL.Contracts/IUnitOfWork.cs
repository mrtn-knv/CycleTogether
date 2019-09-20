using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        IEquipmentsRepository Equipments { get; }
        IImageRepository Images { get; }
        IRouteEquipmentRepositoy RouteEquipments { get; }
        IRouteRepository Routes { get; }
        IUserEquipmentRepository UserEquipments { get; }
        IUserRepository Users { get; }
        IUserRouteRepository UserRoutes { get; }
        void SaveChanges();
        void Dispose();
    }
}
