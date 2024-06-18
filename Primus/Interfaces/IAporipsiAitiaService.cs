using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAporipsiAitiaService
    {
        void Create(AporipsiAitiaViewModel data);
        void Destroy(AporipsiAitiaViewModel data);
        IEnumerable<AporipsiAitiaViewModel> Read();
        AporipsiAitiaViewModel Refresh(int entityId);
        void Update(AporipsiAitiaViewModel data);
    }
}