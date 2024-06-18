using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface ISchoolDataService
    {
        void Create(SchoolsGridViewModel data);
        void Destroy(SchoolsGridViewModel data);
        SchoolsViewModel GetRecord(int schoolId);
        IEnumerable<SchoolsGridViewModel> Read();
        SchoolsGridViewModel Refresh(int entityId);
        void Update(SchoolsGridViewModel data);
    }
}