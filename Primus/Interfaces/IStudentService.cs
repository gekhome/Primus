using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IStudentService
    {
        void CopyStudentData(int targetStudentId, string afm);
        void Create(StudentGridViewModel data);
        void Create(StudentGridViewModel data, int schoolId);
        void Destroy(StudentGridViewModel data);
        StudentViewModel GetRecord(int studentId);
        IEnumerable<StudentGridViewModel> Read();
        IEnumerable<StudentGridViewModel> Read(int schoolId);
        StudentGridViewModel Refresh(int entityId);
        void Update(StudentGridViewModel data);
        void Update(StudentGridViewModel data, int schoolId);
        void UpdateRecord(StudentViewModel model, int studentId, int schoolId = 0);
    }
}