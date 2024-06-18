using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class ApofaseisRegistryService : IApofaseisRegistryService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public ApofaseisRegistryService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiRegistryViewModel> Read()
        {
            var data = (from d in entities.sqlΑΠΟΦΑΣΕΙΣ_ΜΗΤΡΩΟ
                        orderby d.SCHOOLYEAR_TEXT descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new ApofasiRegistryViewModel
                        {
                            ID = d.ID,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = d.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ,
                            ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = d.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ
                        }).ToList();
            return (data);
        }

        public ApofasiRegistryViewModel GetRecord(ApofasiParameters ap)
        {
            var data = (from d in entities.sqlΑΠΟΦΑΣΕΙΣ_ΜΗΤΡΩΟ
                        orderby d.SCHOOLYEAR_TEXT descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType && d.SCHOOLYEAR_TEXT == ap.schoolyear && d.SCHOOL_NAME == ap.school
                        select new ApofasiRegistryViewModel
                        {
                            ID = d.ID,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = d.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ,
                            ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = d.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ
                        }).FirstOrDefault();
            return data;
        }

        public List<EpidomaRegistryViewModel> GetEpidomata(ApofasiParameters ap)
        {
            var data = (from d in entities.sqlΕΠΙΔΟΜΑ_ΜΗΤΡΩΟ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId && d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ == ap.apofasiType && d.SCHOOLYEAR_TEXT == ap.schoolyear && d.SCHOOL_NAME == ap.school
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new EpidomaRegistryViewModel
                        {
                            ID = d.ID,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            AITISI_ID = d.AITISI_ID,
                            PERIFERIAKI_ID = d.PERIFERIAKI_ID,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = d.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΤΑΞΗ = d.ΜΑΘΗΤΗΣ_ΤΑΞΗ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΙΣΗ = d.ΣΥΝΕΧΙΣΗ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                        }).ToList();

            return (data);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}