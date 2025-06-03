using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Interfaces
{
    public interface IUserRepo

    {
       
            Task<IEnumerable<AppUser>> GetAllAsync();
            Task<AppUser> GetByIdAsync(string id);
            Task<AppUser> GetByEmailAsync(string email);
            Task<AppUser> CreateAsync(RegisterUser user);
            Task<bool> UpdateAsync(string id,EditUser user);
            Task<bool> Delete(string id);
       
    }
}
