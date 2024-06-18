using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IStudentInfoService
    {
        StudentInfoViewModel GetRecord(int entityId);
        IEnumerable<StudentInfoViewModel> Read();
        IEnumerable<StudentInfoViewModel> Read(int schoolId);
        IEnumerable<sqlAitiseisViewModel> ReadAitiseis(int studentId);
    }
}