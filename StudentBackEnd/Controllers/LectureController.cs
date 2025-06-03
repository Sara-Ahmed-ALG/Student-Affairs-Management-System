using Application.contract;
using BackEnd.Core.DTOs.Lecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StudentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
       
       
            private readonly ILectureService _lectureService;

            public LectureController(ILectureService lectureService)
            {
                _lectureService = lectureService;
            }
            [Authorize(Roles ="Professor")]
            [HttpPost]
            public async Task<IActionResult> CreateLecture([FromBody] CreateLecture dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var lecture = await _lectureService.CreateLectureAsync(dto);

                if (lecture == null)
                    return StatusCode(500, "Failed to create lecture");

                return Ok(lecture);

            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecture( Guid id)
        {


            var lecture = await _lectureService.GetQrCodeAsync(id);

            if (lecture == null)
                return StatusCode(500, "Failed to create lecture");

            return Ok(lecture);


        }

        }
}
