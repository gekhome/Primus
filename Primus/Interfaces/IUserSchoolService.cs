using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IUserSchoolService
    {
        void Create(UserSchoolViewModel data);
        void Destroy(UserSchoolViewModel data);
        IEnumerable<UserSchoolViewModel> Read();
        UserSchoolViewModel Refresh(int entityId);
        void Update(UserSchoolViewModel data);
    }
}