using Application.contract;
using Azure;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.DTOs.UserDTO;
using BackEnd.Core.Interfaces;
using BackEnd.Core.JwtHelpers;
using BackEnd.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
      
       
        private readonly JwtSettings jwtSettings;
        private readonly IUserRepo _userRepo;
        public AuthService( JwtSettings jwtSettings, IUserRepo userRepo)
        {
            
            this.jwtSettings = jwtSettings;
            _userRepo = userRepo;
        }



        public async Task<UserTokens> LoginAsync(LoginDto loginDto)
        {

         

            //login action
            try
            {
                var token = new UserTokens();
                var user = await _userRepo.GetByEmailAsync(loginDto.Email);

                if (user!=null)
                {
                    

                    // create token for this userkm,.

                    token = JwtHelper.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.Email,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Roles =user.Role,
                        Id =user.Id,

                    }, jwtSettings);
                }
                else

                { return null; }

                return token;
                

                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseService<AppUser>> RegisterStudent(RegisterUser dto)
        {
            int r = 0;
            var response = new ResponseService<AppUser>();
            if (dto == null)
            {
                response.Success = false;
                response.Message = "failed create user ";
            }
            else if (dto.Role == "student")
            {
                var result = await _userRepo.CreateAsync(dto);
                response.Success = true;
                response.Message = "Welcome to Our Website ";
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = " Role Not Valide ";
            }

            return response;
        }
    }
    }

