using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofasiSynexiaService
    {
        void Create(ApofasiSynexeiaGridViewModel data);
        void Destroy(ApofasiSynexeiaGridViewModel data);
        ApofasiSynexeiaViewModel GetRecord(int entityId);
        IEnumerable<ApofasiSynexeiaGridViewModel> Read();
        ApofasiSynexeiaGridViewModel Refresh(int entityId);
        void Update(ApofasiSynexeiaGridViewModel data);
        void UpdateRecord(ApofasiSynexeiaViewModel data, int apofasiId);
    }
}