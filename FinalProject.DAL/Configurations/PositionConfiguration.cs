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
    public class PositionConfiguration :IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(x => x.Name).IsRequired()
               .IsUnicode()
                .HasMaxLength(30);
            builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
