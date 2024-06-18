using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IProistamenoiService
    {
        void Create(ProistamenosViewModel data);
        void Destroy(ProistamenosViewModel data);
        IEnumerable<ProistamenosViewModel> Read();
        ProistamenosViewModel Refresh(int entityId);
        void Update(ProistamenosViewModel data);
    }
}