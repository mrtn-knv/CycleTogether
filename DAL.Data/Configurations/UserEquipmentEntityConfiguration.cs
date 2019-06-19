using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data.Configurations
{
    public class UserEquipmentEntityConfiguration : IEntityTypeConfiguration<UserEquipmentEntry>
    {
        public void Configure(EntityTypeBuilder<UserEquipmentEntry> builder)
        {
            builder.HasKey(ue => new { ue.UserId, ue.EquipmentId });
            builder.HasOne(ue => ue.Equipment)
                   .WithMany(e => e.UserEquipments)
                   .HasForeignKey(ue => ue.EquipmentId);
            builder.HasOne(ue => ue.User)
                   .WithMany(u => u.UserEquipments)
                   .HasForeignKey(ue => ue.UserId);
        }
    }
}
