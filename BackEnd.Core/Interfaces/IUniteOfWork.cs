using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Interfaces
{
    public interface IUniteOfWork 
    {
         IProfessorRepo Professors { get;  }
         IStudentRepo Students { get;  }
         IUserRepo Users { get; }
        ICourseRepo Courses { get; }
        IMaterialRepo CourseMaterial { get; }
        ILectureScheduleRepo LectureSchedules { get; }
        ILectureRepo Lectures { get; }
        IAttendanceRepo Attendances { get; }
        int save();
    }
}
