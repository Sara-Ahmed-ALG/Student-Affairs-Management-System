using BackEnd.Core.DTOs.Material;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IMaterialService
    {

        Task<ResponseService<bool>> UploadAsync(MaterialUploadDto dto);


    }
}
