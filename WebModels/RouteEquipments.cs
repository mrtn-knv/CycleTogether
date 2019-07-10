using System;
using System.Collections.Generic;
using System.Text;

namespace WebModels
{
    public class RouteEquipments
    {
        public Guid Id { get; set; }
        public Guid RouteId { get; set; }
        public Guid EquipmentId { get; set; }
    }
}
