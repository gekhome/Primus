using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaXorigisiService
    {
        void Create(EpidomaXorigisiViewModel data, EpidomaParameters ep, AitisiViewModel aitisi);
        void Destroy(EpidomaXorigisiViewModel data);
        EpidomaXorigisiViewModel GetRecord(int entityId);
        IEnumerable<EpidomaXorigisiViewModel> Read(EpidomaParameters ep);
        EpidomaXorigisiViewModel Refresh(int entityId);
        void Update(EpidomaXorigisiViewModel data, EpidomaParameters ep);
        void UpdateRecord(EpidomaXorigisiViewModel data, int epidotisiId);
    }
}