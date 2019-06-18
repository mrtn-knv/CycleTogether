using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations
{
    public class UserRouteEntityConfiguration : IEntityTypeConfiguration<UserRoute>
    {
        public void Configure(EntityTypeBuilder<UserRoute> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RouteId });
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoutes)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ur => ur.Route)
                   .WithMany(r => r.UserRoutes)
                   .HasForeignKey(ur => ur.RouteId);
        }
    }
}
