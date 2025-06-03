using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Material;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Interfaces
{
    public interface IMaterialRepo :IGenericRepo<CourseMaterial>
    {
       
    }
}
