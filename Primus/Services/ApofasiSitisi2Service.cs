using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class ApofasiSitisi2Service : IApofasiSitisi2Service, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public ApofasiSitisi2Service(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiSitisi2ViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2
                        where d.ΣΧΟΛΗ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSitisi2ViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΜΗΝΑΣ = d.ΜΗΝΑΣ,
                            ΕΤΟΣ = d.ΕΤΟΣ
                        }).ToList();
            return data;
        }

        public void Create(ApofasiSitisi2ViewModel data, int schoolId)
        {
            ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 entity = new ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2()
            {
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ",
                ΔΙΑΧΕΙΡΙΣΤΗΣ = Common.GetSchoolDirector(),
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΗ = schoolId,
                ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType(schoolId),
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΜΗΝΑΣ = data.ΜΗΝΑΣ,
                ΕΤΟΣ = Common.GetYearFromSchoolYearMonth((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (int)data.ΜΗΝΑΣ)
            };
            entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΦΑΣΗ_ΚΩΔ = entity.ΑΠΟΦΑΣΗ_ΚΩΔ;
        }

        public void Update(ApofasiSitisi2ViewModel data, int schoolId)
        {
            ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 entity = entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ";
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = Common.GetSchoolDirector();
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType(schoolId);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΜΗΝΑΣ = data.ΜΗΝΑΣ;
            entity.ΕΤΟΣ = Common.GetYearFromSchoolYearMonth((int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (int)data.ΜΗΝΑΣ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ApofasiSitisi2ViewModel data)
        {
            ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 entity = entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public ApofasiSitisi2ViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Select(d => new ApofasiSitisi2ViewModel
            {
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΜΗΝΑΣ = d.ΜΗΝΑΣ,
                ΕΤΟΣ = d.ΕΤΟΣ
            }).Where(d => d.ΑΠΟΦΑΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public ApofasiSitisi2ViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == entityId
                        select new ApofasiSitisi2ViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΕΓΚΥΚΛΙΟΙ_Α2 = d.ΕΓΚΥΚΛΙΟΙ_Α2,
                            ΠΡΑΚΤΙΚΑ_ΣΧΟΛΕΙΟ = d.ΠΡΑΚΤΙΚΑ_ΣΧΟΛΕΙΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = d.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = d.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = d.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ,
                            ΣΤΟ_ΟΡΘΟ = d.ΣΤΟ_ΟΡΘΟ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΤΟΣ = d.ΕΤΟΣ,
                            ΜΗΝΑΣ = d.ΜΗΝΑΣ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(ApofasiSitisi2ViewModel model, int apofasiId, int schoolId)
        {
            ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 entity = entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2.Find(apofasiId);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ";
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = model.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = model.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = model.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
            entity.ΕΓΚΥΚΛΙΟΙ_Α2 = model.ΕΓΚΥΚΛΙΟΙ_Α2;
            entity.ΠΡΑΚΤΙΚΑ_ΣΧΟΛΕΙΟ = model.ΠΡΑΚΤΙΚΑ_ΣΧΟΛΕΙΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = model.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = model.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = model.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΠΛΗΘΟΣ = model.ΠΛΗΘΟΣ;
            entity.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = model.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = model.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = model.ΣΙΤΙΣΗ_ΠΟΣΟ;
            entity.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = model.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
            entity.ΣΤΟ_ΟΡΘΟ = model.ΣΤΟ_ΟΡΘΟ;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType(schoolId);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = model.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΜΗΝΑΣ = model.ΜΗΝΑΣ;
            entity.ΕΤΟΣ = Common.GetYearFromSchoolYearMonth((int)model.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (int)model.ΜΗΝΑΣ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}