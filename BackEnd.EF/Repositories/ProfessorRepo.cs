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
    public class ProfessorRepo : GenericRepo<Professor>, IProfessorRepo
    {
        public ProfessorRepo(StudentDbContext _db) : base(_db)
        {
        }
       
    }
}
