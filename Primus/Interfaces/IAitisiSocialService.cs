using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAitisiSocialService
    {
        void Create(AitisiSocialGroupViewModel data, int aitisiId);
        void Destroy(AitisiSocialGroupViewModel data);
        IEnumerable<AitisiSocialGroupViewModel> Read(int aitisiId);
        AitisiSocialGroupViewModel Refresh(int entityId);
        void Update(AitisiSocialGroupViewModel data, int aitisiId);
    }
}