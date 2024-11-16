using System;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories.Interface
{
	public interface ICategoryRepository
	{
		Task<Category> CreateAsync(Category category);
		Task<IEnumerable<Category>> GetAllAsync();
	}
}

