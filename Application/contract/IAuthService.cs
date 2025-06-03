using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.DTOs.UserDTO;
using BackEnd.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IAuthService
    {
       Task<UserTokens> LoginAsync(LoginDto loginDto);
        Task<ResponseService<AppUser>> RegisterStudent(RegisterUser dto);
    }
}
