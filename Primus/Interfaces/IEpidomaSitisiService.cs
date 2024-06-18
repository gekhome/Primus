using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaSitisiService
    {
        void Create(EpidomaSitisiViewModel data, EpidomaParameters ep, AitisiViewModel aitisi);
        void Destroy(EpidomaSitisiViewModel data);
        EpidomaSitisiViewModel GetRecord(int entityId);
        IEnumerable<EpidomaSitisiViewModel> Read(EpidomaParameters ep);
        EpidomaSitisiViewModel Refresh(int entityId);
        void Update(EpidomaSitisiViewModel data, EpidomaParameters ep);
        void UpdateRecord(EpidomaSitisiViewModel data, int epidotisiId);
    }
}