using Application.contract;
using Application.Services;
using BackEnd.Core.DTOs.ProfessorDTO;
using BackEnd.Core.DTOs.UserDTO;
using BackEnd.Core.JwtHelpers;
using BackEnd.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StudentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _auth;
        

        public AccountController(IAuthService auth)
        {
            _auth = auth;
           
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] RegisterUser stud)
        {

            var result = await _auth.RegisterStudent(stud);
            if (result.Success)
            {

                return Ok(result);

            }
            else
            {


                return BadRequest();
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            try
            {
                var token = await _auth.LoginAsync(dto);
                if (token == null)
                    return BadRequest("Wrong email or password");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}