using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class Lecture
    {
        public Guid Id { get; set; }=Guid.NewGuid();    
        public DateTime StartTime { get; set; }  = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(2);
        public string? QrCodeImgPath { get; set; }
       

        public Guid ScheduleLectureId { get; set; }
        public LectureSchedule ScheduleLecture { get; set; } = null;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
