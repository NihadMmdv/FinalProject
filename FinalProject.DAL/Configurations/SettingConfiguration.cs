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
    public class SettingConfiguration :IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(x => x.Address).IsRequired()
                .HasMaxLength(40);
			builder.Property(x => x.Phone1).IsRequired();
			builder.Property(x => x.Phone2).IsRequired();
			builder.Property(x => x.PhotoWhy).IsRequired();
			builder.Property(x => x.PhotoWhyUrl).IsRequired();
			builder.Property(x => x.Logo).IsRequired();
			builder.Property(x => x.LogoUrl).IsRequired();
			builder.Property(x => x.Email).IsRequired();
			builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
