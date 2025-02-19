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
    public class UserPricingConfiguration :IEntityTypeConfiguration<UserPricing>
    {
        public void Configure(EntityTypeBuilder<UserPricing> builder)
        {
            builder.Property(x => x.Name).IsRequired()
               .IsUnicode()
                .HasMaxLength(30);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
