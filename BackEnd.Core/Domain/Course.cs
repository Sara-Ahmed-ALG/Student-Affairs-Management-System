using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description  { get; set; }

        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
        public ICollection<LectureSchedule> Schedules { get; set; } = new List<LectureSchedule>();


    }
}
