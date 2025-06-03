using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Lecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface ILectureService
    {
        Task<Lecture> CreateLectureAsync(CreateLecture dto);
        Task<string> GetQrCodeAsync(Guid lectureId);

    }
}
