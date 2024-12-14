using System;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        // return BlogPost or null
        Task<BlogPost?> GetById(Guid Id);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid Id);

        Task<BlogPost?> GetByurlHandle(string UrlHandle);

    }
}

