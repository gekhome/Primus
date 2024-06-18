using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Primus.DAL;
using Primus.Models;
using Primus.BPM;

namespace Primus.BPM
{
    public static class Kerberos
    {
        public const int TICKET_TIMEOUT_MINUTES = 240;

        /// <summary>
        /// Υπολογίζει τις εργάσιμες ημέρες μεταξύ δύο ημερομηνιών,
        /// δηλ. χωρίς τα Σαββατοκύριακα.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="daycount"></returns>
        public static int WorkingDays(DateTime initial_date, DateTime final_date)
        {
            int daycount = 0;

            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            while (date1 <= date2)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daycount++;
                        break;
                    default:
                        break;
                }
                date1 = date1.AddDays(1);
            }
            return daycount;
        }


        #region PRIMARY KEY VALIDATORS

        public static bool StudentAitisiExists(AitisiGridViewModel a, int studentId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ
                            where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == a.ΣΧΟΛΙΚΟ_ΕΤΟΣ && d.ΤΑΞΗ == a.ΤΑΞΗ
                            select d).Count();
                if (data > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool StudentAitisi2Exists(Aitisi2GridViewModel a, int studentId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                            where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == a.ΣΧΟΛΙΚΟ_ΕΤΟΣ && d.ΤΑΞΗ == a.ΤΑΞΗ
                            select d).Count();
                if (data > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool ValidatePrimaryKeyStudent(string afm, int school, int eidikotita)
        {
            using (var db = new PrimusDBEntities())
            {
                var existingdata = db.ΜΑΘΗΤΗΣ.Where(s => s.ΑΦΜ == afm && s.ΣΧΟΛΗ == school && s.ΕΙΔΙΚΟΤΗΤΑ == eidikotita).Count();
                if (existingdata == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteStudent(int studentId)
        {
            using (var db = new PrimusDBEntities())
            {
                var existingdata1 = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Where(s => s.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId).Count();
                var existingdata2 = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2.Where(s => s.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId).Count();

                if (existingdata1 == 0 && existingdata2 == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteEidikotita(int eidikotitaId)
        {
            using (var db = new PrimusDBEntities())
            {
                var existingdata1 = db.ΜΑΘΗΤΗΣ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ == eidikotitaId).Count();
                var existingdata2 = db.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Where(s => s.EIDIKOTITA_ID == eidikotitaId).Count();

                if (existingdata1 == 0 && existingdata2 == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteAitisi(int aitisiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var existingdata = db.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ.Where(s => s.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId).Count();
                if (existingdata == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteAitisi2(int aitisiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var existingdata = db.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2.Where(s => s.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId).Count();
                if (existingdata == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteSchoolEidikotita(int eidikotitaId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΜΑΘΗΤΗΣ where d.ΕΙΔΙΚΟΤΗΤΑ == eidikotitaId select d).Count();
                if (count > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanDeleteUpload(int uploadId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();
                if (data > 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion


        #region DELETE RULES

        public static bool CanDeleteApofasiSitisi2(int apofasiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (count > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanDeleteApofasiXorigisi(int apofasiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (count > 0) 
                    return false;
                else 
                    return true;
            }
        }

        public static bool CanDeleteApofasiSynexeia(int apofasiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiStegasi(int apofasiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).ToList();

                if (data.Count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteApofasiSitisi(int apofasiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).Count();

                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteSchoolEidikotita(int eidikotitaId, int schoolId)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΜΑΘΗΤΗΣ where d.ΕΙΔΙΚΟΤΗΤΑ == eidikotitaId && d.ΣΧΟΛΗ == schoolId select d).Count();
                if (count > 0) 
                    return false;
                else 
                    return true;
            }
        }

        #endregion


        #region OTHER VALIDATIONS

        public static bool StudentsExist()
        {
            using (var db = new PrimusDBEntities())
            {
                var students = (from d in db.ΜΑΘΗΤΗΣ select d).ToList();

                if (students.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool EidikotitesExist()
        {
            using (var db = new PrimusDBEntities())
            {
                var eidikotites = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ select d).ToList();

                if (eidikotites.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

    }
}