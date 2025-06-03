using Application.contract;
using BackEnd.Core.DTOs.Attendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StudentBackEnd.Controllers
{
    [Authorize(Roles ="Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
      

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
           
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
        {
            var studentId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            
                var success = await _attendanceService.MarkAttendanceAsync(studentId, dto.LectureId);

                if (!success)
                    return BadRequest("Already marked or lecture not found");

                return Ok("Attendance marked");
            
            
        }

    }
}
