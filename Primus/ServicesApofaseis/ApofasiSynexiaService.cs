﻿using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class ApofasiSynexiaService : IApofasiSynexiaService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public ApofasiSynexiaService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiSynexeiaGridViewModel> Read()
        {
            var data = (from d in entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSynexeiaGridViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
                        }).ToList();

            return (data);
        }

        public void Create(ApofasiSynexeiaGridViewModel data)
        {
            ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ entity = new ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ()
            {
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ",
                ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΗ = data.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ),
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
            };

            entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΦΑΣΗ_ΚΩΔ = entity.ΑΠΟΦΑΣΗ_ΚΩΔ;
        }

        public void Update(ApofasiSynexeiaGridViewModel data)
        {
            ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ entity = entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΧΟΡΗΓΗΣΗ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = data.ΑΠΟΦΑΣΗ_ΚΩΔ;
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ApofasiSynexeiaGridViewModel data)
        {
            ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ entity = entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public ApofasiSynexeiaGridViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Select(d => new ApofasiSynexeiaGridViewModel
            {
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
            }).Where(d => d.ΑΠΟΦΑΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public ApofasiSynexeiaViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == entityId
                        select new ApofasiSynexeiaViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΑΔΑ = d.ΑΔΑ,
                            ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ,
                            ΑΠΟΦΑΣΕΙΣ_ΔΣ = d.ΑΠΟΦΑΣΕΙΣ_ΔΣ,
                            ΑΠΟΦΑΣΗ_ΔΣ = d.ΑΠΟΦΑΣΗ_ΔΣ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                            ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ,
                            ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = d.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = d.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΚΑΕ = d.ΚΑΕ,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = d.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ,
                            ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = d.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ,
                            ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = d.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ,
                            ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = d.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ,
                            ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = d.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ,
                            ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = d.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ,
                            ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = d.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ,
                            ΣΤΟ_ΟΡΘΟ = d.ΣΤΟ_ΟΡΘΟ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΕΓΓΡΑΦΟ = d.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΥΠΟΓΡΑΦΩΝ = d.ΥΠΟΓΡΑΦΩΝ,
                            SHOW_STAR = d.SHOW_STAR,
                            ΑΠΟΦΑΣΕΙΣ_ΣΧΕΤΙΚΕΣ = d.ΑΠΟΦΑΣΕΙΣ_ΣΧΕΤΙΚΕΣ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(ApofasiSynexeiaViewModel data, int apofasiId)
        {
            ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ entity = entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ.Find(apofasiId);

            entity.ΑΔΑ = data.ΑΔΑ;
            entity.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
            entity.ΑΠΟΦΑΣΗ_ΔΣ = data.ΑΠΟΦΑΣΗ_ΔΣ;
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ";
            entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;
            entity.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = data.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΚΑΕ = data.ΚΑΕ;
            entity.ΠΛΗΘΟΣ = data.ΠΛΗΘΟΣ;
            entity.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
            entity.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;
            entity.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
            entity.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
            entity.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
            entity.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = data.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ;
            entity.ΣΤΟ_ΟΡΘΟ = data.ΣΤΟ_ΟΡΘΟ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ = data.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΥΠΟΓΡΑΦΩΝ = data.ΥΠΟΓΡΑΦΩΝ;
            entity.SHOW_STAR = data.SHOW_STAR;
            entity.ΑΠΟΦΑΣΕΙΣ_ΣΧΕΤΙΚΕΣ = data.ΑΠΟΦΑΣΕΙΣ_ΣΧΕΤΙΚΕΣ;
            entity.ΓΕΝΙΚΟΣ = Common.LoadGenikos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΔΙΕΥΘΥΝΤΗΣ = Common.LoadDirector(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΔΙΟΙΚΗΤΗΣ = Common.LoadDioikitis(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΑΝΤΙΠΡΟΕΔΡΟΣ = Common.LoadAntiproedros(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΠΡΟΙΣΤΑΜΕΝΟΣ = Common.LoadProistamenos(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}