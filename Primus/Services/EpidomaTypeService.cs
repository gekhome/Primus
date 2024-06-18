using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EpidomaTypeService : IEpidomaTypeService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EpidomaTypeService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysEpidomaTypeViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ
                        select new SysEpidomaTypeViewModel
                        {
                            ΕΠΙΔΟΜΑ_ΚΩΔ = d.ΕΠΙΔΟΜΑ_ΚΩΔ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ = d.ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ
                        }).ToList();
            return data;
        }

        public void Create(SysEpidomaTypeViewModel data)
        {
            ΣΥΣ_ΕΠΙΔΟΜΑΤΑ entity = new ΣΥΣ_ΕΠΙΔΟΜΑΤΑ()
            {
                ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = data.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ = data.ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ
            };

            entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Add(entity);
            entities.SaveChanges();

            data.ΕΠΙΔΟΜΑ_ΚΩΔ = entity.ΕΠΙΔΟΜΑ_ΚΩΔ;
        }

        public void Update(SysEpidomaTypeViewModel data)
        {
            ΣΥΣ_ΕΠΙΔΟΜΑΤΑ entity = entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Find(data.ΕΠΙΔΟΜΑ_ΚΩΔ);

            entity.ΕΠΙΔΟΜΑ_ΚΩΔ = data.ΕΠΙΔΟΜΑ_ΚΩΔ;
            entity.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = data.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ;
            entity.ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ = data.ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysEpidomaTypeViewModel data)
        {
            ΣΥΣ_ΕΠΙΔΟΜΑΤΑ entity = entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Find(data.ΕΠΙΔΟΜΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysEpidomaTypeViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Select(d => new SysEpidomaTypeViewModel
            {
                ΕΠΙΔΟΜΑ_ΚΩΔ = d.ΕΠΙΔΟΜΑ_ΚΩΔ,
                ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ = d.ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ
            }).Where(d => d.ΕΠΙΔΟΜΑ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}