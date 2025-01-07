using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePulse.Data;
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Repositories.Implementation;
using CodePulse.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            //Map Dto to domain model
            var category = new Category()
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            await categoryRepository.CreateAsync(category);
            // map from domain model to dto
            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }
        // Get : /api/categories?query=html
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? query, [FromQuery] string? sortBy, [FromQuery] string? sortDirection)
        {
            var categories = await categoryRepository.GetAllAsync(query, sortBy, sortDirection);
            // map domain model to dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);
        }
        // Get: /api/categoroies/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var exisetingCategory = await categoryRepository.GetById(id);
            if (exisetingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto()
            {
                Id = exisetingCategory.Id,
                Name = exisetingCategory.Name,
                UrlHandle = exisetingCategory.UrlHandle
            };
            return Ok(response);
        }
        //PUT : /api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> EditCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDTO request)
        {
            // convert from dyo to domain model

            var category = new Category()
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            category = await categoryRepository.UpdateAsync(category);
            if (category is null)
            {
                return NotFound();
            }
            // map domain model to dto 
            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }
        // Delete : /api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            // map domain model to dto 
            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }


    }
}

