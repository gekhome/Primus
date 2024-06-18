using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class Aitisi2SocialService : IAitisi2SocialService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public Aitisi2SocialService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<Aitisi2SocialGroupViewModel> Read(int aitisiId)
        {
            var data = (from d in entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new Aitisi2SocialGroupViewModel
                        {
                            AITISI_SOCIALID = d.AITISI_SOCIALID,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΚΡΙΤΗΡΙΟ_ΚΩΔ = d.ΚΡΙΤΗΡΙΟ_ΚΩΔ
                        }).ToList();
            return data;
        }

        public void Create(Aitisi2SocialGroupViewModel data, int aitisiId)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 entity = new ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2()
            {
                ΑΙΤΗΣΗ_ΚΩΔ = aitisiId,
                ΚΡΙΤΗΡΙΟ_ΚΩΔ = data.ΚΡΙΤΗΡΙΟ_ΚΩΔ,
            };
            entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Add(entity);
            entities.SaveChanges();

            data.AITISI_SOCIALID = entity.AITISI_SOCIALID;
        }

        public void Update(Aitisi2SocialGroupViewModel data, int aitisiId)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 entity = entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Find(data.AITISI_SOCIALID);

            entity.ΑΙΤΗΣΗ_ΚΩΔ = aitisiId;
            entity.ΚΡΙΤΗΡΙΟ_ΚΩΔ = data.ΚΡΙΤΗΡΙΟ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(Aitisi2SocialGroupViewModel data)
        {
            ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 entity = entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Find(data.AITISI_SOCIALID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Remove(entity);
                entities.SaveChanges();
            }
        }

        public Aitisi2SocialGroupViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Select(d => new Aitisi2SocialGroupViewModel
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