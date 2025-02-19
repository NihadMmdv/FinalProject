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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Subject).IsRequired();
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.CreatedAt)
                 .HasColumnType("datetime2")
				 .HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
