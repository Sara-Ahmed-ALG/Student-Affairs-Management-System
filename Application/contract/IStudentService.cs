using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IStudentService

    {
        Task<bool> CreateStudent(RegisterUser student);
        Task<bool> UpdateStudent(string id, EditUser student);
        Task<bool> DeleteStudent(string Id);
        Task<GetUser> GetStudent(string id);
        Task<IEnumerable<GetUser>> GetListStudent();


    }
}
