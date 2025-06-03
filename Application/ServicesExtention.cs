using Application.contract;
using Application.Services;
using BackEnd.Core.Interfaces;
using BackEnd.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServicesExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IUniteOfWork, UniteOfWork>();

            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<IProfessorRepo, ProfessorRepo>();

            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IMaterialRepo, MaterialRepo>();

            services.AddScoped<IScheduleService,ScheduleService>();
            services.AddScoped<ILectureScheduleRepo,LectureScheduleRepo>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepo,UserRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILectureRepo,LectureRepo>();
            services.AddScoped<IAttendanceRepo,AttendanceRepo>();
            services.AddScoped<IAttendanceService,AttendanceService>();

            return services;
        }

    }
}
