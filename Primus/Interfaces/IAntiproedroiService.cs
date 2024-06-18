using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAntiproedroiService
    {
        void Create(AntiproedrosViewModel data);
        void Destroy(AntiproedrosViewModel data);
        IEnumerable<AntiproedrosViewModel> Read();
        AntiproedrosViewModel Refresh(int entityId);
        void Update(AntiproedrosViewModel data);
    }
}