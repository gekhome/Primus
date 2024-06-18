using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaSynexiaService
    {
        void Create(EpidomaSynexeiaViewModel data, EpidomaParameters ep, AitisiViewModel aitisi);
        void Destroy(EpidomaSynexeiaViewModel data);
        EpidomaSynexeiaViewModel GetRecord(int entityId);
        IEnumerable<EpidomaSynexeiaViewModel> Read(EpidomaParameters ep);
        EpidomaSynexeiaViewModel Refresh(int entityId);
        void Update(EpidomaSynexeiaViewModel data, EpidomaParameters ep);
        void UpdateRecord(EpidomaSynexeiaViewModel data, int epidotisiId);
    }
}