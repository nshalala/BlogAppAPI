﻿namespace BlogApp.Core.Entities;

public class Blog:BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedTime { get; set; }
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public IEnumerable<BlogCategory>? BlogCategories { get; set; }
}