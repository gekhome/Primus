using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class AitisiInfoService : IAitisiInfoService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public AitisiInfoService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<sqlAitiseisViewModel> Read(int schoolyearId = 0)
        {
            List<sqlAitiseisViewModel> data = new List<sqlAitiseisViewModel>();
            if (schoolyearId > 0)
            {
                data = (from d in entities.sqlSTUDENT_AITISEIS
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                        select new sqlAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            }
            else
            {
                data = (from d in entities.sqlSTUDENT_AITISEIS
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                        select new sqlAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            }
            return data;
        }

        public IEnumerable<sqlAitiseisViewModel> Read(int schoolId, int schoolyearId = 0)
        {
            List<sqlAitiseisViewModel> data = new List<sqlAitiseisViewModel>();
            if (schoolyearId > 0)
            {
                data = (from d in entities.sqlSTUDENT_AITISEIS
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                        select new sqlAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            }
            else
            {
                data = (from d in entities.sqlSTUDENT_AITISEIS
                        where d.ΣΧΟΛΗ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            }
            return data;
        }

        public sqlAitiseisViewModel GetRecord(int entityId)
        {
            var aitisi = (from d in entities.sqlSTUDENT_AITISEIS where d.ΑΙΤΗΣΗ_ΚΩΔ == entityId select d).FirstOrDefault();
            var student = (from d in entities.sqlSTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == aitisi.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var data = (from d in entities.sqlSTUDENT_AITISEIS
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == entityId
                        orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                            ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                            ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = student.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = student.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = student.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = student.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = student.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = student.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = student.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = student.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = student.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = student.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ = student.ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ
                        }).FirstOrDefault();

            return (data);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}