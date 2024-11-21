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

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
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


            };
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
                Id = blogPost.Id
            };
            return Ok(response);
        }
        // Get : /api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogposts = await blogPostRepository.GetAllAsync();
            // map domain model to dto
            var response = new List<BlogPost>();
            foreach (var blogPost in blogposts)
            {
                response.Add(new BlogPost()
                {
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,

                    PublishedDate = blogPost.PublishedDate,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Id = blogPost.Id
                });
            }
            return Ok(response);
        }
        // // Get: /api/categoroies/{id}
        // [HttpGet]
        // [Route("{id:Guid}")]
        // public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        // {
        //     var exisetingCategory = await categoryRepository.GetById(id);
        //     if (exisetingCategory is null)
        //     {
        //         return NotFound();
        //     }
        //     var response = new CategoryDto()
        //     {
        //         Id = exisetingCategory.Id,
        //         Name = exisetingCategory.Name,
        //         UrlHandle = exisetingCategory.UrlHandle
        //     };
        //     return Ok(response);
        // }
        // //PUT : /api/categories/{id}
        // [HttpPut]
        // [Route("{id:Guid}")]
        // public async Task<IActionResult> EditCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDTO request)
        // {
        //     // convert from dyo to domain model

        //     var category = new Category()
        //     {
        //         Id = id,
        //         Name = request.Name,
        //         UrlHandle = request.UrlHandle
        //     };
        //     category = await categoryRepository.UpdateAsync(category);
        //     if (category is null)
        //     {
        //         return NotFound();
        //     }
        //     // map domain model to dto 
        //     var response = new CategoryDto()
        //     {
        //         Id = category.Id,
        //         Name = category.Name,
        //         UrlHandle = category.UrlHandle
        //     };
        //     return Ok(response);

        // }
        // // Delete : /api/categories/{id}
        // [HttpDelete]
        // [Route("{id:Guid}")]
        // public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        // {
        //     var category = await categoryRepository.DeleteAsync(id);
        //     if (category is null)
        //     {
        //         return NotFound();
        //     }
        //     // map domain model to dto 
        //     var response = new CategoryDto()
        //     {
        //         Id = category.Id,
        //         Name = category.Name,
        //         UrlHandle = category.UrlHandle
        //     };
        //     return Ok(response);
        // }


    }
}

