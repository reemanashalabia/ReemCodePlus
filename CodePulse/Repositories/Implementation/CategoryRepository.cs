using System;
using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Repositories.Interface;

namespace CodePulse.Repositories.Implementation
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
		{
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}

