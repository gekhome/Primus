using Primus.Models;
using System.Collections.Generic;

namespace Primus.Services
{
    public interface IApofaseisRegistryService
    {
        List<EpidomaRegistryViewModel> GetEpidomata(ApofasiParameters ap);
        ApofasiRegistryViewModel GetRecord(ApofasiParameters ap);
        IEnumerable<ApofasiRegistryViewModel> Read();
    }
}