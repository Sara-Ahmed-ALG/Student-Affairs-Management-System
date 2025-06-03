using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class LectureSchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DayOfWeek { get; set; } = null;
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime LectureDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Location { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture> ();

    }
   

 

 
}