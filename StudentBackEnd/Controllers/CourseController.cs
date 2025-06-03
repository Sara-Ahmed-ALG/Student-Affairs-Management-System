using Application.contract;
using Application.Services;
using BackEnd.Core.DTOs.Course;
using BackEnd.Core.DTOs.ProfessorDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace StudentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = "Admin,Professor")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse( [FromBody]CreateCourse course)

        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(); // Returns 400 with validation errors
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _courseService.CreateCourseAsync(course, UserId);
            if (result.Success)
            {

                return Ok(result);

            }
            else
            {


                return BadRequest(result);
            }


        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _courseService.GetAllCoursesAsync();
            if (!result.Success)
                return NotFound();

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result.Success)
                return NotFound();

            return Ok(result);
        }

    }
}
