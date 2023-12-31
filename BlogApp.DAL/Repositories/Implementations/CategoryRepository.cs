﻿using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;

namespace BlogApp.DAL.Repositories.Implementations
{
	public class CategoryRepository:Repository<Category>,ICategoryRepository 
	{
        public CategoryRepository(AppDbContext context):base(context) { }
    }
}
