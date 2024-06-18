using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofasiStegasiService
    {
        void Create(ApofasiStegasiGridViewModel data);
        void Destroy(ApofasiStegasiGridViewModel data);
        ApofasiStegasiViewModel GetRecord(int entityId);
        IEnumerable<ApofasiStegasiGridViewModel> Read();
        ApofasiStegasiGridViewModel Refresh(int entityId);
        void Update(ApofasiStegasiGridViewModel data);
        void UpdateRecord(ApofasiStegasiViewModel data, int apofasiId);
    }
}