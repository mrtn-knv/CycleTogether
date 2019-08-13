using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntry>
    {
        public void Configure(EntityTypeBuilder<UserEntry> builder)
        {
            builder.HasMany(u => u.Routes)
                   .WithOne(r => r.User)
                   .HasForeignKey(u => u.UserId);
        }
    }
}
