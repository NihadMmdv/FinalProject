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
    public class FuelConfiguration :IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .IsUnicode()
                .HasMaxLength(40);
			builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
