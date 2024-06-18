using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAitisi2Service
    {
        void Create(Aitisi2GridViewModel data, int studentId, int schoolId);
        void Destroy(Aitisi2GridViewModel data);
        Aitisi2ViewModel GetRecord(int aitisiId);
        IEnumerable<Aitisi2GridViewModel> Read(int studentId);
        Aitisi2GridViewModel Refresh(int entityId);
        void Update(Aitisi2GridViewModel data, int studentId, int schoolId);
        void UpdateRecord(Aitisi2ViewModel data, StudentViewModel student, int aitisiId, int schoolId);
    }
}