using Application.contract;
using BackEnd.Core.DTOs.LectureScheduleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureScheduleController : ControllerBase
    {
        
        
            private readonly IScheduleService   _ScheduleService;

        public LectureScheduleController(IScheduleService scheduleService)
        {
            _ScheduleService = scheduleService;
        }



         
        [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var result = await _ScheduleService.GetAllAsync();
              if (!result.Success)
                return NotFound(result);
                return Ok(result);
            }



      
        [Authorize(Roles = "Admin")]
        [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateScheduleDto dto)
            {
                var created = await _ScheduleService.CreateAsync(dto);
                return Ok(created);
            }

     
        [Authorize(Roles = "Admin")]
        [HttpPut()]
            public async Task<IActionResult> Update( [FromBody] UpdateScheduleDto dto)
            {
                var updated = await _ScheduleService.UpdateAsync(dto);
                if (!updated.Success)
                    return NotFound();

                return Ok("Updated Successfully");
            }

       
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var deleted = await _ScheduleService.DeleteAsync(id);
                if (!deleted.Success)
                    return NotFound();

                return Ok("Deleted Successfully");
            }
        

    }
}
