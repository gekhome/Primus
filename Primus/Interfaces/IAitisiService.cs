using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAitisiService
    {
        void Create(AitisiGridViewModel data, int studentId);
        void Destroy(AitisiGridViewModel data);
        AitisiViewModel GetRecord(int aitisiId);
        IEnumerable<AitisiGridViewModel> Read(int studentId);
        AitisiGridViewModel Refresh(int entityId);
        void Update(AitisiGridViewModel data, int studentId);
        void UpdateRecord(AitisiViewModel data, StudentViewModel student, int aitisiId, bool admin = false);
    }
}