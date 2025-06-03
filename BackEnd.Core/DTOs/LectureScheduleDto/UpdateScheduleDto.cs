using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.DTOs.LectureScheduleDto
{
    public class UpdateScheduleDto: CreateScheduleDto
    {
        public Guid Id { get; set; }
    }
}
