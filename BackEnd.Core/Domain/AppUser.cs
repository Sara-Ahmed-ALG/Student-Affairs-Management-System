using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace BackEnd.Core.Domain
{
    public class AppUser: IdentityUser 
    {
        public Student student { get; set; }
        public Professor professor { get; set; }
        public string Role { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
