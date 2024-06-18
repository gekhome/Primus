using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IParametersService
    {
        void Create(SysApofasiParametersViewModel data);
        void Destroy(SysApofasiParametersViewModel data);
        IEnumerable<SysApofasiParametersViewModel> Read();
        SysApofasiParametersViewModel Refresh(int entityId);
        void Update(SysApofasiParametersViewModel data);
    }
}