using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IDirectorsService
    {
        void Create(DirectorViewModel data);
        void Destroy(DirectorViewModel data);
        IEnumerable<DirectorViewModel> Read();
        DirectorViewModel Refresh(int entityId);
        void Update(DirectorViewModel data);
    }
}