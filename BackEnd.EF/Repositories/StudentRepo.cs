using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class StudentRepo : GenericRepo<Student>, IStudentRepo
    {
        public StudentRepo(StudentDbContext _db) : base(_db)
        {
        }
    }
}
