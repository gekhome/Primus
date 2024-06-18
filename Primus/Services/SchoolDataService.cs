using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class SchoolDataService : ISchoolDataService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public SchoolDataService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SchoolsGridViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΣΧΟΛΕΣ
                        orderby d.SCHOOL_TYPE, d.SCHOOL_NAME
                        select new SchoolsGridViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOL_TYPE = d.SCHOOL_TYPE,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                        }).ToList();
            return data;
        }

        public void Create(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = new ΣΥΣ_ΣΧΟΛΕΣ()
            {
                SCHOOL_NAME = data.SCHOOL_NAME,
                SCHOOL_TYPE = data.SCHOOL_TYPE,
                ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ,
                ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ
            };

            entities.ΣΥΣ_ΣΧΟΛΕΣ.Add(entity);
            entities.SaveChanges();

            data.SCHOOL_ID = entity.SCHOOL_ID;
        }

        public void Update(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = entities.ΣΥΣ_ΣΧΟΛΕΣ.Find(data.SCHOOL_ID);

            entity.SCHOOL_ID = data.SCHOOL_ID;
            entity.SCHOOL_TYPE = data.SCHOOL_TYPE;
            entity.SCHOOL_NAME = data.SCHOOL_NAME;
            entity.ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = entities.ΣΥΣ_ΣΧΟΛΕΣ.Find(data.SCHOOL_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΣΧΟΛΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SchoolsGridViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΣΧΟΛΕΣ.Select(d => new SchoolsGridViewModel
            {
                SCHOOL_ID = d.SCHOOL_ID,
                SCHOOL_NAME = d.SCHOOL_NAME,
                SCHOOL_TYPE = d.SCHOOL_TYPE,
                ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ
            }).Where(d => d.SCHOOL_ID == entityId).FirstOrDefault();
        }

        public SchoolsViewModel GetRecord(int schoolId)
        {
            SchoolsViewModel schoolData;

            schoolData = (from s in entities.ΣΥΣ_ΣΧΟΛΕΣ
                          where s.SCHOOL_ID == schoolId
                          select new SchoolsViewModel
                          {
                              SCHOOL_ID = s.SCHOOL_ID,
                              SCHOOL_NAME = s.SCHOOL_NAME,
                              SCHOOL_TYPE = s.SCHOOL_TYPE,
                              ΔΙΕΥΘΥΝΤΗΣ = s.ΔΙΕΥΘΥΝΤΗΣ,
                              ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                              ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = s.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ,
                              ΤΗΛΕΦΩΝΑ = s.ΤΗΛΕΦΩΝΑ,
                              ΓΡΑΜΜΑΤΕΙΑ = s.ΓΡΑΜΜΑΤΕΙΑ,
                              ΦΑΞ = s.ΦΑΞ,
                              EMAIL = s.EMAIL,
                              ΚΙΝΗΤΟ = s.ΚΙΝΗΤΟ,
                              ΥΠΟΔΙΕΥΘΥΝΤΗΣ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ,
                              ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                              ΠΕΡΙΦΕΡΕΙΑΚΗ = s.ΠΕΡΙΦΕΡΕΙΑΚΗ
                          }).FirstOrDefault();
            return schoolData;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}