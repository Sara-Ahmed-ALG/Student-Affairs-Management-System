using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class MaterialRepo : GenericRepo<CourseMaterial>, IMaterialRepo
    {
        public MaterialRepo(StudentDbContext _db) : base(_db)
        {
        }
    }
}
