using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Repositories.Implementation;
using CodePulse.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateBlogPostDto request)
        {
            //Map Dto to domain model
            var blogpost = new BlogPost()
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,

                PublishedDate = request.PublishedDate,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()


            };
            foreach (var categoryGuid in request.Categories)
            {
                var exisetingCategory = await categoryRepository.GetById(categoryGuid);
                if (exisetingCategory is not null)
                {
                    blogpost.Categories.Add(exisetingCategory);
                }
            }
            var blogPost = await blogPostRepository.CreateAsync(blogpost);
            // map from domain model to dto
            var response = new BlogPostDto()
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,

                PublishedDate = blogPost.PublishedDate,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Id = blogPost.Id,
                Categories = blogPost.Categories.Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }
        // Get : /api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {

            var blogposts = await blogPostRepository.GetAllAsync();
            // map domain model to dto
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogposts)
            {
                response.Add(new BlogPostDto()
                {
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,

                    PublishedDate = blogPost.PublishedDate,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Id = blogPost.Id,
                    Categories = blogPost.Categories.Select(x => new CategoryDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()

                });
            }
            return Ok(response);
        }
        // Get: /api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            // get blog post frpm repo
            var exisetingBlogPost = await blogPostRepository.GetById(id);
            if (exisetingBlogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDto()
            {
                Author = exisetingBlogPost.Author,
                Content = exisetingBlogPost.Content,
                FeaturedImageUrl = exisetingBlogPost.FeaturedImageUrl,
                IsVisible = exisetingBlogPost.IsVisible,

                PublishedDate = exisetingBlogPost.PublishedDate,
                Title = exisetingBlogPost.Title,
                ShortDescription = exisetingBlogPost.ShortDescription,
                UrlHandle = exisetingBlogPost.UrlHandle,
                Id = exisetingBlogPost.Id,
                Categories = exisetingBlogPost.Categories.Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }
        //PUT : /api/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditBlogPost([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            // convert from dyo to domain model

            var blogPost = new BlogPost()
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,

                PublishedDate = request.PublishedDate,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>(),

            };
            foreach (var categoryGuid in request.Categories)
            {
                var exisetingCategory = await categoryRepository.GetById(categoryGuid);
                if (exisetingCategory is not null)
                {
                    blogPost.Categories.Add(exisetingCategory);
                }
            }
            var updatedblogPost = await blogPostRepository.UpdateAsync(blogPost);
            if (updatedblogPost is null)
            {
                return NotFound();
            }
            // map domain model to dto 
            var response = new BlogPostDto()
            {
                Author = updatedblogPost.Author,
                Content = updatedblogPost.Content,
                FeaturedImageUrl = updatedblogPost.FeaturedImageUrl,
                IsVisible = updatedblogPost.IsVisible,

                PublishedDate = updatedblogPost.PublishedDate,
                Title = updatedblogPost.Title,
                ShortDescription = updatedblogPost.ShortDescription,
                UrlHandle = updatedblogPost.UrlHandle,
                Id = updatedblogPost.Id,
                Categories = updatedblogPost.Categories.Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);

        }
        // Delete : /api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.DeleteAsync(id);
            if (blogPost is null)
            {
                return NotFound();
            }
            // map domain model to dto 
            var response = new BlogPostDto()
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,

                PublishedDate = blogPost.PublishedDate,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Id = blogPost.Id,

            };
            return Ok(response);
        }


    }
}

