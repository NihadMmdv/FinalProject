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
    public class CarImageConfiguration :IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.Property(x => x.CarId).IsRequired();
			builder.Property(x => x.Image).IsRequired();
			builder.Property(x => x.ImageUrl).IsRequired();
			builder.Property(x => x.isMain).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
