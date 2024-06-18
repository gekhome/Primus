using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class ParametersService : IParametersService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public ParametersService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysApofasiParametersViewModel> Read()
        {
            var data = (from d in entities.APOFASI_PARAMETERS
                        select new SysApofasiParametersViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΣΧΟΛΗ_ΕΙΔΟΣ = d.ΣΧΟΛΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = d.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ,
                            ΑΠΟΦΑΣΗ_ΔΣ = d.ΑΠΟΦΑΣΗ_ΔΣ,
                            ΑΠΟΦΑΣΕΙΣ_ΔΣ = d.ΑΠΟΦΑΣΕΙΣ_ΔΣ,
                            ΚΑΕ = d.ΚΑΕ
                        }).ToList();
            return data;
        }

        public void Create(SysApofasiParametersViewModel data)
        {
            APOFASI_PARAMETERS entity = new APOFASI_PARAMETERS()
            {
                ΣΧΟΛΗ_ΕΙΔΟΣ = data.ΣΧΟΛΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ,
                ΑΠΟΦΑΣΗ_ΔΣ = data.ΑΠΟΦΑΣΗ_ΔΣ,
                ΑΠΟΦΑΣΕΙΣ_ΔΣ = data.ΑΠΟΦΑΣΕΙΣ_ΔΣ,
                ΚΑΕ = data.ΚΑΕ
            };
            entities.APOFASI_PARAMETERS.Add(entity);
            entities.SaveChanges();

            data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
        }

        public void Update(SysApofasiParametersViewModel data)
        {
            APOFASI_PARAMETERS entity = entities.APOFASI_PARAMETERS.Find(data.ΚΩΔΙΚΟΣ);

            entity.ΚΩΔΙΚΟΣ = data.ΚΩΔΙΚΟΣ;
            entity.ΣΧΟΛΗ_ΕΙΔΟΣ = data.ΣΧΟΛΗ_ΕΙΔΟΣ;
            entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            entity.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            entity.ΑΠΟΦΑΣΗ_ΔΣ = data.ΑΠΟΦΑΣΗ_ΔΣ;
            entity.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
            entity.ΚΑΕ = data.ΚΑΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysApofasiParametersViewModel data)
        {
            APOFASI_PARAMETERS entity = entities.APOFASI_PARAMETERS.Find(data.ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.APOFASI_PARAMETERS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysApofasiParametersViewModel Refresh(int entityId)
        {
            return entities.APOFASI_PARAMETERS.Select(d => new SysApofasiParametersViewModel
            {
                ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                ΣΧΟΛΗ_ΕΙΔΟΣ = d.ΣΧΟΛΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = d.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ,
                ΑΠΟΦΑΣΗ_ΔΣ = d.ΑΠΟΦΑΣΗ_ΔΣ,
                ΑΠΟΦΑΣΕΙΣ_ΔΣ = d.ΑΠΟΦΑΣΕΙΣ_ΔΣ,
                ΚΑΕ = d.ΚΑΕ
            }).Where(d => d.ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}