using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EpidomaPosoService : IEpidomaPosoService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EpidomaPosoService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EpidomaPosoViewModel> Read()
        {
            var data = (from d in entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ
                        select new EpidomaPosoViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΚΑΕ = d.ΚΑΕ,
                            ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = d.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ,
                            ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = d.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ,
                            ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = d.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ,
                            ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = d.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ
                        }).ToList();
            return data;
        }

        public void Create(EpidomaPosoViewModel data)
        {
            ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ entity = new ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ()
            {
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΚΑΕ = data.ΚΑΕ,
                ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ,
                ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ,
                ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ,
                ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ
            };

            entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Add(entity);
            entities.SaveChanges();

            data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = entity.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ;
        }

        public void Update(EpidomaPosoViewModel data)
        {
            ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ entity = entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            entity.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΚΑΕ = data.ΚΑΕ;
            entity.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
            entity.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
            entity.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
            entity.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EpidomaPosoViewModel data)
        {
            ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ entity = entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            entities.Entry(entity).State = EntityState.Deleted;
            entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Remove(entity);
            entities.SaveChanges();
        }

        public EpidomaPosoViewModel Refresh(int entityId)
        {
            return entities.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Select(d => new EpidomaPosoViewModel
            {
                ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΚΑΕ = d.ΚΑΕ,
                ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = d.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ,
                ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = d.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ,
                ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = d.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ,
                ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = d.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ
            }).Where(d => d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}