using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations
{
    public class RouteEquipmentEntityConfiguration : IEntityTypeConfiguration<RouteEquipment>
    {
        public void Configure(EntityTypeBuilder<RouteEquipment> builder)
        {
            builder.HasKey(re => new { re.EquipmentId, re.RouteId });
            builder.HasOne(re => re.Route)
                   .WithMany(r => r.RouteEquipments)
                   .HasForeignKey(re => re.RouteId);
            builder.HasOne(re => re.Equipment)
                   .WithMany(e => e.RouteEquipments)
                   .HasForeignKey(re => re.EquipmentId);
        }
    }
}
