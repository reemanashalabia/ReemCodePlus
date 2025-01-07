using System;
using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category?> DeleteAsync(Guid Id)
        {
            var exisetingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (exisetingCategory is null)
            {
                return null;
            }
            dbContext.Categories.Remove(exisetingCategory);
            await dbContext.SaveChangesAsync();
            return exisetingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string? query = null)
        {
            var categories = dbContext.Categories.AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }

            // sorting 

            //pagination

            return await categories.ToListAsync();


            //return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid Id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var exisetingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (exisetingCategory is not null)
            {
                dbContext.Entry(exisetingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}

