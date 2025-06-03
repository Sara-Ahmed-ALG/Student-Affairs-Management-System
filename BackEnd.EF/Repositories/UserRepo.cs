using AutoMapper;
using Azure;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class UserRepo : IUserRepo
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _map;


        public UserRepo(UserManager<AppUser> userManager, IMapper map)
        {
            _userManager = userManager;
            _map = map;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var count = await _userManager.Users.CountAsync();

            return users;
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            var use = await _userManager.FindByIdAsync(id);
            if (use == null)
            { return null; }
            return use;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
           var user= await _userManager.FindByEmailAsync(email);
            return  user;
        }

        public async Task<AppUser> CreateAsync(RegisterUser user)
        {
            var newUser = _map.Map<AppUser>(user);

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
                return null;


            if (!await _userManager.IsInRoleAsync(newUser, user.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(newUser, user.Role);
                if (!roleResult.Succeeded) { return null; }


            }

            return newUser;
        }

        public async Task<bool> UpdateAsync(string id, EditUser user)
        {

            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
                return false;

                  _map.Map(user, existingUser);

            var updated = await _userManager.UpdateAsync(existingUser);

            return true;

        }

        public async Task<bool> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            { return false; }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

    } 
}
