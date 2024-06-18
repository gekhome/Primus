using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEidikotitesService
    {
        void Create(EidikotitesViewModel data);
        void Destroy(EidikotitesViewModel data);
        IEnumerable<EidikotitesViewModel> Read();
        EidikotitesViewModel Refresh(int entityId);
        void Update(EidikotitesViewModel data);
    }
}