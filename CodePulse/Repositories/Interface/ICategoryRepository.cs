using System;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);
		Task<IEnumerable<Category>> GetAllAsync(string? query = null, string? sortBy = null,
		 string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100);
		// return category or null
		Task<Category?> GetById(Guid Id);

		Task<Category?> UpdateAsync(Category category);
		Task<Category?> DeleteAsync(Guid Id);

		Task<int> CategoriesCount();
	}
}

