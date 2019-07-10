using DAL.Contracts;
using DAL.Models;
using System;
using DAL.Data;

namespace DAL
{
    public class EquipmentRepository : DbRepository<EquipmentEntry>, IEquipmentsRepository
    {        
        public EquipmentRepository(CycleTogetherDbContext context) : base(context) { }
    }
}
