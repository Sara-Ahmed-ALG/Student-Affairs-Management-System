using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IUserService
    {
        Task<ResponseService<RegisterUser>> RegisterUser(RegisterUser dto);
        Task<ResponseService<AppUser>> UpdateUser(string id, EditUser dto);
        Task<bool> DeleteUser(string Id);
        Task<ResponseService<GetUser>> GetUser(string id);
        Task<ResponseService<IEnumerable<GetUser>>> GetListUser();

    }
}
