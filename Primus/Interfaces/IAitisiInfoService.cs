using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IAitisiInfoService
    {
        sqlAitiseisViewModel GetRecord(int entityId);
        IEnumerable<sqlAitiseisViewModel> Read(int schoolyearId = 0);
        IEnumerable<sqlAitiseisViewModel> Read(int schoolId, int schoolyearId = 0);
    }
}