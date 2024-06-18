using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface ISocialGroupService
    {
        void Create(SocialGroupViewModel data);
        void Destroy(SocialGroupViewModel data);
        IEnumerable<SocialGroupViewModel> Read();
        SocialGroupViewModel Refresh(int entityId);
        void Update(SocialGroupViewModel data);
    }
}