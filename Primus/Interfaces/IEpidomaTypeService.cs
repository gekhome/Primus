using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidomaTypeService
    {
        void Create(SysEpidomaTypeViewModel data);
        void Destroy(SysEpidomaTypeViewModel data);
        IEnumerable<SysEpidomaTypeViewModel> Read();
        SysEpidomaTypeViewModel Refresh(int entityId);
        void Update(SysEpidomaTypeViewModel data);
    }
}