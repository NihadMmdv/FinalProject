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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Author).IsRequired();
            builder.Property(x =>x.Comment).IsRequired();
            builder.Property(x => x.CreatedAt)
                 .HasColumnType("datetime2")
				 .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
