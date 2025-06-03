using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class LectureRepo : GenericRepo<Lecture>, ILectureRepo
    {
        public LectureRepo(StudentDbContext _db) : base(_db)
        {
        }
    }
}
