using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaStegasiService
    {
        void Create(EpidomaStegasiViewModel data, EpidomaParameters ep, AitisiViewModel aitisi);
        void Destroy(EpidomaStegasiViewModel data);
        EpidomaStegasiViewModel GetRecord(int entityId);
        IEnumerable<EpidomaStegasiViewModel> Read(EpidomaParameters ep);
        EpidomaStegasiViewModel Refresh(int entityId);
        void Update(EpidomaStegasiViewModel data, EpidomaParameters ep);
        void UpdateRecord(EpidomaStegasiViewModel data, int epidotisiId);
    }
}