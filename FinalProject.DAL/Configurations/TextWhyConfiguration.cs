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
	public class TextWhyConfiguration: IEntityTypeConfiguration<TextWhy>
	{
		public void Configure(EntityTypeBuilder<TextWhy> builder)
		{
			builder.Property(x => x.Title).IsRequired()
				.HasMaxLength(20);
			builder.Property(x => x.Text).IsRequired()
				.HasMaxLength(100);
			builder.Property(x => x.Icon).IsRequired();
			builder.Property(x => x.SettingId).IsRequired();
			builder.Property(x => x.CreatedAt)
			   .HasColumnType("datetime2")
			   .HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
