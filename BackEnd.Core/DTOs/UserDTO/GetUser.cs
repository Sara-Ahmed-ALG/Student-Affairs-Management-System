using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.DTOs.ProfessorDTO
{
    public class GetUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
         public string Email { get; set; }

         public string PhoneNumber { get; set; }
         public string role { get; set; }

    }
}
