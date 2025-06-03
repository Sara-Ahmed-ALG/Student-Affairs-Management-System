using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.DTOs.Material
{
    public class MaterialUploadDto
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Guid CourseId { get; set; }
        public IFormFile File { get; set; }

    }
}
