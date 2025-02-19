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
    public class AboutTextConfiguration :IEntityTypeConfiguration<AboutText>
    {
        public void Configure(EntityTypeBuilder<AboutText> builder)
        {
            builder.Property(x => x.Title).IsRequired()
                .HasMaxLength(20);
			builder.Property(x => x.SubTitle).IsRequired()
				.HasMaxLength(50);
			builder.Property(x => x.Text).IsRequired()
				.HasMaxLength(500);
			builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
