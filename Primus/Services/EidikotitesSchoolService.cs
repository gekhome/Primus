using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EidikotitesSchoolService : IEidikotitesSchoolService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EidikotitesSchoolService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EidikotitaYearSchoolViewModel> Read(int schoolyearId)
        {
            var data = (from d in entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ
                        where d.SCHOOLYEAR_ID == schoolyearId
                        orderby d.SCHOOL_ID, d.EIDIKOTITA_ID
                        select new EidikotitaYearSchoolViewModel
                        {
                            SYE_ID = d.SYE_ID,
                            SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                            SCHOOL_ID = d.SCHOOL_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                        }).ToList();

            return data;
        }

        public IEnumerable<EidikotitaYearSchoolViewModel> Read(int schoolyearId, int schoolId)
        {
            var data = (from d in entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ
                        where d.SCHOOLYEAR_ID == schoolyearId && d.SCHOOL_ID == schoolId
                        orderby d.SCHOOL_ID, d.EIDIKOTITA_ID
                        select new EidikotitaYearSchoolViewModel
                        {
                            SYE_ID = d.SYE_ID,
                            SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                            SCHOOL_ID = d.SCHOOL_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                        }).ToList();

            return data;
        }

        public void Create(EidikotitaYearSchoolViewModel data, int schoolyearId)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ entity = new ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ()
            {
                SCHOOLYEAR_ID = schoolyearId,
                SCHOOL_ID = data.SCHOOL_ID,
                EIDIKOTITA_ID = data.EIDIKOTITA_ID
            };

            entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Add(entity);
            entities.SaveChanges();

            data.SYE_ID = entity.SYE_ID;
        }

        public void Create(EidikotitaYearSchoolViewModel data, int schoolyearId, int schoolId)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ entity = new ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ()
            {
                SCHOOLYEAR_ID = schoolyearId,
                SCHOOL_ID = schoolId,
                EIDIKOTITA_ID = data.EIDIKOTITA_ID
            };

            entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Add(entity);
            entities.SaveChanges();

            data.SYE_ID = entity.SYE_ID;
        }

        public void Update(EidikotitaYearSchoolViewModel data, int schoolyearId)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ entity = entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Find(data.SYE_ID);

            entity.SCHOOLYEAR_ID = schoolyearId;
            entity.SCHOOL_ID = data.SCHOOL_ID;
            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(EidikotitaYearSchoolViewModel data, int schoolyearId, int schoolId)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ entity = entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Find(data.SYE_ID);

            entity.SCHOOLYEAR_ID = schoolyearId;
            entity.SCHOOL_ID = schoolId;
            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EidikotitaYearSchoolViewModel data)
        {
            ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ entity = entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Find(data.SYE_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public EidikotitaYearSchoolViewModel Refresh(int entityId)
        {
            return entities.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Select(d => new EidikotitaYearSchoolViewModel
            {
                SYE_ID = d.SYE_ID,
                SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                SCHOOL_ID = d.SCHOOL_ID,
                EIDIKOTITA_ID = d.EIDIKOTITA_ID
            }).Where(d => d.SYE_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}