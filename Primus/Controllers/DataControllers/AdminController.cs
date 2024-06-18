using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Primus.Filters;
using Primus.DAL;
using Primus.Models;
using Primus.BPM;
using Primus.Notification;
using Primus.Services;

namespace Primus.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class AdminController : ControllerUnit
    {
        private readonly PrimusDBEntities db;
        private USER_ADMINS loggedAdmin;

        private readonly IStudentService studentService;
        private readonly IAitisiService aitisiService;
        private readonly IAitisiSocialService aitisiSocialService;
        private readonly IStudentInfoService studentInfoService;
        private readonly IAitisiInfoService aitisiInfoService;

        public AdminController(PrimusDBEntities entities, IStudentService studentService, IAitisiService aitisiService, 
            IAitisiSocialService aitisiSocialService, IStudentInfoService studentInfoService, IAitisiInfoService aitisiInfoService) : base(entities)
        {
            db = entities;

            this.studentService = studentService;
            this.aitisiService = aitisiService;
            this.aitisiSocialService = aitisiSocialService;
            this.studentInfoService = studentInfoService;
            this.aitisiInfoService = aitisiInfoService;
        }


        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }


        #region ΜΑΘΗΤΕΣ ΚΑΙ ΑΙΤΗΣΕΙΣ

        #region ΜΑΘΗΤΕΣ

        public ActionResult xStudentData(int studentId = 0, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!Kerberos.EidikotitesExist())
            {
                string msg = "Για να καταχωρηθούν μαθητές πρέπει πρώτα να ορίσετε τις ειδικότητες στις Ρυθμίσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateEidikotites();
            PopulateSchools();
            PopulateSchoolYears();
            PopulateTakseis();
            PopulateStudents();
            return View();
        }

        #region ΜΑΘΗΤΕΣ GRID CRUD FUNCTIONS

        public ActionResult Student_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0)
        {
            var data = studentService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Create([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            var newData = new StudentGridViewModel();

            if (!Common.CheckAFM(data.ΑΦΜ))
                ModelState.AddModelError("", "Το ΑΦΜ που καταχωρήσατε δεν είναι έγκυρο.");

            if (!Kerberos.ValidatePrimaryKeyStudent(data.ΑΦΜ, (int)data.ΣΧΟΛΗ, (int)data.ΕΙΔΙΚΟΤΗΤΑ))
                ModelState.AddModelError("", "Το ΑΦΜ και η ειδικότητα που δώσατε υπάρχουν ήδη καταχωρημένα για το σχολείο αυτό.");

            if (ModelState.IsValid)
            {
                studentService.Create(data);
                studentService.CopyStudentData(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ, data.ΑΦΜ);

                newData = studentService.Refresh(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Update([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            int? oldEidikotita = 0;
            bool editEidikotitaOK = true;

            var newData = new StudentGridViewModel();

            var src = (from d in db.ΜΑΘΗΤΗΣ where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();
            if (src != null) oldEidikotita = src.ΕΙΔΙΚΟΤΗΤΑ;
            if (oldEidikotita != data.ΕΙΔΙΚΟΤΗΤΑ) editEidikotitaOK = Common.CanEditEidikotita(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);

            if (!editEidikotitaOK)
            {
                ModelState.AddModelError("", "Δεν μπορεί να γίνει αλλαγή της ειδικότητας διότι υπάρχουν αιτήσεις του μαθητή με αυτή.");
            }

            if (ModelState.IsValid)
            {
                studentService.Update(data);
                newData = studentService.Refresh(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Destroy([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteStudent(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ ))
                {
                    studentService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή του μαθητή διότι υπάρχουν σχετιζόμενες αιτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region ΑΙΤΗΣΕΙΣ GRID CRUD FUNCTIONS

        public ActionResult Aitisi_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0)
        {
            var data = aitisiService.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Create([DataSourceRequest] DataSourceRequest request, AitisiGridViewModel data, int studentId = 0)
        {
            AitisiGridViewModel newData = new AitisiGridViewModel();

            if (studentId > 0)
            {
                if (!Common.DateInSchoolYear((DateTime)data.ΗΜΝΙΑ_ΑΙΤΗΣΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                    ModelState.AddModelError("", "Η ημερομηνία αίτησης είναι εκτός σχολικού έτους.");

                if (Kerberos.StudentAitisiExists(data, studentId))
                    ModelState.AddModelError("", "Υπάρχει ήδη αίτηση του μαθητή γι' αυτό το σχολικό έτος και τάξη. Η καταχώρηση ακυρώθηκε.");

                if (data != null && ModelState.IsValid)
                {
                    aitisiService.Create(data, studentId);
                    newData = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                    PopulateStudents();
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μαθητή. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Update([DataSourceRequest] DataSourceRequest request, AitisiGridViewModel data, int studentId = 0)
        {
            AitisiGridViewModel newData = new AitisiGridViewModel();
            if (studentId > 0)
            {
                if (!Common.DateInSchoolYear((DateTime)data.ΗΜΝΙΑ_ΑΙΤΗΣΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ))
                    ModelState.AddModelError("", "Η ημερομηνία αίτησης είναι εκτός σχολικού έτους.");

                if (data != null && ModelState.IsValid)
                {
                    aitisiService.Update(data, studentId);
                    newData = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μαθητή. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Destroy([DataSourceRequest] DataSourceRequest request, AitisiGridViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeleteAitisi(data.ΑΙΤΗΣΗ_ΚΩΔ))
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της αίτησης διότι έχει συσχετισμένα κοινωνικά κριτήρια.");
                    return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
                try
                {
                    aitisiService.Destroy(data);
                }
                catch
                {
                    ModelState.AddModelError("", "Προέκυψε κάποιο σφάλμα κατά τη διαγραφή.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region STUDENT DATA FORM

        public ActionResult xStudentEdit(int studentId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            StudentViewModel student = studentService.GetRecord(studentId);
            if (student == null)
            {
                string msg = "Δεν βρέθηκε μαθητής. Πρέπει πρώτα να γίνει ενημέρωση των στοιχείων από το πλέγμα.";
                return RedirectToAction("xStudentData", "Admin", new { notify = msg });
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult xStudentEdit(int studentId, StudentViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            int? oldEidikotita = 0;
            bool editEidikotitaOK = true;

            var src = (from d in db.ΜΑΘΗΤΗΣ where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == studentId select d).FirstOrDefault();
            if (src != null) oldEidikotita = src.ΕΙΔΙΚΟΤΗΤΑ;
            if (oldEidikotita != model.ΕΙΔΙΚΟΤΗΤΑ) editEidikotitaOK = Common.CanEditEidikotita(studentId);
            if (!editEidikotitaOK)
            {
                this.ShowMessage(MessageType.Warning, "Δεν μπορεί να γίνει αλλαγή της ειδικότητας διότι υπάρχουν αιτήσεις του μαθητή με αυτή.");
                ModelState.AddModelError("", "");
            }

            string ErrorMsg = Common.ValidateStudentFields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                studentService.UpdateRecord(model, studentId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentViewModel newStudent = studentService.GetRecord(studentId);
                return View(newStudent);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        #endregion


        #region AITISI DATA FORM

        public ActionResult xAitisiEdit(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (!(aitisiId > 0))
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("xStudentData", "Admin", new { notify = msg });
            }

            AitisiViewModel aitisi = aitisiService.GetRecord(aitisiId);
            if (aitisi == null)
            {
                return HttpNotFound();
            }
            int studentId = aitisi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            qrySTUDENT_INFO SelectedStudent = Common.GetStudentInfo(studentId);

            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης μαθητή.";
                return RedirectToAction("Index", "Admin", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }
            PopulateSocialGroups();
            // Addition: Do this to display age
            StudentViewModel student = studentService.GetRecord(studentId);
            aitisi.ΗΛΙΚΙΑ = Common.CalculateAge(aitisi, student);
            // End addition
            return View(aitisi);
        }

        [HttpPost]
        public ActionResult xAitisiEdit(int aitisiId, AitisiViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PopulateSocialGroups();

            data = SetAitisiFields(data, aitisiId);

            qrySTUDENT_INFO SelectedStudent = Common.GetStudentInfo(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            StudentViewModel student = studentService.GetRecord(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης μαθητή.";
                return RedirectToAction("Index", "Admin", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            string ErrorMsg = Common.ValidateAitisiFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                aitisiService.UpdateRecord(data, student, aitisiId, admin: true);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                AitisiViewModel newAitisi = aitisiService.GetRecord(aitisiId);
                return View(newAitisi);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion AITISI DATA FORM


        #region ΔΙΠΛΟΤΥΠΟΙ ΜΑΘΗΤΕΣ

        public ActionResult DuplStudentData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateEidikotites();
            PopulateSchools();
            PopulateSchoolYears();
            PopulateTakseis();
            PopulateStudents();

            return View();
        }

        public ActionResult DuplStudent_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<DuplStudentViewModel> data = GetDuplStudentsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
       }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DuplStudent_Destroy([DataSourceRequest] DataSourceRequest request, DuplStudentViewModel data)
        {
            if (data != null)
            {
                ΜΑΘΗΤΗΣ entity = db.ΜΑΘΗΤΗΣ.Find(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
                if (Kerberos.CanDeleteStudent(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ))
                {
                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.ΜΑΘΗΤΗΣ.Remove(entity);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή του μαθητή διότι υπάρχουν σχετιζόμενες αιτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public List<DuplStudentViewModel> GetDuplStudentsFromDB()
        {
            var data = (from s in db.dupl_STUDENT_DATA
                         orderby s.ΕΠΩΝΥΜΟ, s.ΟΝΟΜΑ, s.SCHOOL_NAME, s.EIDIKOTITA_TEXT
                         select new DuplStudentViewModel
                         {
                             ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = s.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                             ΣΧΟΛΗ = s.ΣΧΟΛΗ,
                             ΑΦΜ = s.ΑΦΜ,
                             ΕΠΩΝΥΜΟ = s.ΕΠΩΝΥΜΟ,
                             ΟΝΟΜΑ = s.ΟΝΟΜΑ,
                             ΠΑΤΡΩΝΥΜΟ = s.ΠΑΤΡΩΝΥΜΟ,
                             ΜΗΤΡΩΝΥΜΟ = s.ΜΗΤΡΩΝΥΜΟ,
                             EIDIKOTITA_TEXT = s.EIDIKOTITA_TEXT,
                             SCHOOL_NAME = s.SCHOOL_NAME,
                             ΑΔΤ = s.ΑΔΤ,
                             EIDIKOTITA_ID = s.EIDIKOTITA_ID
                         }).ToList();
            return (data);
        }

        #endregion

        #endregion


        #region ΑΙΤΗΣΕΙΣ ΜΑΘΗΤΩΝ (readonly grid with edit form)

        public ActionResult xAitiseisData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult Aitiseis_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = aitisiInfoService.Read(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAitiseisApofasiCode()
        {
            string msg = "Η ενημέρωση των κωδικών αποφάσεων στις αιτήσεις ολοκληρώθηκε.";

            var data1 = (from d in db.ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ orderby d.AITISI_ID select d).ToList();
            foreach (var item in data1)
            {
                ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.ΑΠΟΦΑΣΗ_ΚΩΔ = item.ΑΠΟΦΑΣΗ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data2 = (from d in db.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ orderby d.AITISI_ID select d).ToList();
            foreach (var item in data2)
            {
                ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.ΑΠΟΦΑΣΗ_ΚΩΔ = item.ΑΠΟΦΑΣΗ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data3 = (from d in db.ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ orderby d.AITISI_ID select d).ToList();
            foreach (var item in data3)
            {
                ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.ΑΠΟΦΑΣΗ_ΚΩΔ = item.ΑΠΟΦΑΣΗ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var data4 = (from d in db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ orderby d.AITISI_ID select d).ToList();
            foreach (var item in data4)
            {
                ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.ΑΠΟΦΑΣΗ_ΚΩΔ = item.ΑΠΟΦΑΣΗ_ΚΩΔ;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΟΙΝΩΝΙΚΑ ΚΡΙΤΗΡΙΑ ΑΙΤΗΣΗΣ

        public ActionResult AitisiSocial_Read([DataSourceRequest] DataSourceRequest request, int aitisiId)
        {
            var data = aitisiSocialService.Read(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AitisiSocial_Create([DataSourceRequest] DataSourceRequest request, AitisiSocialGroupViewModel data, int aitisiId)
        {
            var newData = new AitisiSocialGroupViewModel();

            if (data != null && ModelState.IsValid)
            {
                aitisiSocialService.Create(data, aitisiId);
                newData = aitisiSocialService.Refresh(data.AITISI_SOCIALID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AitisiSocial_Update([DataSourceRequest] DataSourceRequest request, AitisiSocialGroupViewModel data, int aitisiId)
        {
            var newData = new AitisiSocialGroupViewModel();

            if (data != null & ModelState.IsValid)
            {
                aitisiSocialService.Update(data, aitisiId);
                newData = aitisiSocialService.Refresh(data.AITISI_SOCIALID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AitisiSocial_Destroy([DataSourceRequest] DataSourceRequest request, AitisiSocialGroupViewModel data)
        {
            if (data != null)
            {
                aitisiSocialService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΜΑΘΗΤΩΝ

        public ActionResult xStudentInfoList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            StudentInfoViewModel student = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            PopulateSchools();
            return View(student);
        }

        public ActionResult StudentInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = studentInfoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AitiseisInfo_Read([DataSourceRequest] DataSourceRequest request, int studentId)
        {
            var data = studentInfoService.ReadAitiseis(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetStudentRecord(int studentId)
        {
            var data = studentInfoService.GetRecord(studentId);

            return PartialView("xStudentInfoPartial", data);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΑΙΤΗΣΕΩΝ

        public ActionResult xAitiseisInfoList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<sqlAitiseisViewModel> data = aitisiInfoService.Read();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlAitiseisViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public PartialViewResult GetAitisiRecord(int aitisiId)
        {
            var aitisi = aitisiInfoService.GetRecord(aitisiId);

            return PartialView("xAitiseisInfoPartial", aitisi);
        }

        #endregion


        #region ΠΡΟΒΟΛΗ ΚΑΙ ΕΚΤΥΠΩΣΕΙΣ ΜΗΤΡΩΟΥ ΑΙΤΗΣΕΩΝ

        #region ΕΓΚΡΙΘΕΙΣΕΣ ΑΙΤΗΣΕΙΣ

        public ActionResult xAitiseisAccept(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<AitiseisAcceptViewModel> data = GetAitiseisAcceptFromDB();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν εγκριθείσες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            AitiseisAcceptViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);
            
            return View(aitisi);
        }

        public ActionResult AitiseisAccept_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitiseisAcceptFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiAcceptRecord(int aitisiId)
        {
            var data = GetStudentAitiseisAcceptFromDB(aitisiId);

            return PartialView("xAitiseisAcceptPartial", data);
        }

        public AitiseisAcceptViewModel GetStudentAitiseisAcceptFromDB(int aitisiId)
        {
            var data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΓΚΡΙΣΗ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            var sdata = (from d in db.sqlSTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var xdata = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΓΚΡΙΣΗ
                         where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                         orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                         select new AitiseisAcceptViewModel
                         {
                            ΑΦΜ = d.ΑΦΜ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                            ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                            ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = sdata.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = sdata.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = sdata.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = sdata.ΠΑΡΑΤΗΡΗΣΕΙΣ
                         }).FirstOrDefault();

            return (xdata);
        }

        public List<AitiseisAcceptViewModel> GetAitiseisAcceptFromDB()
        {
            List<AitiseisAcceptViewModel> data = new List<AitiseisAcceptViewModel>();

            data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΓΚΡΙΣΗ
                    orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                    select new AitiseisAcceptViewModel
                    {
                        ΑΦΜ = d.ΑΦΜ,
                        ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                        EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                        SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                        SCHOOL_NAME = d.SCHOOL_NAME,
                        PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                        ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                        ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                        ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                        ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                        ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                        ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                        ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                        ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                        ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ
                    }).ToList();

            return (data);
        }

        public ActionResult xAitiseisAcceptPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion ΕΓΚΡΙΘΕΙΣΕΣ ΑΙΤΗΣΕΙΣ

        #region ΑΠΟΡΡΙΦΘΕΙΣΕΣ ΑΙΤΗΣΕΙΣ

        public ActionResult xAitiseisReject(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<AitiseisRejectViewModel> data = GetAitiseisRejectFromDB();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν απορριφθείσες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            AitiseisRejectViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public ActionResult AitiseisReject_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitiseisRejectFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiRejectRecord(int aitisiId)
        {
            var data = GetStudentAitiseisRejectFromDB(aitisiId);

            return PartialView("xAitiseisRejectPartial", data);
        }

        public AitiseisRejectViewModel GetStudentAitiseisRejectFromDB(int aitisiId)
        {
            var data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΑΠΟΡΡΙΨΗ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            var sdata = (from d in db.sqlSTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var xdata = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΑΠΟΡΡΙΨΗ
                         where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                         orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                         select new AitiseisRejectViewModel
                         {
                             ΑΦΜ = d.ΑΦΜ,
                             ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                             EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                             SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                             SCHOOL_NAME = d.SCHOOL_NAME,
                             PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                             ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                             ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                             ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                             ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                             ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                             ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                             ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                             ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                             ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
                             ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                             ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = sdata.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = sdata.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = sdata.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                             ΠΑΡΑΤΗΡΗΣΕΙΣ = sdata.ΠΑΡΑΤΗΡΗΣΕΙΣ
                         }).FirstOrDefault();

            return (xdata);
        }

        public List<AitiseisRejectViewModel> GetAitiseisRejectFromDB()
        {
            List<AitiseisRejectViewModel> data = new List<AitiseisRejectViewModel>();

            data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΑΠΟΡΡΙΨΗ
                    orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                    select new AitiseisRejectViewModel
                    {
                        ΑΦΜ = d.ΑΦΜ,
                        ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                        EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                        SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                        SCHOOL_NAME = d.SCHOOL_NAME,
                        PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                        ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                        ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                        ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                        ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                        ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                        ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                        ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                        ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                        ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                        ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ
                    }).ToList();

            return (data);
        }

        public ActionResult xAitiseisRejectPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }


        #endregion ΑΠΟΡΡΙΦΘΕΙΣΕΣ ΑΙΤΗΣΕΙΣ

        #region ΕΚΚΡΕΜΕΙΣ ΑΙΤΗΣΕΙΣ

        public ActionResult xAitiseisPending(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<AitiseisPendingViewModel> data = GetAitiseisPendingFromDB();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν εκκρεμείς αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            AitiseisPendingViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public ActionResult AitiseisPending_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitiseisPendingFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiPendingRecord(int aitisiId)
        {
            var data = GetStudentAitiseisPendingFromDB(aitisiId);

            return PartialView("xAitiseisPendingPartial", data);
        }

        public AitiseisPendingViewModel GetStudentAitiseisPendingFromDB(int aitisiId)
        {
            var data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΚΚΡΕΜΕΙΣ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            var sdata = (from d in db.sqlSTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var xdata = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΚΚΡΕΜΕΙΣ
                         where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                         orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                         select new AitiseisPendingViewModel
                         {
                             ΑΦΜ = d.ΑΦΜ,
                             ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                             EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                             SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                             SCHOOL_NAME = d.SCHOOL_NAME,
                             PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                             ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                             ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                             ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                             ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                             ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                             ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                             ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                             ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                             ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                             ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = sdata.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = sdata.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = sdata.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                             ΠΑΡΑΤΗΡΗΣΕΙΣ = sdata.ΠΑΡΑΤΗΡΗΣΕΙΣ
                         }).FirstOrDefault();

            return (xdata);
        }

        public List<AitiseisPendingViewModel> GetAitiseisPendingFromDB()
        {
            List<AitiseisPendingViewModel> data = new List<AitiseisPendingViewModel>();

            data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΚΚΡΕΜΕΙΣ
                    orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                    select new AitiseisPendingViewModel
                    {
                        ΑΦΜ = d.ΑΦΜ,
                        ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                        EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                        SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                        SCHOOL_NAME = d.SCHOOL_NAME,
                        PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                        ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                        ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                        ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                        ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                        ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                        ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                        ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                        ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                        ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                    }).ToList();

            return (data);
        }

        public ActionResult xAitiseisPendingPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region ΓΕΝΙΚΑ ΣΤΟΙΧΕΙΑ ΑΙΤΗΣΕΩΝ

        public ActionResult xAitiseisRegistry(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }

            IEnumerable<AitiseisRegistryViewModel> data = GetAitiseisRegistryFromDB();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            AitiseisRegistryViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public ActionResult AitiseisRegistry_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetAitiseisRegistryFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiRegistryRecord(int aitisiId)
        {
            var data = GetStudentAitiseisRegistryFromDB(aitisiId);

            return PartialView("xAitiseisRegistryPartial", data);
        }

        public AitiseisRegistryViewModel GetStudentAitiseisRegistryFromDB(int aitisiId)
        {
            var data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΜΗΤΡΩΟ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            var sdata = (from d in db.sqlSTUDENT_INFO where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var xdata = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΜΗΤΡΩΟ
                         where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                         orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                         select new AitiseisRegistryViewModel
                         {
                             ΑΦΜ = d.ΑΦΜ,
                             ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                             EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                             SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                             SCHOOL_NAME = d.SCHOOL_NAME,
                             PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                             ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                             ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                             ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                             ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                             ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                             ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                             ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                             ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                             ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                             ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                             ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
                             ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ = d.ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ,
                             ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                             ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = sdata.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = sdata.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = sdata.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                             ΠΑΡΑΤΗΡΗΣΕΙΣ = sdata.ΠΑΡΑΤΗΡΗΣΕΙΣ
                         }).FirstOrDefault();

            return (xdata);
        }

        public List<AitiseisRegistryViewModel> GetAitiseisRegistryFromDB()
        {
            List<AitiseisRegistryViewModel> data = new List<AitiseisRegistryViewModel>();

            data = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΜΗΤΡΩΟ
                    orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                    select new AitiseisRegistryViewModel
                    {
                        ΑΦΜ = d.ΑΦΜ,
                        ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                        EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                        SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                        SCHOOL_NAME = d.SCHOOL_NAME,
                        PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                        ΤΑΞΗ_ΛΕΚΤΙΚΟ = d.ΤΑΞΗ_ΛΕΚΤΙΚΟ,
                        ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                        ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ,
                        ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ = d.ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ,
                        ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                        ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ = d.ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                        ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ = d.ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ,
                        ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                        ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                        ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                        ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ = d.ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ,
                        ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = d.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ,
                        ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ = d.ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ,
                        ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ
                    }).ToList();

            return (data);
        }

        public ActionResult xAitiseisRegistryPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion ΓΕΝΙΚΑ ΣΤΟΙΧΕΙΑ ΑΙΤΗΣΕΩΝ

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ ΕΠΙΔΟΤΗΣΕΩΝ

        public ActionResult EpidomaOlikoPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaSitisiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaStegasiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaSynexeiaPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaXorigisiPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SumEpidomaAllTypesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult SumEpidomaDetailPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion


        #region ΣΤΑΤΙΣΤΙΚΑ

        // Added 28-01-2022
        public ActionResult AitiseisIncomePrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AitiseisAgesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AitiseisGenderPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AitiseisSocialPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaPeriferiakes1Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EpidomaPeriferiakes2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DapaniEpidomaType1Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DapaniEpidomaType2Print()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                return View();
            }
        }

        #endregion

    }
}