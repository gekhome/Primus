using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IGenikoiService
    {
        void Create(DirectorGeneralViewModel data);
        void Destroy(DirectorGeneralViewModel data);
        IEnumerable<DirectorGeneralViewModel> Read();
        DirectorGeneralViewModel Refresh(int entityId);
        void Update(DirectorGeneralViewModel data);
    }
}