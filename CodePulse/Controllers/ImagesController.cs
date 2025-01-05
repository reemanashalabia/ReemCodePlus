
using CodePulse.Models.Domain;
using CodePulse.Models.DTO;
using CodePulse.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodePulse.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // Post: api/Images/
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName,
        [FromForm] string title)
        {
            // validate file
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage()
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = file.FileName,
                    Title = title,
                    DateCreated = DateTime.Now

                };
                blogImage = await imageRepository.Upload(file, blogImage);
                //convert domain model to dto 
                var response = new BlogImageDto()
                {
                    Id = blogImage.Id,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url,

                };

                return Ok(response);
            }
            return BadRequest(ModelState);


        }
        // get all images /api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            // call images repo to get all images
            var images = await imageRepository.GetAll();
            // convert model to dto 
            var response = new List<BlogImageDto>();
            foreach (var blogImage in images)
            {
                response.Add(new BlogImageDto()
                {
                    Id = blogImage.Id,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url,
                });

            }
            return Ok(response);

        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "UnSupported File Format");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size Can not be more 10 mb");

            }

        }
    }
}