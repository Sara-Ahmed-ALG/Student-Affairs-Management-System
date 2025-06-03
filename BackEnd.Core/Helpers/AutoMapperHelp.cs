using AutoMapper;
using BackEnd.Core.Domain;
using BackEnd.Core.DTOs.Course;
using BackEnd.Core.DTOs.LectureScheduleDto;
using BackEnd.Core.DTOs.ProfessorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;


namespace BackEnd.Core.Helpers
{
    public class AutoMapperHelp:Profile
    {
        public AutoMapperHelp() {

            CreateMap<RegisterUser, Student>().ReverseMap();
            CreateMap<RegisterUser, Professor>().ReverseMap();

            CreateMap<RegisterUser, AppUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));


            CreateMap<AppUser, GetUser>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role));

            CreateMap<EditUser, AppUser>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
          

            CreateMap<Student, GetUser>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<LectureSchedule,GetSchedule>().ReverseMap();
            CreateMap<LectureSchedule, UpdateScheduleDto>().ReverseMap();
            CreateMap<LectureSchedule, CreateScheduleDto>().ReverseMap();

        }
    }
}
