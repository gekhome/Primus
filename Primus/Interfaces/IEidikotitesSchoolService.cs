using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEidikotitesSchoolService
    {
        void Create(EidikotitaYearSchoolViewModel data, int schoolyearId);
        void Create(EidikotitaYearSchoolViewModel data, int schoolyearId, int schoolId);
        void Destroy(EidikotitaYearSchoolViewModel data);
        IEnumerable<EidikotitaYearSchoolViewModel> Read(int schoolyearId);
        IEnumerable<EidikotitaYearSchoolViewModel> Read(int schoolyearId, int schoolId);
        EidikotitaYearSchoolViewModel Refresh(int entityId);
        void Update(EidikotitaYearSchoolViewModel data, int schoolyearId);
        void Update(EidikotitaYearSchoolViewModel data, int schoolyearId, int schoolId);
    }
}