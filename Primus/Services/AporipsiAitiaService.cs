using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class AporipsiAitiaService : IAporipsiAitiaService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public AporipsiAitiaService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AporipsiAitiaViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ
                        orderby d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ
                        select new AporipsiAitiaViewModel
                        {
                            ΑΠΟΡΡΙΨΗ_ΚΩΔ = d.ΑΠΟΡΡΙΨΗ_ΚΩΔ,
                            ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
                        }).ToList();
            return data;
        }

        public void Create(AporipsiAitiaViewModel data)
        {
            ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ entity = new ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ()
            {
                ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = data.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
            };

            entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΡΡΙΨΗ_ΚΩΔ = entity.ΑΠΟΡΡΙΨΗ_ΚΩΔ;
        }

        public void Update(AporipsiAitiaViewModel data)
        {
            ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ entity = entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Find(data.ΑΠΟΡΡΙΨΗ_ΚΩΔ);

            entity.ΑΠΟΡΡΙΨΗ_ΚΩΔ = data.ΑΠΟΡΡΙΨΗ_ΚΩΔ;
            entity.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = data.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AporipsiAitiaViewModel data)
        {
            ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ entity = entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Find(data.ΑΠΟΡΡΙΨΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AporipsiAitiaViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Select(d => new AporipsiAitiaViewModel
            {
                ΑΠΟΡΡΙΨΗ_ΚΩΔ = d.ΑΠΟΡΡΙΨΗ_ΚΩΔ,
                ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
            }).Where(d => d.ΑΠΟΡΡΙΨΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}