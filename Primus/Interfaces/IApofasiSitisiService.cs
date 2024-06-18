using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofasiSitisiService
    {
        void Create(ApofasiSitisiGridViewModel data);
        void Destroy(ApofasiSitisiGridViewModel data);
        ApofasiSitisiViewModel GetRecord(int entityId);
        IEnumerable<ApofasiSitisiGridViewModel> Read();
        ApofasiSitisiGridViewModel Refresh(int entityId);
        void Update(ApofasiSitisiGridViewModel data);
        void UpdateRecord(ApofasiSitisiViewModel data, int apofasiId);
    }
}