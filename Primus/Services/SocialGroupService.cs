using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class SocialGroupService : ISocialGroupService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public SocialGroupService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SocialGroupViewModel> Read()
        {
            var data = (from d in entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ
                        orderby d.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ
                        select new SocialGroupViewModel
                        {
                            ΚΟΙΝΩΝΙΚΟ_ΚΩΔ = d.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ,
                            ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ = d.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ
                        }).ToList();
            return data;
        }

        public void Create(SocialGroupViewModel data)
        {
            ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ entity = new ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ()
            {
                ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ = data.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ,
            };

            entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ = entity.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ;
        }

        public void Update(SocialGroupViewModel data)
        {
            ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ entity = entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Find(data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ);

            entity.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ = data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ;
            entity.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ = data.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SocialGroupViewModel data)
        {
            ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ entity = entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Find(data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SocialGroupViewModel Refresh(int entityId)
        {
            return entities.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Select(d => new SocialGroupViewModel
            {
                ΚΟΙΝΩΝΙΚΟ_ΚΩΔ = d.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ,
                ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ = d.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ
            }).Where(d => d.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}