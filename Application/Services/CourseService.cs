using Application.contract;
using AutoMapper;
using Azure;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Course;
using BackEnd.Core.Interfaces;
using BackEnd.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CourseService: ICourseService
    {
        private readonly IUniteOfWork _uniteOfWork;
       
        private readonly IMapper _map;
        public CourseService(IUniteOfWork uniteOfWork,  IMapper map)
        {
            _uniteOfWork = uniteOfWork;
           
            _map = map;
        }

        public async Task<ResponseService<Course>> CreateCourseAsync(CreateCourse dto, string professorId)
        {

            var response = new ResponseService<Course>();
            try
            {
                var professor = await _uniteOfWork.Professors.GetFirstOrDefaultAsync(p=> p.AppUserId ==professorId);
                if (professor == null)
                {
                    response.Success = false;
                    response.Message = "Professor not found.";
                    return response;
                }

                var course = _map.Map<Course>(dto);
                course.ProfessorId = professor.Id;

                _uniteOfWork.Courses.Add(course);
                int result = _uniteOfWork.save();

                if (result > 0)
                {
                    response.Message = "Course created successfully.";
                   
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to create course.";
                }
            }
            catch (FormatException)
            {
                response.Success = false;
                response.Message = "Invalid professor ID format.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;


        }

        public async Task<ResponseService<IEnumerable<GetCourse>>> GetAllCoursesAsync()
        {
            var response = new ResponseService<IEnumerable<GetCourse>>();
            var courses = await _uniteOfWork.Courses.GetAll().Include(c => c.Professor)

                .Select(c => new GetCourse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    ProfessorName = c.Professor.UserName
                })
                .ToListAsync();

            if (courses!=null)
            { response.Message = "course fetched successfully"; response.Data = courses; }
            else
            { response.Message = "failed to fetch the course"; response.Success = false; }
            return response;
        }

        public async Task<ResponseService<bool>> DeleteCourseAsync(Guid id)
        {
            int r = 0;
            var response = new ResponseService<bool>();
            var course = await _uniteOfWork.Courses.GetByIdAsync(id);
            if (course == null)
            { response.Message = " course not found "; response.Success = false; }
            else
            {
                _uniteOfWork.Courses.Delete(course);
                r= _uniteOfWork.save();

            }
            if (r > 0) {  response.Message = "course deleted successfully";   }
            else { response.Message = "failed to delete the course "; response.Success = false; }
            return response;

        }




    }
}
