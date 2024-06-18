using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofasiSitisi2Service
    {
        void Create(ApofasiSitisi2ViewModel data, int schoolId);
        void Destroy(ApofasiSitisi2ViewModel data);
        ApofasiSitisi2ViewModel GetRecord(int entityId);
        IEnumerable<ApofasiSitisi2ViewModel> Read(int schoolId);
        ApofasiSitisi2ViewModel Refresh(int entityId);
        void Update(ApofasiSitisi2ViewModel data, int schoolId);
        void UpdateRecord(ApofasiSitisi2ViewModel model, int apofasiId, int schoolId);
    }
}