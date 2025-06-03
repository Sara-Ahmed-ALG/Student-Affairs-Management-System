using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface ICourseService
    {
        Task<ResponseService<Course>> CreateCourseAsync(CreateCourse dto, string professorId);
        Task<ResponseService<IEnumerable<GetCourse>>> GetAllCoursesAsync();
        Task<ResponseService<bool>> DeleteCourseAsync(Guid id);
    }
}
