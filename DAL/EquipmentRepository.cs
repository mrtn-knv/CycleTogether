using DAL.Contracts;
using DAL.Models;
using System;

namespace DAL
{
    public class EquipmentRepository : Repository<EquipmentEntry>, IEquipmentsRepository
    {        
        public EquipmentRepository()
        {
            context.Add(new EquipmentEntry {Id = Guid.NewGuid(), Name = "Helmet"});
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Knee pads" });
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Naxers" });
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Wrist guards" });
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Torch" });
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Spare tyres" });
            context.Add(new EquipmentEntry { Id = Guid.NewGuid(), Name = "Raincoat" });
        }
    }
}
