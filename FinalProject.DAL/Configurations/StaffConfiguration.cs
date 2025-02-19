using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Configurations
{
    public class StaffConfiguration :IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(x => x.FullName).IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.Info).IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.PositionId).IsRequired();
            builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
