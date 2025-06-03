using BackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly StudentDbContext _studentDbContext;
        public IProfessorRepo Professors { get; }
        public IStudentRepo Students { get; }

        public IUserRepo Users { get; }

        public ICourseRepo Courses { get; }

        public IMaterialRepo CourseMaterial { get; }

        public ILectureScheduleRepo LectureSchedules { get; }

        public ILectureRepo Lectures { get; }

        public IAttendanceRepo Attendances {  get; }

        public UniteOfWork(StudentDbContext studentDbContext, IProfessorRepo professors, IStudentRepo students, IUserRepo users, ICourseRepo courses, IMaterialRepo courseMaterial, ILectureScheduleRepo lectureSchedules, ILectureRepo lectures, IAttendanceRepo attendances)
        {
            this._studentDbContext = studentDbContext;
            Professors = professors;
            Students = students;
            Users = users;
            Courses = courses;
            CourseMaterial = courseMaterial;
            LectureSchedules = lectureSchedules;
            Lectures = lectures;
            Attendances = attendances;
        }



        public int save()
        {
            return _studentDbContext.SaveChanges();
        }
    }
}
