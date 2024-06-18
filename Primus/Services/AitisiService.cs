using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class AitisiService : IAitisiService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public AitisiService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AitisiGridViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new AitisiGridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ = d.ΤΑΞΗ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ
                        }).ToList();
            return (data);
        }

        public void Create(AitisiGridViewModel data, int studentId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = new ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΤΑΞΗ = data.ΤΑΞΗ,
                ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                ΣΤΕΓΑΣΗ_ΠΟΣΟ = Common.GetStegasiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ),
                ΣΙΤΙΣΗ_ΠΟΣΟ = Common.GetSitisiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ)
            };
            entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(AitisiGridViewModel data, int studentId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΤΑΞΗ = data.ΤΑΞΗ;
            entity.ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = Common.GetStegasiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = Common.GetSitisiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AitisiGridViewModel data)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public AitisiGridViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Select(d => new AitisiGridViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΤΑΞΗ = d.ΤΑΞΗ,
                ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public AitisiViewModel GetRecord(int aitisiId)
        {
            var data = (from a in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ
                        where a.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ, a.ΤΑΞΗ, a.ΜΑΘΗΤΗΣ_ΚΩΔ
                        select new AitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = a.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = a.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
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
            return (data);
        }

        public void UpdateRecord(AitisiViewModel data, StudentViewModel student, int aitisiId, bool admin = false)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(aitisiId);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΤΑΞΗ = data.ΤΑΞΗ;
            entity.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = "";    // clear existing text
            entity.ΗΜΝΙΑ_ΑΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            entity.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = data.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ;
            entity.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = data.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ;
            entity.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = data.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ;
            entity.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = data.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ;
            entity.ΗΛΙΚΙΑ = Common.CalculateAge(data, student);
            entity.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = data.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ;
            entity.ΣΥΝΕΧΕΙΑ = data.ΣΥΝΕΧΕΙΑ;
            entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ;
            if (admin == true)
            {
                entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = (data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == 2 || data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == 4) ? 0 : data.ΣΤΕΓΑΣΗ_ΠΟΣΟ;
                entity.ΣΙΤΙΣΗ_ΠΟΣΟ = data.ΣΙΤΙΣΗ_ΠΟΣΟ;
            }
            else
            {
                entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = (data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == 2 || data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == 4) ? 0 : Common.GetStegasiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
                entity.ΣΙΤΙΣΗ_ΠΟΣΟ = Common.GetSitisiPoso((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            }
            entity.ΑΞΙΟΛΟΓΗΣΗ = data.ΑΞΙΟΛΟΓΗΣΗ;
            entity.ΑΠΟΡΡΙΨΗ = data.ΑΠΟΡΡΙΨΗ;
            entity.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = Common.BuildAitisiSocialGroupsText(aitisiId);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}