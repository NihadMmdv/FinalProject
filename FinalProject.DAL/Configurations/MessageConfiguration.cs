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
	public class MessageConfiguration: IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.Property(x => x.FirstName).IsRequired()
				.HasMaxLength(20);
			builder.Property(x => x.Email).IsRequired()
				.HasMaxLength(30);
			builder.Property(x => x.Phone).IsRequired()
			.HasMaxLength(20);
			builder.Property(x => x.Address).IsRequired()
			.HasMaxLength(100);
			builder.Property(x => x.Text).IsRequired()
			.HasMaxLength(100);
			builder.Property(x => x.CreatedAt)
			   .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
