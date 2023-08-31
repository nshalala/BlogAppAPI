﻿using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;

namespace BlogApp.DAL.Repositories.Implementations;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
	public BlogRepository(AppDbContext context) : base(context) { }
}
