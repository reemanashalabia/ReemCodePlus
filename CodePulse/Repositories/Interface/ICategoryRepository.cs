using System;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);
		Task<IEnumerable<Category>> GetAllAsync(string? query = null);
		// return category or null
		Task<Category?> GetById(Guid Id);

		Task<Category?> UpdateAsync(Category category);
		Task<Category?> DeleteAsync(Guid Id);
	}
}

