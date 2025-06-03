using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.Domain
{
    public class Professor 
    {
        public Guid Id { get; set; } = Guid.NewGuid();

      
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public string UserName => AppUser?.UserName;

    }
}
