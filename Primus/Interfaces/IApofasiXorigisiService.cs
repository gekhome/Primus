using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofasiXorigisiService
    {
        void Create(ApofasiXorigisiGridViewModel data);
        void Destroy(ApofasiXorigisiGridViewModel data);
        ApofasiXorigisiViewModel GetRecord(int entityId);
        IEnumerable<ApofasiXorigisiGridViewModel> Read();
        ApofasiXorigisiGridViewModel Refresh(int entityId);
        void Update(ApofasiXorigisiGridViewModel data);
        void UpdateRecord(ApofasiXorigisiViewModel data, int apofasiId);
    }
}