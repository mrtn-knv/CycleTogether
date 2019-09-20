using DAL.Data.Configurations;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class CycleTogetherDbContext : DbContext
    {
        public CycleTogetherDbContext(DbContextOptions<CycleTogetherDbContext> options) : base(options)
        {
                
        }

        public DbSet<UserEntry> Users { get; set; }
        public DbSet<EquipmentEntry> Equipments { get; set; }
        public DbSet<PictureEntry> Pictures { get; set; }
        public DbSet<RouteEntry> Routes { get; set; }
        public DbSet<RouteEquipmentEntry> RouteEquipments { get; set; }
        public DbSet<UserEquipmentEntry> UserEquipments { get; set; }
        public DbSet<UserRouteEntry> UserRoutes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RouteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RouteEquipmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEquipmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRouteEntityConfiguration());
        }
    }
}
