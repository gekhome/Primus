using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class StudentInfoService : IStudentInfoService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public StudentInfoService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentInfoViewModel> Read()
        {
            var data = (from d in entities.sqlSTUDENT_INFO
                        orderby d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new StudentInfoViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΑΦΜ = d.ΑΦΜ,
                            GENDER = d.GENDER,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                            ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = d.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = d.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = d.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                            ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = d.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ = d.ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ
                        }).ToList();

            return (data);
        }

        public IEnumerable<StudentInfoViewModel> Read(int schoolId)
        {
            var data = (from d in entities.sqlSTUDENT_INFO
                        where d.ΣΧΟΛΗ == schoolId
                        orderby d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new StudentInfoViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΑΦΜ = d.ΑΦΜ,
                            GENDER = d.GENDER,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                            ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = d.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = d.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = d.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                            ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = d.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ = d.ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ
                        }).ToList();

            return (data);
        }

        public StudentInfoViewModel GetRecord(int entityId)
        {
            return entities.sqlSTUDENT_INFO.Select(d => new StudentInfoViewModel
            {
                ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                ΑΦΜ = d.ΑΦΜ,
                GENDER = d.GENDER,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                SCHOOL_NAME = d.SCHOOL_NAME,
                ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = d.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = d.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = d.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = d.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ = d.ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ
            }).Where(d => d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public IEnumerable<sqlAitiseisViewModel> ReadAitiseis(int studentId)
        {
            var data = (from d in entities.sqlSTUDENT_AITISEIS
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == studentId
                        orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
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
                            ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ
                        }).ToList();
            return (data);

        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}