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
    }
}
