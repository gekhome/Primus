using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class Aitisi2Service : IAitisi2Service, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public Aitisi2Service(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<Aitisi2GridViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new Aitisi2GridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ = d.ΤΑΞΗ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            return data;
        }

        public void Create(Aitisi2GridViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 entity = new ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΣΧΟΛΗ = schoolId,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΤΑΞΗ = data.ΤΑΞΗ,
                ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                ΣΤΕΓΑΣΗ_ΠΟΣΟ = 0,
                ΣΙΤΙΣΗ_ΠΟΣΟ = Common.GetSitisiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ)
            };
            entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(Aitisi2GridViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΤΑΞΗ = data.ΤΑΞΗ;
            entity.ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = 0;
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = Common.GetSitisiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(Aitisi2GridViewModel data)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public Aitisi2GridViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Select(d => new Aitisi2GridViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΤΑΞΗ = d.ΤΑΞΗ,
                ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public Aitisi2ViewModel GetRecord(int aitisiId)
        {
            var data = (from a in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                        where a.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ, a.ΤΑΞΗ, a.ΜΑΘΗΤΗΣ_ΚΩΔ
                        select new Aitisi2ViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = a.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = a.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΣΧΟΛΗ = a.ΣΧΟΛΗ,
                            ΑΞΙΟΛΟΓΗΣΗ = a.ΑΞΙΟΛΟΓΗΣΗ,
                            ΑΠΟΡΡΙΨΗ = a.ΑΠΟΡΡΙΨΗ,
                            ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = a.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = a.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΗΛΙΚΙΑ = a.ΗΛΙΚΙΑ,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = a.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = a.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = a.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = a.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                            ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = a.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                            ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = a.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = a.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = a.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΕΙΑ = a.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = a.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ = a.ΤΑΞΗ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(Aitisi2ViewModel data, StudentViewModel student, int aitisiId, int schoolId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Find(aitisiId);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΤΑΞΗ = data.ΤΑΞΗ;
            entity.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = "";    // clear existing text
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            entity.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = data.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ;
            entity.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = data.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ;
            entity.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = data.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ;
            entity.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = data.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ;
            entity.ΗΛΙΚΙΑ = Common.CalculateAge2(data, student);
            entity.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = data.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ;
            entity.ΣΥΝΕΧΕΙΑ = false;
            entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ;
            entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = 0;
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = data.ΣΙΤΙΣΗ_ΠΟΣΟ;
            entity.ΑΞΙΟΛΟΓΗΣΗ = data.ΑΞΙΟΛΟΓΗΣΗ;
            entity.ΑΠΟΡΡΙΨΗ = data.ΑΠΟΡΡΙΨΗ;
            entity.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = Common.BuildAitisi2SocialGroupsText(aitisiId);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}