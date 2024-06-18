using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IDioikitesService
    {
        void Create(DioikitisViewModel data);
        void Destroy(DioikitisViewModel data);
        IEnumerable<DioikitisViewModel> Read();
        DioikitisViewModel Refresh(int entityId);
        void Update(DioikitisViewModel data);
    }
}