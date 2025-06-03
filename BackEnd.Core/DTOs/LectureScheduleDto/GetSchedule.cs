using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.DTOs.LectureScheduleDto
{
    public class GetSchedule
    {
      
            public Guid Id { get; set; }
            public Guid CourseId { get; set; }
            public string DayOfWeek { get; set; }
            public string CourseName { get; set; } 
            public DateTime LectureDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string Location { get; set; }
       
    }
}
