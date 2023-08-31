﻿using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DAL.Configurations
{
	public class BlogConfiguration : IEntityTypeConfiguration<Blog>
	{
		public void Configure(EntityTypeBuilder<Blog> builder)
		{
			builder.Property(b => b.Title)
				.IsRequired()
				.HasMaxLength(255);
			builder.Property(b => b.Description)
				.IsRequired();
			builder.Property(b=>b.CoverImageUrl) 
				.IsRequired();
			builder.Property(b=>b.ViewCount)
				.HasDefaultValue(0);
			builder.Property(b => b.CreatedTime)
				.HasDefaultValue(DateTime.UtcNow);
			builder.HasOne(b => b.AppUser)
				.WithMany(au => au.Blogs)
				.HasForeignKey(b => b.AppUserId);
		}
	}
}
