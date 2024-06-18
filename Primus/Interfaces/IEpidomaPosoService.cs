using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaPosoService
    {
        void Create(EpidomaPosoViewModel data);
        void Destroy(EpidomaPosoViewModel data);
        IEnumerable<EpidomaPosoViewModel> Read();
        EpidomaPosoViewModel Refresh(int entityId);
        void Update(EpidomaPosoViewModel data);
    }
}