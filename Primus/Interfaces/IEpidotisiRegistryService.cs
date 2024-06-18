using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IEpidotisiRegistryService
    {
        sqlEpidomaAllDetailViewModel GetRecordOliko(int recordId);
        sqlEpidomaSitisiViewModel GetRecordSitisi(int epidomaId);
        sqlEpidomaSitisiSchoolViewModel GetRecordSitisi2(int epidomaId);
        sqlEpidomaStegasiViewModel GetRecordStegasi(int epidomaId);
        sqlEpidomaSynexeiaViewModel GetRecordSynexia(int epidomaId);
        sqlEpidomaXorigisiViewModel GetRecordXorigisi(int epidomaId);
        List<sqlEpidomaAllDetailViewModel> ReadOliko();
        List<sqlEpidomaSitisiViewModel> ReadSitisi();
        List<sqlEpidomaSitisiSchoolViewModel> ReadSitisi2();
        List<sqlEpidomaStegasiViewModel> ReadStegasi();
        List<sqlEpidomaSynexeiaViewModel> ReadSynexia();
        List<sqlEpidomaXorigisiViewModel> ReadXorigisi();
    }
}