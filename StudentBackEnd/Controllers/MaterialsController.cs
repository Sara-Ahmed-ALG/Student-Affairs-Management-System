using Application.contract;
using BackEnd.Core.DTOs.Material;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [Authorize(Roles = "Professor")]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]MaterialUploadDto dto )

        {
            var result = await _materialService.UploadAsync(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
