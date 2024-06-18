using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAitisi2SocialService
    {
        void Create(Aitisi2SocialGroupViewModel data, int aitisiId);
        void Destroy(Aitisi2SocialGroupViewModel data);
        IEnumerable<Aitisi2SocialGroupViewModel> Read(int aitisiId);
        Aitisi2SocialGroupViewModel Refresh(int entityId);
        void Update(Aitisi2SocialGroupViewModel data, int aitisiId);
    }
}