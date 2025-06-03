using Application.contract;
using AutoMapper;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.LectureScheduleDto;
using BackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.ScheduleService;

namespace Application.Services
{
    public class ScheduleService : IScheduleService
    {
        
            private readonly IUniteOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public ScheduleService(IUniteOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ResponseService<bool>> CreateAsync(CreateScheduleDto dto)
            {
                var response = new ResponseService<bool>();

                var schedule = _mapper.Map<LectureSchedule>(dto);
                await _unitOfWork.LectureSchedules.AddAsync(schedule);
                int result =  _unitOfWork.save();

                if (result > 0)
                {
                    response.Data = true;
                    response.Message = "Lecture scheduled successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to schedule lecture.";
                }

                return response;
            }

            public async Task<ResponseService<List<GetSchedule>>> GetAllAsync()
            {
                var response = new ResponseService<List<GetSchedule>>();

                var data = await _unitOfWork.LectureSchedules.GetAllAsync(includeProperties: "Course");

                response.Data = _mapper.Map<List<GetSchedule>>(data);

                return response;
            }

            public async Task<ResponseService<bool>> UpdateAsync(UpdateScheduleDto dto)
            {
                var response = new ResponseService<bool>();

                var existing = await _unitOfWork.LectureSchedules.GetByIdAsync(dto.Id);
                if (existing == null)
                {
                    response.Success = false;
                    response.Message = "Schedule not found.";
                    return response;
                }

                _mapper.Map(dto, existing);
                _unitOfWork.LectureSchedules.Update(existing);
                int result =  _unitOfWork.save();

                response.Data = result > 0;
                response.Message = result > 0 ? "Updated successfully." : "Update failed.";
                return response;
            }

            public async Task<ResponseService<bool>> DeleteAsync(Guid id)
            {
                var response = new ResponseService<bool>();

                var existing = await _unitOfWork.LectureSchedules.GetByIdAsync(id);
                if (existing == null)
                {
                    response.Success = false;
                    response.Message = "Schedule not found.";
                    return response;
                }

                _unitOfWork.LectureSchedules.Delete(existing);
                int result =  _unitOfWork.save();

                response.Data = result > 0;
                response.Message = result > 0 ? "Deleted successfully." : "Delete failed.";
                return response;
            }
     
    }
}
