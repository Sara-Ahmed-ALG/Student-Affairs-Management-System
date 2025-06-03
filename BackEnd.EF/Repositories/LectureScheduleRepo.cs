using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class LectureScheduleRepo : GenericRepo<LectureSchedule>, ILectureScheduleRepo
    {
        public LectureScheduleRepo(StudentDbContext _db) : base(_db)
        {
        }
    }
}
