using BackEnd.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.DTOs.Lecture
{
    public class CreateLecture
    {

        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(2);
        public string? QrCodeImgPath { get; set; }
        public Guid ScheduleLectureId { get; set; }
        
    }
}
