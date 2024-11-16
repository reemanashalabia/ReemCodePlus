using System;
namespace CodePulse.Models.DTO
{
    public class UpdateCategoryRequestDTO
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; } // slug
    }
}

