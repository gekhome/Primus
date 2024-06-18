using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IStudentInfo2Service
    {
        StudentInfo2ViewModel GetRecord(int entityId);
        IEnumerable<StudentInfo2ViewModel> Read(int schoolId);
        IEnumerable<sqlAitiseis2ViewModel> ReadAitiseis(int studentId);
    }
}