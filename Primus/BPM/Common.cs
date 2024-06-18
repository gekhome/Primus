using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Primus.DAL;
using Primus.Models;
using Primus.BPM;
using Primus.Notification;

namespace Primus.BPM
{
    public static class Common
    {

        #region STRING FUNCTIONS (equivalent to VB)

        public static string Right(string text, int numberCharacters)
        {
            return text.Substring(numberCharacters > text.Length ? 0 : text.Length - numberCharacters);
        }

        public static string Left(string text, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length <= length)
                return text;
            else
                return text.Substring(0, length);
        }
        public static int Len(string text)
        {
            int _length;
            _length = text.Length;
            return _length;
        }
        public static byte Asc(string src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src + "")[0]);
        }
        public static char Chr(byte src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] { src })[0]);
        }
        public static bool isNumber(string param)
        {
            Regex isNum = new Regex("[^0-9]");
            return !isNum.IsMatch(param);
        }

        #endregion


        #region DATE FUNCTIONS

        public static int DayOfWeek(int day, int month, int year)
        {
            string strDate = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            DateTime theDate;
            int weekday;

            if (DateTime.TryParse(strDate, out theDate) == true)
            {
                theDate = Convert.ToDateTime(strDate);
                weekday = (int)theDate.DayOfWeek;
            }
            else weekday = -1;

            return (weekday);
        }


        /// <summary>
        /// Μετατρέπει τον αριθμό του μήνα σε λεκτικό
        /// στη γενική πτώση.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string monthToGRstring(int m)
        {
            string stGRmonth = "";

            switch (m)
            {
                case 1: stGRmonth = "Ιανουαρίου"; break;
                case 2: stGRmonth = "Φεβρουαρίου"; break;
                case 3: stGRmonth = "Μαρτίου"; break;
                case 4: stGRmonth = "Απριλίου"; break;
                case 5: stGRmonth = "Μαϊου"; break;
                case 6: stGRmonth = "Ιουνίου"; break;
                case 7: stGRmonth = "Ιουλίου"; break;
                case 8: stGRmonth = "Αυγούστου"; break;
                case 9: stGRmonth = "Σεπτεμβρίου"; break;
                case 10: stGRmonth = "Οκτωβρίου"; break;
                case 11: stGRmonth = "Νοεμβρίου"; break;
                case 12: stGRmonth = "Δεκεμβρίου"; break;
                default: break;
            }
            return stGRmonth;
        }

        /// <summary>
        /// Ελέγχει αν η αρχική ημερομηνία είναι μικρότερη
        /// ή ίση με την τελική ημερομηνία.
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static bool ValidStartEndDates(DateTime dateStart, DateTime dateEnd)
        {
            bool result;

            if (dateStart > dateEnd)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες ανήκουν στο ίδιο έτος.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool DatesInSameYear(DateTime date1, DateTime date2)
        {
            bool result;

            if (date1.Year != date2.Year)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες είναι μέσα στο ίδιο Σχ. Έτος
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="schoolYearID"></param>
        /// <returns></returns>
        public static bool DatesInSchoolYear(DateTime dateStart, DateTime dateEnd, int schoolYearID)
        {
            bool result = true;

            using (var db = new PrimusDBEntities())
            {
                var schoolYear = (from s in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                                  where s.SCHOOLYEAR_ID == schoolYearID
                                  select new { s.DATE_START, s.DATE_END }).FirstOrDefault();

                if (dateStart < schoolYear.DATE_START || dateEnd > schoolYear.DATE_END)
                    result = false;

                return result;
            }
        }

        public static bool DateInSchoolYear(DateTime theDate, int schoolYearID)
        {
            bool result = true;

            using (var db = new PrimusDBEntities())
            {
                var schoolYear = (from s in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                                  where s.SCHOOLYEAR_ID == schoolYearID
                                  select new { s.DATE_START, s.DATE_END }).FirstOrDefault();

                if (theDate < schoolYear.DATE_START || theDate > schoolYear.DATE_END)
                    result = false;

                return result;
            }
        }


        /// <summary>
        /// Ελέγχει αν το σχολικό έτος έχει τη μορφή ΝΝΝΝ-ΝΝΝΝ
        /// και αν τα έτη είναι συμβατά με τις ημερομηνίες
        /// έναρξης και λήξης.
        /// </summary>
        /// <param name="syear"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool VerifySchoolYear(string syear, DateTime d1, DateTime d2)
        {
            if (syear.IndexOf('-') == -1)
            {
                return false;
            }

            string[] split = syear.Split(new Char[] { '-' });
            string sy1 = Convert.ToString(split[0]);
            string sy2 = Convert.ToString(split[1]);

            if (!isNumber(sy1) || !isNumber(sy2))
            {
                return false;
            }
            else
            {
                int y1 = Convert.ToInt32(sy1);
                int y2 = Convert.ToInt32(sy2);

                if (y2 - y1 > 1 || y2 - y1 <= 0)
                {
                    return false;
                }
                if (d1.Year != y1 || d2.Year != y2)
                {
                    return false;
                }
            }
            // at this point everything is ok
            return true;
        }

        /// <summary>
        /// Ελέγχει αν το χολικό έτος μορφής ΝΝΝΝ-ΝΝΝΝ υπάρχει ήδη.
        /// </summary>
        /// <param name="syear"></param>
        /// <returns></returns>
        public static bool SchoolYearExists(int syear)
        {
            using (var db = new PrimusDBEntities())
            {
                var syear_recs = (from s in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                                  where s.SCHOOLYEAR_ID == syear
                                  select s).Count();

                if (syear_recs != 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Υπολογίζει τα έτη (στρογγυλοποιημένα) μεταξύ δύο ημερομηνιών
        /// </summary>
        /// <param name="sdate">αρχική ημερομηνία</param>
        /// <param name="edate">τελική ημερομηνία</param>
        /// <returns></returns>
        public static int YearsDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            double _years = days / 365;

            int years = Convert.ToInt32(Math.Ceiling(_years));

            return years;
        }

        public static int DaysDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            return days;
        }

        #endregion


        #region GENERAL FUNCTIONS

        public static float Max(params float[] values)
        {
            return Enumerable.Max(values);
        }

        public static float Min(params float[] values)
        {
            return Enumerable.Min(values);
        }

        /// <summary>
        /// Υπολογίζει τις ημέρες λογιστικού έτους μεταξύ δύο ημερομηνιών,
        /// προσομειώνοντας τη συνάρτηση Days360 του Excel.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="meres"></returns>
        public static int Days360(DateTime initial_date, DateTime final_date)
        {
            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            var y1 = date1.Year;
            var y2 = date2.Year;
            var m1 = date1.Month;
            var m2 = date2.Month;
            var d1 = date1.Day;
            var d2 = date2.Day;

            DateTime tempDate = date1.AddDays(1);
            if (tempDate.Day == 1 && date1.Month == 2)
            {
                d1 = 30;
            }
            if (d2 == 31 && d1 == 30)
            {
                d2 = 30;
            }

            double meres = (y2 - y1) * 360 + (m2 - m1) * 30 + (d2 - d1);
            meres = (meres / 30) * 25;
            meres = Math.Ceiling(meres);

            return Convert.ToInt32(meres);
        }

        #endregion


        #region PROTOCOL GENERATOR

        public static string Get8Digits()
        {
            //var bytes = new byte[4];
            //var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(bytes);
            //uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            //return String.Format("{0:D8}", random);

            Random rnd = new Random();
            int random = rnd.Next(1, 100000);
            return string.Format("{0:00000000}", random);
        }

        public static string GenerateProtocol()
        {
            DateTime date1 = DateTime.Now;
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);

            string protocol = Get8Digits() + "/" + stDate;
            return protocol;
        }

        public static string GenerateProtocol(DateTime date1)
        {
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);               //Convert.ToString(dateOnly);

            string protocol = Get8Digits() + "/" + stDate;
            return protocol;
        }

        public static string GeneratePassword(Random rnd)
        {
            int random = rnd.Next(1, 1000);
            return string.Format("{0:000}", random);
        }

        #endregion


        #region AFM VALIDATION

        /// ------------------------------------------------------------------------
        /// CheckAFM: Ελέγχει αν ένα ΑΦΜ είναι σωστό
        /// Το ΑΦΜ που θα ελέγξουμε
        /// true = ΑΦΜ σωστό, false = ΑΦΜ Λάθος
        /// Αυτή είναι η χρησιμοποιούμενη μεθοδος.
        /// Προσθήκη: Αποκλεισμός όταν όλα τα ψηφία = 0 (ο αλγόριθμος τα δέχεται!)
        /// Ημ/νια: 12/3/2013
        /// ------------------------------------------------------------------------
        public static bool CheckAFM(string cAfm)
        {
            int nExp = 1;
            // Ελεγχος αν περιλαμβάνει μόνο γράμματα
            try { long nAfm = Convert.ToInt64(cAfm); }

            catch (Exception) { return false; }

            // Ελεγχος μήκους ΑΦΜ
            if (string.IsNullOrWhiteSpace(cAfm))
            {
                return false;
            }

            cAfm = cAfm.Trim();
            int nL = cAfm.Length;
            if (nL != 9) return false;

            // Έλεγχος αν όλα τα ψηφία είναι 0
            var count = cAfm.Count(x => x == '0');
            if (count == cAfm.Length) return false;

            //Υπολογισμός αν το ΑΦΜ είναι σωστό

            int nSum = 0;
            int xDigit = 0;
            int nT = 0;

            for (int i = nL - 2; i >= 0; i--)
            {
                xDigit = int.Parse(cAfm.Substring(i, 1));
                nT = xDigit * (int)(Math.Pow(2, nExp));
                nSum += nT;
                nExp++;
            }

            xDigit = int.Parse(cAfm.Substring(nL - 1, 1));

            nT = nSum / 11;
            int k = nT * 11;
            k = nSum - k;
            if (k == 10) k = 0;
            if (xDigit != k) return false;

            return true;
        }

        #endregion


        #region AGE CALCULATORS

        public static int CalculateAge(ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ aitisi, ΜΑΘΗΤΗΣ student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        public static int CalculateAge(AitisiViewModel aitisi, StudentViewModel student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        public static int CalculateAge2(ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 aitisi, ΜΑΘΗΤΗΣ student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        public static int CalculateAge2(Aitisi2ViewModel aitisi, StudentViewModel student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        #endregion


        #region AGE VALIDATION (Student Rule)

        public static bool ValidateBirthdate(StudentViewModel student)
        {
            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ == null) return false;

            DateTime _birthdate = (DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;

            if (!ValidBirthDate(_birthdate)) return false;
            else return true;
        }

        public static bool ValidBirthDate(DateTime birthdate)
        {
            bool result = true;
            int maxAge = 30;
            int minAge = 15;

            DateTime minDate = DateTime.Today.Date.AddYears(-maxAge);
            DateTime maxDate = DateTime.Today.Date.AddYears(-minAge);

            if (birthdate >= minDate && birthdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }

        #endregion


        #region AITISI DATE VALIDATION (Student Rule)

        public static bool ValidateAitisiDate(AitisiViewModel aitisi)
        {
            if (aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ == null) return false;

            DateTime _aitisidate = (DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            int schoolyearId = (int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (!DateInSchoolYear(_aitisidate, schoolyearId)) return false;
            else return true;
        }

        public static bool ValidateAitisi2Date(Aitisi2ViewModel aitisi)
        {
            if (aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ == null) return false;

            DateTime _aitisidate = (DateTime)aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ;
            int schoolyearId = (int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (!DateInSchoolYear(_aitisidate, schoolyearId)) return false;
            else return true;
        }

        public static bool ValidAitisiDate(DateTime regdate)
        {
            bool result = true;
            int yearsbefore = 7;
            int yearsafter = 13;

            DateTime minDate = DateTime.Today.Date.AddYears(-yearsbefore);
            DateTime maxDate = DateTime.Today.Date.AddYears(yearsafter);

            if (regdate >= minDate && regdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }

        #endregion


        #region PRIMARY KEY GETTERS

        public static int GetStudentID(int am, int school)
        {
            int studentId = 0;
            using (var db = new PrimusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΗΣ
                               where d.ΜΑΘΗΤΗΣ_ΑΜ == am && d.ΣΧΟΛΗ == school
                               select d).FirstOrDefault();

                if (student != null)
                {
                    studentId = student.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ;
                }
                return (studentId);
            }
        }

        public static int GetStudentAm(int studentId)
        {
            int studentAm = 0;
            using (var db = new PrimusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΗΣ
                               where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == studentId
                               select d).FirstOrDefault();

                if (student != null)
                {
                    studentAm = (int)student.ΜΑΘΗΤΗΣ_ΑΜ;
                }
                return (studentAm);
            }
        }

        public static string GetStudentAfm(int studentId)
        {
            string studentAfm = "";
            using (var db = new PrimusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΗΣ
                               where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == studentId
                               select d).FirstOrDefault();

                if (student != null)
                {
                    studentAfm = student.ΑΦΜ;
                }
                return (studentAfm);
            }
        }

        #endregion


        #region CUSTOM PRIMUS FUNCTIONS

        public static int GetStudentIdFromAitisi(int aitisiId)
        {
            using (var db = new PrimusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select new { d.ΜΑΘΗΤΗΣ_ΚΩΔ }).FirstOrDefault();
                if (student != null)
                    return (int)student.ΜΑΘΗΤΗΣ_ΚΩΔ;
                else
                    return 0;
            }
        }

        public static int GetSchoolType(int schoolId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
                int school_type = (int)data.SCHOOL_TYPE;

                return school_type;
            }
        }

        public static qrySTUDENT_INFO GetStudentInfo(int id)
        {
            using (var db = new PrimusDBEntities())
            {
                var student = (from d in db.qrySTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == id select d).FirstOrDefault();

                return (student);
            }
        }

        public static bool CanEditAitisi(int aitisiID)
        {
            using (var db = new PrimusDBEntities())
            {
                ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(aitisiID);

                if (entity.ΑΠΟΦΑΣΗ_ΚΩΔ != null)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanEditEidikotita(int studentId)
        {
            bool ok = true;

            using (var db = new PrimusDBEntities())
            {
                var data1 = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId select d).Count();
                var data2 = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2 where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId select d).Count();

                if (data1 > 0 || data2 > 0)
                    return false;

                return (ok);
            }
        }

        public static decimal GetStegasiPoso(int schoolyearId)
        {
            decimal StegasiPoso = 0.00M;

            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null)
                {
                    StegasiPoso = (decimal)data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                }
                return StegasiPoso;
            }
        }

        public static decimal GetSitisiPoso(int schoolyearId)
        {
            decimal SitisiPoso = 0.00M;

            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null)
                {
                    SitisiPoso = (decimal)data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ * 30.0M;
                }
                return SitisiPoso;
            }
        }

        public static string BuildAitisiSocialGroupsText(int aitisiId)
        {
            string txtSocialGroup = "";

            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).ToList();
                if (data.Count > 0)
                {
                    foreach (var d in data)
                    {
                        var group = (from e in db.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ where e.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ == d.ΚΡΙΤΗΡΙΟ_ΚΩΔ select e).FirstOrDefault();
                        txtSocialGroup += group.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ + ", ";
                    }
                    txtSocialGroup = txtSocialGroup.Substring(0, txtSocialGroup.Length - 2);
                }
                else
                {
                    txtSocialGroup = "ΚΑΝΕΝΑ";
                }
                return (txtSocialGroup);
            }
        }

        public static string BuildAitisi2SocialGroupsText(int aitisiId)
        {
            string txtSocialGroup = "";

            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).ToList();
                if (data.Count > 0)
                {
                    foreach (var d in data)
                    {
                        var group = (from e in db.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ where e.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ == d.ΚΡΙΤΗΡΙΟ_ΚΩΔ select e).FirstOrDefault();
                        txtSocialGroup += group.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ + ", ";
                    }
                    txtSocialGroup = txtSocialGroup.Substring(0, txtSocialGroup.Length - 2);
                }
                else
                {
                    txtSocialGroup = "ΚΑΝΕΝΑ";
                }
                return (txtSocialGroup);
            }
        }

        public static string ValidateStudentFields(StudentViewModel student)
        {
            string errMsg = "";

            if (!ValidateBirthdate(student)) errMsg += "-> Η ηλικία είναι εκτός λογικών ορίων. ";
            if (!CheckAFM(student.ΑΦΜ)) errMsg += "-> Το ΑΦΜ δεν είναι έγκυρο. ";

            return (errMsg);
        }

        public static string ValidateAitisiFields(AitisiViewModel aitisi)
        {
            string ErrMsg = "";

            if (!(aitisi.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ > 0))
            {
                ErrMsg += "-> Πρέπει να επιλεγεί ένα είδος επιδόματος.";
            }
            if (aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ > aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ)
            {
                ErrMsg += "-> Το ποσό στέγασης δεν μπορεί να υπεβαίνει το μισθωτήριο!";
            }
            if (aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ > GetStegasiPoso((int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
            {
                ErrMsg += "-> Το ποσό στέγασης δεν μπορεί να υπερβαίνει το προκαθορισμένο επιτρεπόμενο!";
            }
            if (aitisi.ΣΙΤΙΣΗ_ΠΟΣΟ > GetSitisiPoso((int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
            {
                ErrMsg += "-> Το ποσό σίτισης δεν μπορεί να υπερβαίνει το προκαθορισμένο επιτρεπόμενο!";
            }
            if (!ValidateAitisiDate(aitisi))
            {
                ErrMsg += "-> Η ημερομηνία αίτησης είναι εκτός σχολικού έτους!";
            }
            if (aitisi.ΗΛΙΚΙΑ > 30 || aitisi.ΗΛΙΚΙΑ < 15)
            {
                ErrMsg += "-> Η ηλικία είναι εκτός λογικών ορίων (15 - 30 ετών)!";
            }
            if (aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ < 0 || aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ > 2000)
            {
                ErrMsg += "-> Το ποσό μισθωτήριου είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ < 0 || aitisi.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ > 30)
            {
                ErrMsg += "-> Ο αριθμός τέκνων είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ < 0 || aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ > 1000)
            {
                ErrMsg += "-> Η απόσταση διαμονής είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ == null)
            {
                ErrMsg += "Πρέπει να δοθεί κάποια τιμή για την απόσταση διαμονής.";
            }
            return ErrMsg;
        }

        public static string ValidateAitisi2Fields(Aitisi2ViewModel aitisi)
        {
            string ErrMsg = "";

            if (aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ > aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ)
            {
                ErrMsg += "-> Το ποσό στέγασης δεν μπορεί να υπεβαίνει το μισθωτήριο!";
            }
            if (aitisi.ΣΙΤΙΣΗ_ΠΟΣΟ > GetSitisiPoso((int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
            {
                ErrMsg += "-> Το ποσό σίτισης δεν μπορεί να υπερβαίνει το προκαθορισμένο επιτρεπόμενο!";
            }
            if (!ValidateAitisi2Date(aitisi))
            {
                ErrMsg += "-> Η ημερομηνία αίτησης είναι εκτός σχολικού έτους!";
            }
            if (aitisi.ΗΛΙΚΙΑ > 30 || aitisi.ΗΛΙΚΙΑ < 15)
            {
                ErrMsg += "-> Η ηλικία είναι εκτός λογικών ορίων (15 - 30 ετών)!";
            }
            if (aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ < 0 || aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ > 2000)
            {
                ErrMsg += "-> Το ποσό μισθωτήριου είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ < 0 || aitisi.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ > 30)
            {
                ErrMsg += "-> Ο αριθμός τέκνων είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ < 0 || aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ > 1000)
            {
                ErrMsg += "-> Η απόσταση διαμονής είναι εκτός λογικών ορίων.";
            }
            if (aitisi.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ == null)
            {
                ErrMsg += "Πρέπει να δωθεί κάποια τιμή για την απόσταση διαμονής.";
            }
            return ErrMsg;
        }

        public static string ValidateApofasiSitisi2Fields(ApofasiSitisi2ViewModel apofasi)
        {
            string errMsg = "";
            if (!string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ) && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null)
                errMsg += "-> Εκτός από το πρωτόκολλο πρέπει να καταχωρηθεί και ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null)
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            return (errMsg);
        }

        public static string GetYearFromSchoolYearMonth(int schoolyearId, int month)
        {
            int theYear = 0;
            string strYear = "";

            using (var db = new PrimusDBEntities())
            {
                if (schoolyearId > 0 && month > 0)
                {
                    var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ where d.SCHOOLYEAR_ID == schoolyearId select d).FirstOrDefault();
                    if (month >= 9 && month <= 12)
                    {
                        DateTime theDate = (DateTime)data.DATE_START;
                        theYear = theDate.Year;
                        strYear = theYear.ToString();
                    }
                    else if (month >= 1 && month <= 8)
                    {
                        DateTime theDate = (DateTime)data.DATE_END;
                        theYear = theDate.Year;
                        strYear = theYear.ToString();
                    }
                }
                return (strYear);
            }
        }

        public static AitisiViewModel SetDefaultPosa(AitisiViewModel aitisi)
        {
            decimal StegasiPoso = 0.00M;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
                if (data != null)
                {
                    StegasiPoso = (decimal)data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                    if (aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ > 0 && aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ <= data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ)
                    {
                        aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ = aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ;
                    }
                    else
                    {
                        aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ = StegasiPoso;
                    }
                }
                aitisi.ΣΙΤΙΣΗ_ΠΟΣΟ = GetSitisiPoso((int)aitisi.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

                return (aitisi);
            }
        }

        public static string GetSchoolDirector()
        {
            USER_SCHOOLS loggedSchool;
            loggedSchool = GetLoginSchool();
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            string director = "";
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
                director = data.ΔΙΕΥΘΥΝΤΗΣ;

                return (director);
            }

        }

        public static AitisiViewModel GetRelatedAitisi(int studentId, EpidomaParameters ep)
        {
            AitisiViewModel data = new AitisiViewModel();

            using (var db = new PrimusDBEntities())
            {
                if (RelatedAitisiExists(studentId, ep))
                {
                    data = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ
                            where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId &&
                                  d.ΜΑΘΗΤΗΣ.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                            select new AitisiViewModel
                            {
                                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                                ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                                ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                                ΤΑΞΗ = d.ΤΑΞΗ,
                                ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                                ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                                ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                                ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                                ΜΑΘΗΤΗΣ = d.ΜΑΘΗΤΗΣ
                            }).FirstOrDefault();
                    return data;
                }
                return null;
            }
        }

        public static bool RelatedAitisiExists(int studentId, EpidomaParameters ep)
        {
            using (var db = new PrimusDBEntities())
            {
                var count = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ
                             where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId &&
                                   d.ΜΑΘΗΤΗΣ.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                             select d).Count();

                if (count > 0) return true;
                else return false;
            }
        }

        #endregion


        #region APOFASEIS COMMON FUNCTIONS

        public static int? LoadProistamenos(int? schoolyearId)
        {
            int? value = null;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null) value = data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
                return value;
            }
        }

        public static int? LoadDirector(int? schoolyearId)
        {
            int? value = null;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null) value = data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
                return value;
            }
        }

        public static int? LoadGenikos(int? schoolyearId)
        {
            int? value = null;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null) value = data.ΓΕΝΙΚΟΣ_ΚΩΔ;
                return value;
            }
        }

        public static int? LoadDioikitis(int? schoolyearId)
        {
            int? value = null;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null)
                    value = data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
                return value;
            }
        }

        public static int? LoadAntiproedros(int? schoolyearId)
        {
            int? value = null;
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).FirstOrDefault();
                if (data != null) value = data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;
                return value;
            }
        }

        public static bool ValidSignatures(int? schoolyearId)
        {
            bool ok = true;

            int? proistamenos = LoadProistamenos(schoolyearId);
            int? director = LoadDirector(schoolyearId);
            int? genikos = LoadGenikos(schoolyearId);
            int? dioikitis = LoadDioikitis(schoolyearId);
            int? aproedros = LoadAntiproedros(schoolyearId);

            bool predicate = proistamenos == null || director == null || genikos == null || dioikitis == null || aproedros == null;
            if (predicate == true) ok = false;

            return (ok);
        }

        #endregion


        #region UPLOAD FUNCTIONS

        public static string GetSchoolYearText(int syearId)
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                            where d.SCHOOLYEAR_ID == syearId
                            select d).FirstOrDefault();

                string syearText = data.SCHOOLYEAR_TEXT;
                return (syearText);
            }
        }

        public static string GetSchoolUsername(int schoolId)
        {
            string username = "";
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.USER_SCHOOLS where d.USER_SCHOOLID == schoolId select d).FirstOrDefault();
                if (data != null) username = data.USERNAME;

                return (username);
            }
        }

        public static Guid GetFileGuidFromName(string filename, int uploadId)
        {
            Guid file_id = new Guid();
            
            using (var db = new PrimusDBEntities())
            {
                var fileData = (from d in db.UPLOADS_FILES where d.FILENAME == filename && d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (fileData != null) file_id = fileData.ID;

                return (file_id);
            }
        }

        public static Tuple<int, int> GetUploadInfo(int uploadId)
        {
            int school_id = 0;
            int syear_id = 0;

            using (var db = new PrimusDBEntities())
            {
                var upload = (from d in db.UPLOADS where d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (upload != null)
                {
                    school_id = (int)upload.SCHOOL_ID;
                    syear_id = (int)upload.SCHOOLYEAR_ID;
                }

                var data = Tuple.Create(school_id, syear_id);
                return (data);
            }
        }


        #endregion


        #region CONVERTERS
        public static string StudentNumberToText(int number)
        {
            string s;
            string suffix1 = "μαθητή";
            string suffix = "μαθητές";
            string[] tens = new string[] { "είκοσι ", "τριάντα ", "σαράντα ", "πενήντα ", "εξήντα ", "εβδομήντα ", "οδόντα ", "ενενήντα ", "εκατό ", "εκατόν " };
            string[] digits = new string[] { "μηδέν ", "ένα ", "δύο ", "τρεις ", "τέσσερις ", "πέντε ", "έξι ", "επτά ", "οκτώ ", "εννέα " };
            string[] teens = new string[] { "δέκα ", "έντεκα ", "δώδεκα ", "δεκατρείς ", "δεκατέσσερις ", "δεκαπέντε ", "δεκαέξι", "δεκαεπτά ", "δεκαοκτώ ", "δεκαεννέα " };

            if (number == 0) s = digits[0] + suffix;
            else if (number == 1) s = digits[1] + suffix1;
            else if (number == 2) s = digits[2] + suffix;
            else if (number == 3) s = digits[3] + suffix;
            else if (number == 4) s = digits[4] + suffix;
            else if (number == 5) s = digits[5] + suffix;
            else if (number == 6) s = digits[6] + suffix;
            else if (number == 7) s = digits[7] + suffix;
            else if (number == 8) s = digits[8] + suffix;
            else if (number == 9) s = digits[9] + suffix;
            else if (number == 10) s = teens[0] + suffix;
            else if (number == 11) s = teens[1] + suffix;
            else if (number == 12) s = teens[2] + suffix;
            else if (number == 13) s = teens[3] + suffix;
            else if (number == 14) s = teens[4] + suffix;
            else if (number == 15) s = teens[5] + suffix;
            else if (number == 16) s = teens[6] + suffix;
            else if (number == 17) s = teens[7] + suffix;
            else if (number == 18) s = teens[8] + suffix;
            else if (number == 19) s = teens[9] + suffix;
            else if (number == 20) s = tens[0] + suffix;
            else if (number == 21) s = tens[0] + digits[1] + suffix;
            else if (number == 22) s = tens[0] + digits[2] + suffix;
            else if (number == 23) s = tens[0] + digits[3] + suffix;
            else if (number == 24) s = tens[0] + digits[4] + suffix;
            else if (number == 25) s = tens[0] + digits[5] + suffix;
            else if (number == 26) s = tens[0] + digits[6] + suffix;
            else if (number == 27) s = tens[0] + digits[7] + suffix;
            else if (number == 28) s = tens[0] + digits[8] + suffix;
            else if (number == 29) s = tens[0] + digits[9] + suffix;
            else if (number == 30) s = tens[1] + suffix;
            else if (number == 31) s = tens[1] + digits[1] + suffix;
            else if (number == 32) s = tens[1] + digits[2] + suffix;
            else if (number == 33) s = tens[1] + digits[3] + suffix;
            else if (number == 34) s = tens[1] + digits[4] + suffix;
            else if (number == 35) s = tens[1] + digits[5] + suffix;
            else if (number == 36) s = tens[1] + digits[6] + suffix;
            else if (number == 37) s = tens[1] + digits[7] + suffix;
            else if (number == 38) s = tens[1] + digits[8] + suffix;
            else if (number == 39) s = tens[1] + digits[9] + suffix;
            else if (number == 40) s = tens[2] + suffix;
            else if (number == 41) s = tens[2] + digits[1] + suffix;
            else if (number == 42) s = tens[2] + digits[2] + suffix;
            else if (number == 43) s = tens[2] + digits[3] + suffix;
            else if (number == 44) s = tens[2] + digits[4] + suffix;
            else if (number == 45) s = tens[2] + digits[5] + suffix;
            else if (number == 46) s = tens[2] + digits[6] + suffix;
            else if (number == 47) s = tens[2] + digits[7] + suffix;
            else if (number == 48) s = tens[2] + digits[8] + suffix;
            else if (number == 49) s = tens[2] + digits[9] + suffix;
            else if (number == 50) s = tens[3] + suffix;
            else if (number == 51) s = tens[3] + digits[1] + suffix;
            else if (number == 52) s = tens[3] + digits[2] + suffix;
            else if (number == 53) s = tens[3] + digits[3] + suffix;
            else if (number == 54) s = tens[3] + digits[4] + suffix;
            else if (number == 55) s = tens[3] + digits[5] + suffix;
            else if (number == 56) s = tens[3] + digits[6] + suffix;
            else if (number == 57) s = tens[3] + digits[7] + suffix;
            else if (number == 58) s = tens[3] + digits[8] + suffix;
            else if (number == 59) s = tens[3] + digits[9] + suffix;
            else if (number == 60) s = tens[4] + suffix;
            else if (number == 61) s = tens[4] + digits[1] + suffix;
            else if (number == 62) s = tens[4] + digits[2] + suffix;
            else if (number == 63) s = tens[4] + digits[3] + suffix;
            else if (number == 64) s = tens[4] + digits[4] + suffix;
            else if (number == 65) s = tens[4] + digits[5] + suffix;
            else if (number == 66) s = tens[4] + digits[6] + suffix;
            else if (number == 67) s = tens[4] + digits[7] + suffix;
            else if (number == 68) s = tens[4] + digits[8] + suffix;
            else if (number == 69) s = tens[4] + digits[9] + suffix;
            else if (number == 70) s = tens[5] + suffix;
            else if (number == 71) s = tens[5] + digits[1] + suffix;
            else if (number == 72) s = tens[5] + digits[2] + suffix;
            else if (number == 73) s = tens[5] + digits[3] + suffix;
            else if (number == 74) s = tens[5] + digits[4] + suffix;
            else if (number == 75) s = tens[5] + digits[5] + suffix;
            else if (number == 76) s = tens[5] + digits[6] + suffix;
            else if (number == 77) s = tens[5] + digits[7] + suffix;
            else if (number == 78) s = tens[5] + digits[8] + suffix;
            else if (number == 79) s = tens[5] + digits[9] + suffix;
            else if (number == 80) s = tens[6] + suffix;
            else if (number == 81) s = tens[6] + digits[1] + suffix;
            else if (number == 82) s = tens[6] + digits[2] + suffix;
            else if (number == 83) s = tens[6] + digits[3] + suffix;
            else if (number == 84) s = tens[6] + digits[4] + suffix;
            else if (number == 85) s = tens[6] + digits[5] + suffix;
            else if (number == 86) s = tens[6] + digits[6] + suffix;
            else if (number == 87) s = tens[6] + digits[7] + suffix;
            else if (number == 88) s = tens[6] + digits[8] + suffix;
            else if (number == 89) s = tens[6] + digits[9] + suffix;
            else if (number == 90) s = tens[7] + suffix;
            else if (number == 91) s = tens[7] + digits[1] + suffix;
            else if (number == 92) s = tens[7] + digits[2] + suffix;
            else if (number == 93) s = tens[7] + digits[3] + suffix;
            else if (number == 94) s = tens[7] + digits[4] + suffix;
            else if (number == 95) s = tens[7] + digits[5] + suffix;
            else if (number == 96) s = tens[7] + digits[6] + suffix;
            else if (number == 97) s = tens[7] + digits[7] + suffix;
            else if (number == 98) s = tens[7] + digits[8] + suffix;
            else if (number == 99) s = tens[7] + digits[9] + suffix;
            else if (number == 100) s = tens[8] + suffix;

            else s = "";

            return (s);
        }

        public static string DecimalToFractional(int NumLessons, int gradeSum)
        {
            double grade_decimal = GradeDecimal(NumLessons, gradeSum);

            int decPart = (int)((grade_decimal -Math.Floor(grade_decimal)) * 100.0);

            int nominator = (int)Math.Round((double)(decPart * NumLessons / 100.0), 0);

            string OutGrade = Math.Floor(grade_decimal).ToString() + " " + nominator.ToString() + "/" + NumLessons.ToString();

            return (OutGrade);
        }

        public static double GradeDecimal(int NumLessons, int gradeSum)
        {
            double grade_decimal = (double)gradeSum / (double)NumLessons;

            return (grade_decimal);
        }

        public static string NominatorWord(int number)
        {
            string s;

            if (number == 0) s = "ΜΗΔΕΝ";
            else if (number == 1) s = "ENA";
            else if (number == 2) s = "ΔΥΟ";
            else if (number == 3) s = "ΤΡΙΑ";
            else if (number == 4) s = "ΤΕΣΣΕΡΑ";
            else if (number == 5) s = "ΠΕΝΤΕ";
            else if (number == 6) s = "ΕΞΙ";
            else if (number == 7) s = "ΕΠΤΑ";
            else if (number == 8) s = "ΟΚΤΩ";
            else if (number == 9) s = "ΕΝΝΕΑ";
            else if (number == 10) s = "ΔΕΚΑ";
            else if (number == 11) s = "ENTEKA";
            else if (number == 12) s = "ΔΩΔΕΚΑ";
            else if (number == 13) s = "ΔΕΚΑΤΡΙΑ";
            else if (number == 14) s = "ΔΕΚΑΤΕΣΣΕΡΑ";
            else if (number == 15) s = "ΔΕΚΑΠΕΝΤΕ";
            else if (number == 16) s = "ΔΕΚΑΕΞΙ";
            else if (number == 17) s = "ΔΕΚΑΕΠΤΑ";
            else if (number == 18) s = "ΔΕΚΑΟΚΤΩ";
            else if (number == 19) s = "ΔΕΚΑΕΝΝΕΑ";
            else if (number == 20) s = "ΕΙΚΟΣΙ";
            else if (number == 21) s = "ΕΙΚΟΣΙΕΝΑ";
            else if (number == 22) s = "ΕΙΚΟΣΙΔΥΟ";
            else if (number == 23) s = "ΕΙΚΟΣΙΤΡΙΑ";
            else if (number == 24) s = "ΕΙΚΟΣΙΤΕΣΣΕΡΑ";
            else if (number == 25) s = "ΕΙΚΟΣΙΠΕΝΤΕ";
            else if (number == 26) s = "ΕΙΚΟΣΙΕΞΙ";
            else if (number == 27) s = "ΕΙΚΟΣΙΕΠΤΑ";
            else if (number == 28) s = "ΕΙΚΟΣΙΟΚΤΩ";
            else if (number == 29) s = "ΕΙΚΟΣΙΕΝΝΕΑ";
            else if (number == 30) s = "ΤΡΙΑΝΤΑ";
            else s = "";

            return (s);
        }

        public static string DenominatorWord(int number)
        {
            string s;

            if (number == 0) s = "ΜΗΔΕΝ";
            else if (number == 1) s = "ΠΡΩΤΑ";
            else if (number == 2) s = "ΔΕΥΤΕΡΑ";
            else if (number == 3) s = "ΤΡΙΤΑ";
            else if (number == 4) s = "ΤΕΤΑΡΤΑ";
            else if (number == 5) s = "ΠΕΜΠΤΑ";
            else if (number == 6) s = "ΕΚΤΑ";
            else if (number == 7) s = "ΕΒΔΟΜΑ";
            else if (number == 8) s = "ΟΓΔΟΑ";
            else if (number == 9) s = "ΕΝΝΑΤΑ";
            else if (number == 10) s = "ΔΕΚΑΤΑ";
            else if (number == 11) s = "ΕΝΤΕΚΑΤΑ";
            else if (number == 12) s = "ΔΩΔΕΚΑΤΑ";
            else if (number == 13) s = "ΔΕΚΑΤΑ ΤΡΙΤΑ";
            else if (number == 14) s = "ΔΕΚΑΤΑ ΤΕΤΑΡΤΑ";
            else if (number == 15) s = "ΔΕΚΑΤΑ ΠΕΜΠΤΑ";
            else if (number == 16) s = "ΔΕΚΑΤΑ ΕΚΤΑ";
            else if (number == 17) s = "ΔΕΚΑΤΑ ΕΒΔΟΜΑ";
            else if (number == 18) s = "ΔΕΚΑΤΑ ΟΓΔΟΑ";
            else if (number == 19) s = "ΔΕΚΑΤΑ ΕΝΑΤΑ";
            else if (number == 20) s = "ΕΙΚΟΣΤΑ";
            else if (number == 21) s = "ΕΙΚΟΣΤΑ ΠΡΩΤΑ";
            else if (number == 22) s = "ΕΙΚΟΣΤΑ ΔΕΥΤΕΡΑ";
            else if (number == 23) s = "ΕΙΚΟΣΤΑ ΤΡΙΤΑ";
            else if (number == 24) s = "ΕΙΚΟΣΤΑ ΤΕΤΑΡΤΑ";
            else if (number == 25) s = "ΕΙΚΟΣΤΑ ΠΕΜΠΤΑ";
            else if (number == 26) s = "ΕΙΚΟΣΤΑ ΕΚΤΑ";
            else if (number == 27) s = "ΕΙΚΟΣΤΑ ΕΒΔΟΜΑ";
            else if (number == 28) s = "ΕΙΚΟΣΤΑ ΟΓΔΟΑ";
            else if (number == 29) s = "ΕΙΚΟΣΤΑ ΕΝΑΤΑ";
            else if (number == 30) s = "ΤΡΙΑΚΟΣΤΑ";
            else s = "";

            return (s);
        }

        public static string textGradeParse(string grade)
        {
            string result;
            string con1 = " & ";
            string con2 = "/";

            int posSpace = grade.IndexOf(" ");
            int posSlash = grade.IndexOf("/");

            string intGrade = grade.Substring(0, posSpace);
            string strFraction = grade.Substring(posSpace + 1);

            int endIndex = strFraction.Length;
            int startIndex = strFraction.IndexOf("/");
            int DenomLen = strFraction.Substring(startIndex + 1).Length;
            int NomLen = strFraction.Substring(0, startIndex).Length;
            int sLen = endIndex - startIndex;

            string intNom = strFraction.Substring(0, NomLen);
            string intDenom = strFraction.Substring(startIndex + 1);

            int wholeNum = Int32.Parse(intGrade);
            int fractionNom = Int32.Parse(intNom);
            int fractionDenom = Int32.Parse(intDenom);

            result = NominatorWord(wholeNum) + con1 + NominatorWord(fractionNom) + con2 + DenominatorWord(fractionDenom);
            return (result);
        }

        #endregion

        public static USER_SCHOOLS GetLoginSchool()
        {
            using (var db = new PrimusDBEntities())
            {
                var loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

                int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
                var _school = (from s in db.sqlUSER_SCHOOL
                               where s.USER_SCHOOLID == SchoolID
                               select new { s.SCHOOL_NAME }).FirstOrDefault();
                return loggedSchool;
            }
        }

    }   // class Common

    public static class HumanFriendlyInteger
    {
        static string[] ones = new string[] { "", "ΜΙΑ", "ΔΥΟ", "ΤΡΕΙΣ", "ΤΕΣΣΕΡΙΣ", "ΠΕΝΤΕ", "ΕΞΙ", "ΕΠΤΑ", "ΟΚΤΩ", "ΕΝΝΕΑ" };
        static string[] teens = new string[] { "ΔΕΚΑ", "ΕΝΤΕΚΑ", "ΔΩΔΕΚΑ", "ΔΕΚΑΤΡΕΙΣ", "ΔΕΚΑΤΕΣΣΕΡΙΣ", "ΔΕΚΑΠΕΝΤΕ", "ΔΕΚΑΕΞΙ", "ΔΕΚΑΕΠΤΑ", "ΔΕΚΑΟΚΤΩ", "ΔΕΚΑΕΝΝΕΑ" };
        static string[] tens = new string[] { "ΕΙΚΟΣΙ", "ΤΡΙΑΝΤΑ", "ΣΑΡΑΝΤΑ", "ΠΕΝΗΝΤΑ", "ΕΞΗΝΤΑ", "ΕΒΔΟΜΗΝΤΑ", "ΟΓΔΟΝΤΑ", "ΕΝΕΝΗΝΤΑ" };
        static string[] hundreds = new string[] {"ΕΚΑΤΟ", "ΕΚΑΤΟΝ ", "ΔΙΑΚΟΣΙΕΣ ", "ΤΡΙΑΚΟΣΙΕΣ ", "ΤΕΤΡΑΚΟΣΙΕΣ ", "ΠΕΝΤΑΚΟΣΙΕΣ ", "ΕΞΑΚΟΣΙΕΣ ", "ΕΠΤΑΚΟΣΙΕΣ ", "ΟΚΤΑΚΟΣΙΕΣ ", "ΕΝΝΙΑΚΟΣΙΕΣ ", "ΧΙΛΙΕΣ " };
        static string[] thousandsGroups = { "", " ΧΙΛΙΑΔΕΣ", " ΕΚΑΤΟΜΜΥΡΙΑ", " ΔΙΣΕΚΑΤΟΜΜΥΡΙΑ" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n > 99999) return "*** ΑΝΩΤΑΤΟ ΟΡΙΟ ΥΠΟΛΟΓΙΣΜΩΝ ***";

            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                if (n == 100)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100 - 1] + ones[(n % 100)]), 0);
                    return friendlyInt;
                }

                if (n > 100 && n <= 109 || n >= 200 && n <= 209 || n >= 300 && n <= 309 || n >= 400 && n <= 409 || n >= 500 && n <= 509 ||
                    n >= 600 && n <= 609 || n >= 700 && n <= 709 || n >= 800 && n <= 809 || n >= 900 && n <= 909) 
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + ones[(n % 100)]), 0);
                    return friendlyInt;
                }
                if (n > 109 && n <= 119 || n > 209 && n <= 219 || n > 309 && n <= 319 || n > 409 && n <= 419 || n > 509 && n <= 519 ||
                    n > 609 && n <= 619 || n > 709 && n <= 719 || n > 809 && n <= 819 || n > 909 && n <= 919)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + teens[(n % 100) % 10]), 0);
                    return friendlyInt;
                }
                if (n > 119 && n <= 199 || n > 219 && n <= 299 || n > 319 && n <= 399 || n > 419 && n <= 499 || n > 519 && n <= 599 ||
                    n > 619 && n <= 699 || n > 719 && n <= 799 || n > 819 && n <= 899 || n > 919 && n <= 999)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + tens[(n % 100) / 10 - 2] + " " + ones[(n % 100) % 10]), 0);
                    return friendlyInt;
                }
            }
            else if (n >= 1000 && n < 2000)
            {
                friendlyInt += FriendlyInteger(n-1000, "ΧΙΛΙΕΣ", 0);
                return friendlyInt;
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }
            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "ΜΗΔΕΝ";
            }
            else if (n < 0)
            {
                return "ΜΕΙΟΝ " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }
    }

    // View Engine extension of Primus
    public class PrimusViewEngine : RazorViewEngine
    {
        public PrimusViewEngine()
        {
            string[] locations = new string[] {  
            "~/Views/{1}/{0}.cshtml",
            "~/Views/Document/{0}.cshtml",

            "~/Views/School/Apofaseis/{0}.cshtml",
            "~/Views/School/Students/{0}.cshtml",
            "~/Views/School/Statistics/{0}.cshtml",
            "~/Views/School/Settings/{0}.cshtml",          

            "~/Views/Admin/{1}/{0}.cshtml",
            "~/Views/Admin/Aitiseis/{0}.cshtml",
            "~/Views/Admin/Apofaseis/{0}.cshtml",
            "~/Views/Admin/Epidotiseis/{0}.cshtml",
            "~/Views/Admin/Students/{0}.cshtml",
            "~/Views/Admin/Statistics/{0}.cshtml",

            "~/Views/Shared/PartialViews/{0}.cshtml",
            "~/Views/Shared/EditorTemplates/{0}.cshtml",
            "~/Views/Shared/Layouts/{0}.cshtml"
        };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;
        }
    }

}   // namespace