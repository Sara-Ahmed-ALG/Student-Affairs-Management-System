using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.contract
{
    public interface IAttendanceService 
    {
        Task<bool> MarkAttendanceAsync(string studentId, Guid LectureId); 
    }
}
