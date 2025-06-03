using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.LectureScheduleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IScheduleService
    {
       
            Task<ResponseService<bool>> CreateAsync(CreateScheduleDto dto);
            Task<ResponseService<List<GetSchedule>>> GetAllAsync();
            Task<ResponseService<bool>> UpdateAsync(UpdateScheduleDto dto);
            Task<ResponseService<bool>> DeleteAsync(Guid id);
        
    }
}
