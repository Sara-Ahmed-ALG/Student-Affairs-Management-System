using Application.contract;
using AutoMapper;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUniteOfWork _unit;
        private readonly IMapper _map;

        public StudentService(IMapper map, IUniteOfWork unit, UserManager<AppUser> userManager)
        {
            _map = map;
            _unit = unit;
            this.userManager = userManager;
        }

        public async Task<bool> CreateStudent(RegisterUser student)
        {
            var appuser = _map.Map<AppUser>(student);
            var result = await userManager.CreateAsync(appuser, student.Password);
            if (!result.Succeeded)
                return false;
            await userManager.AddToRoleAsync(appuser, student.Role);


            var user = _map.Map<Student>(student);
            user.AppUserId = appuser.Id;



            _unit.Students.Add(user);
            int r = _unit.save();

            if (r > 0)
            {
                return true;
            }
            else
            { return false; }


        }

        public async Task<bool> DeleteStudent(string  Id)
        {
            var student= await  _unit.Students.GetByIdAsync(Id);
            if (student == null)
                return false;
             _unit.Students.Delete(student);
            int r = _unit.save();
            if (r > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public async Task<IEnumerable<GetUser>> GetListStudent()
        {
            
            
                var students = _unit.Students.GetAll(s => s.AppUser).ToList();
                var studentReq = _map.Map<IEnumerable<GetUser>>(students);
                return studentReq;

        }

        public async Task<GetUser> GetStudent(string id)
        {

            var userstudent = await _unit.Students.GetByIdWithIncludeAsync(s => s.AppUserId == id,s=>s.AppUser);

            if (userstudent != null)

            {
                var response = _map.Map<GetUser>(userstudent);


                return response;


            }
            else 
                return null;
           
        }

        public async Task<bool> UpdateStudent(string id,EditUser updatedStudent)
        {
            var existingStudent = await _unit.Students.GetByIdAsync(id);

            if (existingStudent == null)
                return false;

            existingStudent.AppUser.Email = updatedStudent.Email;
            existingStudent.AppUser.UserName = updatedStudent.Email;
            existingStudent.AppUser.PasswordHash = updatedStudent.Password;
            existingStudent.AppUser.PhoneNumber = updatedStudent.PhoneNumber;
           

          _unit.Students.Update(existingStudent); 
            

            
            int r = _unit.save();
            if (r > 0)
            {
                return true;
            }
            else
            { return false; }

        }

    }
}
