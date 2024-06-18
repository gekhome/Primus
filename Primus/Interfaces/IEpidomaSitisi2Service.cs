using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaSitisi2Service
    {
        void Create(EpidomaSitisi2ViewModel data, EpidomaParameters2 ep, Aitisi2ViewModel aitisi);
        void Destroy(EpidomaSitisi2ViewModel data);
        EpidomaSitisi2ViewModel GetRecord(int entityId);
        Aitisi2ViewModel GetRelatedAitisi2(int studentId, EpidomaParameters2 ep);
        IEnumerable<EpidomaSitisi2ViewModel> Read(EpidomaParameters2 ep);
        EpidomaSitisi2ViewModel Refresh(int entityId);
        bool RelatedAitisi2Exists(int studentId, EpidomaParameters2 ep);
        void Update(EpidomaSitisi2ViewModel data, EpidomaParameters2 ep);
    }
}