using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class AitisiSocialService : IAitisiSocialService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public AitisiSocialService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AitisiSocialGroupViewModel> Read(int aitisiId)
        {
            var data = (from d in entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new AitisiSocialGroupViewModel
                        {
                            AITISI_SOCIALID = d.AITISI_SOCIALID,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΚΡΙΤΗΡΙΟ_ΚΩΔ = d.ΚΡΙΤΗΡΙΟ_ΚΩΔ
                        }).ToList();
            return (data);
        }

        public void Create(AitisiSocialGroupViewModel data, int aitisiId)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ entity = new ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ()
            {
                ΑΙΤΗΣΗ_ΚΩΔ = aitisiId,
                ΚΡΙΤΗΡΙΟ_ΚΩΔ = data.ΚΡΙΤΗΡΙΟ_ΚΩΔ,
            };
            entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Add(entity);
            entities.SaveChanges();

            data.AITISI_SOCIALID = entity.AITISI_SOCIALID;
        }

        public void Update(AitisiSocialGroupViewModel data, int aitisiId)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ entity = entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Find(data.AITISI_SOCIALID);

            entity.ΑΙΤΗΣΗ_ΚΩΔ = aitisiId;
            entity.ΚΡΙΤΗΡΙΟ_ΚΩΔ = data.ΚΡΙΤΗΡΙΟ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AitisiSocialGroupViewModel data)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ entity = entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Find(data.AITISI_SOCIALID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AitisiSocialGroupViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Select(d => new AitisiSocialGroupViewModel
            {
                AITISI_SOCIALID = d.AITISI_SOCIALID,
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΚΡΙΤΗΡΙΟ_ΚΩΔ = d.ΚΡΙΤΗΡΙΟ_ΚΩΔ
            }).Where(d => d.AITISI_SOCIALID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}