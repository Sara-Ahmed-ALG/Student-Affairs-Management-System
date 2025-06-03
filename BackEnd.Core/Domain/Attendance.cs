using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string  StudentId { get; set; }
        public AppUser Student { get; set; }

        public Guid LectureId { get; set; }
        public Lecture Lecture { get; set; }
        public DateTime AttendanceTime { get; set; } = DateTime.UtcNow;
    }
}
