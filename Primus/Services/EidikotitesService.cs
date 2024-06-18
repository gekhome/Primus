using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EidikotitesService : IEidikotitesService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EidikotitesService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EidikotitesViewModel> Read()
        {
            var data = (from d in entities.ΕΙΔΙΚΟΤΗΤΕΣ
                        orderby d.ΣΥΣ_ΣΧΟΛΕΣ_ΕΙΔΗ.SCHOOLTYPE_TEXT, d.EIDIKOTITA_TEXT
                        select new EidikotitesViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_TYPE = d.SCHOOL_TYPE
                        }).ToList();
            return data;
        }

        public void Create(EidikotitesViewModel data)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ entity = new ΕΙΔΙΚΟΤΗΤΕΣ()
            {
                EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT,
                SCHOOL_TYPE = data.SCHOOL_TYPE
            };

            entities.ΕΙΔΙΚΟΤΗΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.EIDIKOTITA_ID = entity.EIDIKOTITA_ID;
        }

        public void Update(EidikotitesViewModel data)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.EIDIKOTITA_ID);

            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;
            entity.EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT;
            entity.SCHOOL_TYPE = data.SCHOOL_TYPE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EidikotitesViewModel data)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.EIDIKOTITA_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΙΔΙΚΟΤΗΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public EidikotitesViewModel Refresh(int entityId)
        {
            return entities.ΕΙΔΙΚΟΤΗΤΕΣ.Select(d => new EidikotitesViewModel
            {
                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                SCHOOL_TYPE = d.SCHOOL_TYPE
            }).Where(d => d.EIDIKOTITA_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}