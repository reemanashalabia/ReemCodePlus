using System;
using System.Net;
using CodePulse.Models.Domain;

namespace CodePulse.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage image);
        Task<IEnumerable<BlogImage>> GetAll();
    }
}