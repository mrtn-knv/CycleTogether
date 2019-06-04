using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentsRepository
    {        
        public EquipmentRepository()
        {
            context.Add(new Equipment {Id = Guid.NewGuid(), Name = "Helmet"});
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Knee pads" });
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Naxers" });
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Wrist guards" });
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Torch" });
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Spare tyres" });
            context.Add(new Equipment { Id = Guid.NewGuid(), Name = "Raincoat" });
        }
    }
}
