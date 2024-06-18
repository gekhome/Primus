using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IDiaxriristesService
    {
        void Create(DiaxiristisViewModel data);
        void Destroy(DiaxiristisViewModel data);
        IEnumerable<DiaxiristisViewModel> Read();
        DiaxiristisViewModel Refresh(int entityId);
        void Update(DiaxiristisViewModel data);
    }
}