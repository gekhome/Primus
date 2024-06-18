using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class SchoolYearService : ISchoolYearService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public SchoolYearService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysSchoolYearViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                        orderby d.SCHOOLYEAR_TEXT
                        select new SysSchoolYearViewModel
                        {
                            SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            DATE_START = d.DATE_START,
                            DATE_END = d.DATE_END,
                        }).ToList();

            return data;
        }

        public void Create(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = new ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ()
            {
                SCHOOLYEAR_TEXT = data.SCHOOLYEAR_TEXT,
                DATE_START = data.DATE_START,
                DATE_END = data.DATE_END,
            };

            entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Add(entity);
            entities.SaveChanges();

            data.SCHOOLYEAR_ID = entity.SCHOOLYEAR_ID;
        }

        public void Update(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Find(data.SCHOOLYEAR_ID);

            entity.SCHOOLYEAR_ID = data.SCHOOLYEAR_ID;
            entity.SCHOOLYEAR_TEXT = data.SCHOOLYEAR_TEXT;
            entity.DATE_START = data.DATE_START;
            entity.DATE_END = data.DATE_END;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Find(data.SCHOOLYEAR_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysSchoolYearViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Select(d => new SysSchoolYearViewModel
            {
                SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                DATE_START = d.DATE_START,
                DATE_END = d.DATE_END
            }).Where(d => d.SCHOOLYEAR_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}