using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EpidotisiRegistryService : IEpidotisiRegistryService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EpidotisiRegistryService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<sqlEpidomaXorigisiViewModel> ReadXorigisi()
        {
            var data = (from d in entities.sqlEPIDOMA_XORIGISI
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaXorigisiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaXorigisiViewModel GetRecordXorigisi(int epidomaId)
        {
            var data = (from d in entities.sqlEPIDOMA_XORIGISI
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == epidomaId
                        select new sqlEpidomaXorigisiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEpidomaSynexeiaViewModel> ReadSynexia()
        {
            var data = (from d in entities.sqlEPIDOMA_SYNEXEIA
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaSynexeiaViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaSynexeiaViewModel GetRecordSynexia(int epidomaId)
        {
            var data = (from d in entities.sqlEPIDOMA_SYNEXEIA
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == epidomaId
                        select new sqlEpidomaSynexeiaViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEpidomaStegasiViewModel> ReadStegasi()
        {
            var data = (from d in entities.sqlEPIDOMA_STEGASI
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaStegasiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaStegasiViewModel GetRecordStegasi(int epidomaId)
        {
            var data = (from d in entities.sqlEPIDOMA_STEGASI
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == epidomaId
                        select new sqlEpidomaStegasiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEpidomaSitisiViewModel> ReadSitisi()
        {
            var data = (from d in entities.sqlEPIDOMA_SITISI
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaSitisiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaSitisiViewModel GetRecordSitisi(int epidomaId)
        {
            var data = (from d in entities.sqlEPIDOMA_SITISI
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == epidomaId
                        select new sqlEpidomaSitisiViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEpidomaAllDetailViewModel> ReadOliko()
        {
            var data = (from d in entities.sqlEPIDOMA_ALL_DETAIL
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaAllDetailViewModel
                        {
                            ID = d.ID,
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaAllDetailViewModel GetRecordOliko(int recordId)
        {
            var data = (from d in entities.sqlEPIDOMA_ALL_DETAIL
                        where d.ID == recordId
                        select new sqlEpidomaAllDetailViewModel
                        {
                            ID = d.ID,
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public List<sqlEpidomaSitisiSchoolViewModel> ReadSitisi2()
        {
            var data = (from d in entities.sqlEPIDOMA_SITISI2
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlEpidomaSitisiSchoolViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();

            return (data);
        }

        public sqlEpidomaSitisiSchoolViewModel GetRecordSitisi2(int epidomaId)
        {
            var data = (from d in entities.sqlEPIDOMA_SITISI2
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == epidomaId
                        select new sqlEpidomaSitisiSchoolViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();

            return (data);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}