using Application.contract;
using BackEnd.Core.DTOs.ProfessorDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

     namespace StudentBackEnd.Controllers
    {
   
        [ApiController]
        [Route("api/[controller]")]
    public class UserController : ControllerBase
     {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
                    {
                        _userService = userService;
                    }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm]RegisterUser stud)
        {
          
            var result= await _userService.RegisterUser(stud);
            if (result.Success)
            {
               
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
            public async Task<IActionResult> GetAllUsers()
            {
                var result = await _userService.GetListUser();
            if (result==null)
                return NotFound();

            return Ok(result);
            
            }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
            public async Task<IActionResult> GetUserById(string id)
            {
                var result = await _userService.GetUser(id);
                if (!result.Success) 
                    return NotFound();

                return Ok(result);
            }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUser dto)
        {
            var result = await _userService.UpdateUser(id, dto);
            if (!result.Success)
                return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var deleted = await _userService.DeleteUser(Id);
            if (!deleted)
                return NotFound();
            else
                return Ok("User deleted Successfully");
        }

        //----------------------------------------------------------------------------//

        [Authorize(Roles = "Student,Professor")]
        [HttpPut("Update-Profile")]
            public async Task<IActionResult> UpdateProfile([FromBody] EditUser dto)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _userService.UpdateUser(userId, dto);
                if (!result.Success)
                    return BadRequest();
            return Ok(result);
             }


        [Authorize(Roles = "Professor,Student")]
        [HttpGet("Me")]
        public async Task<IActionResult> GetMeById()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.GetUser(userId);
            if (!result.Success)
                return NotFound();

            return Ok(result);
        }
        [Authorize(Roles = "Professor,Student")]
        [HttpDelete("Delete-Me")]
        public async Task<IActionResult> DeleteMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleted = await _userService.DeleteUser(userId);
            if (!deleted)
                return NotFound();
            else
                return Ok("Your Account deleted Successfully");
        }
        


      
        }
    }

