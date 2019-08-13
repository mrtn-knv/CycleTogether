using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations
{
    public class RouteEntityConfiguration : IEntityTypeConfiguration<RouteEntry>
    {
        public void Configure(EntityTypeBuilder<RouteEntry> builder)
        {
            builder.HasMany(r => r.Pictures)
                   .WithOne(p => p.Route)
                   .HasForeignKey(r => r.RouteId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
