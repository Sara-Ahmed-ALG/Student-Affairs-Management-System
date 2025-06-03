using Application.contract;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Material;
using BackEnd.Core.Interfaces;
using BackEnd.EF.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MaterialService : IMaterialService
    {
      
            private readonly IUniteOfWork  _unitOfWork;

            public MaterialService(IUniteOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

        public async Task<ResponseService<bool>> UploadAsync(MaterialUploadDto dto)
        {
            var response= new ResponseService<bool>();
            var relativePath = SaveFileToDisk(dto.File);

            var material = new CourseMaterial
            {
                Id = Guid.NewGuid(),
                FileName = dto.FileName,
                FileType = dto.FileType,    
                CourseId = dto.CourseId,
                FilePath = relativePath,
                UploadedAt = DateTime.Now
                
            };

            _unitOfWork.CourseMaterial.Add(material);
            int r=  _unitOfWork.save();

            if (r > 0) {
                response.Message = "File Uploaded Successfully";
            }
            else
            {
                response.Success =false;
                response.Message = "Failed to upload File";
            }
            return response;


        }

            private string SaveFileToDisk(IFormFile file)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "materials");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return Path.Combine("materials", uniqueFileName);
            }
        }


    }

