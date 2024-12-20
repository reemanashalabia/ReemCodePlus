using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid Id)
        {
            var exisetingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == Id);
            if (exisetingBlogPost is null)
            {
                return null;
            }
            dbContext.BlogPosts.Remove(exisetingBlogPost);
            await dbContext.SaveChangesAsync();
            return exisetingBlogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetById(Guid Id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<BlogPost?> GetByurlHandle(string UrlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == UrlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var exisetingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (exisetingBlogPost is null)
            {
                return null;
            }
            // update blogpost
            dbContext.Entry(exisetingBlogPost).CurrentValues.SetValues(blogPost);
            // update categories
            exisetingBlogPost.Categories = blogPost.Categories;
            await dbContext.SaveChangesAsync();
            return blogPost;
        }
    }
}