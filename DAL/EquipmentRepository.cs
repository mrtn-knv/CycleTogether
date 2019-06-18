using DAL.Contracts;
using DAL.Models;
using System;
using DAL.Data;

namespace DAL
{
    public class EquipmentRepository : DbRepository<EquipmentEntry>, IEquipmentsRepository
    {        
        public EquipmentRepository(CycleTogetherDbContext context) : base(context)
        {
            //context.Add(new EquipmentEntry {Id = Guid.NewGuid(), Name = "Helmet"});
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Knee pads" });
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Naxers" });
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Wrist guards" });
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Torch" });
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Spare tyres" });
            //context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Raincoat" });
        }
    }
}
