using Application.contract;
using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AttendanceService:IAttendanceService
    {
        
            private readonly IUniteOfWork _unitOfWork;

            public AttendanceService(IUniteOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<bool> MarkAttendanceAsync(string studentId, Guid lectureId)
            {
                // تحقق إن الطالب لم يحضر من قبل
                var alreadyExists =  await _unitOfWork.Attendances.AnyAsync(a => a.StudentId == studentId && a.LectureId == lectureId);


            if (alreadyExists
                )
                    return false;

                var attendance = new Attendance
                {
                    StudentId = studentId,
                    LectureId = lectureId,
                    AttendanceTime = DateTime.UtcNow
                };

                await _unitOfWork.Attendances.AddAsync(attendance);
                return _unitOfWork.save() > 0;
            }
        
    }
}
