using Application.contract;
using AutoMapper;
using Azure;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
       
        private readonly IUniteOfWork _unit;
        private readonly IMapper _map;
        private readonly IUserRepo _userRepo;

        public UserService(IUniteOfWork unit, IMapper map, IUserRepo userRepo)
        {

            _unit = unit;
            _map = map;
            _userRepo = userRepo;
        }

        public async Task<ResponseService<RegisterUser>> RegisterUser(RegisterUser dtouser)
        {
            int r = 0;
            var response = new ResponseService<RegisterUser>();
            var result = await _userRepo.CreateAsync(dtouser);

            if (result == null)
            {
                response.Success = false;
                response.Message = "failed create user ";
            }

            else if (result.Role.ToLower() == "student")
            {
                var user = _map.Map<Student>(dtouser);
                user.AppUserId = result.Id;
                _unit.Students.Add(user);
                r = _unit.save();
                if (r > 0) { response.Message = "successfully created student "; response.Data = dtouser; }
                else { response.Message = "successfully created user "; response.Data = dtouser; }
            }
            else if (dtouser.Role.ToLower() == "professor")
            {

                var user = _map.Map<Professor>(dtouser);
                user.AppUserId = result.Id;
                _unit.Professors.Add(user);
                r = _unit.save();
                if (r > 0)
                {
                    response.Message = "successfully created professor ";
                    response.Data = dtouser; return response;
                }


            }

            else
            {
                response.Message = "failed register user";
                response.Success = false;
            }

            return response;

        }
        public async Task<bool> DeleteUser(string Id)
        {
            
           return  await _userRepo.Delete(Id);
           
            
        }

        public async Task<ResponseService<IEnumerable<GetUser>>> GetListUser()
        {
            var response = new ResponseService<IEnumerable<GetUser>>();

            var students = await _userRepo.GetAllAsync();
            if (students == null) { response.Message = "no user found! "; response.Success = false; }
            else
            {
                var studentReq = _map.Map<IEnumerable<GetUser>>(students);
                response.Data = studentReq;

            }
            return response;



        }

        public async Task<ResponseService<GetUser>> GetUser(string id)
        {
            var response= new ResponseService<GetUser>();
            var userstudent = await _userRepo.GetByIdAsync(id);

            if (userstudent != null)

            {
                 response.Data = _map.Map<GetUser>(userstudent);
                 
                response.Message = "user fetched successfully";


                return response;


            }
            else { response.Success = false; response.Message = "user not found "; 
                return response;
            }

        }

        public async Task<ResponseService<AppUser>> UpdateUser(string id, EditUser updatedStudent)
        {
            var response = new ResponseService<AppUser>();


           var respon=  await _userRepo.UpdateAsync(id,updatedStudent);
            if (!respon)
            {
                response.Success = false;
                response.Message = "failed update user ";

            }

            response.Message = "user successfully updated";

            return response;




        }


    }
}
