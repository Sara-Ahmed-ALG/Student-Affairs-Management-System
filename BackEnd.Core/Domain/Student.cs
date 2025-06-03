using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class Student 
    {
       
        public Guid Id { get; set; }= Guid.NewGuid();
       
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
      
        public string UserName => AppUser?.UserName;
    }

}

