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
    public class SchoolController : ControllerUnit
    {
        private USER_SCHOOLS loggedSchool;
        private readonly PrimusDBEntities db;

        private readonly IStudentService studentService;
        private readonly IAitisiService aitisiService;
        private readonly IAitisiSocialService aitisiSocialService;
        private readonly IStudentInfoService studentInfoService;
        private readonly IAitisi2Service aitisi2Service;
        private readonly IAitisi2SocialService aitisi2SocialService;
        private readonly IStudentInfo2Service studentInfo2Service;
        private readonly IAitisiInfoService aitisiInfoService;

        private readonly IApofasiSitisi2Service apofasiSitisi2Service;
        private readonly IEpidomaSitisi2Service epidomaSitisi2Service;

        public SchoolController(PrimusDBEntities entities, IStudentService studentService, IAitisiService aitisiService,
            IAitisiSocialService aitisiSocialService, IStudentInfoService studentInfoService, IAitisi2Service aitisi2Service,
            IAitisi2SocialService aitisi2SocialService, IStudentInfo2Service studentInfo2Service, IAitisiInfoService aitisiInfoService,
            IApofasiSitisi2Service apofasiSitisi2Service, IEpidomaSitisi2Service epidomaSitisi2Service) : base(entities)
        {
            db = entities;

            this.studentService = studentService;
            this.aitisiService = aitisiService;
            this.aitisiSocialService = aitisiSocialService;
            this.studentInfoService = studentInfoService;
            this.aitisi2Service = aitisi2Service;
            this.aitisi2SocialService = aitisi2SocialService;
            this.studentInfo2Service = studentInfo2Service;
            this.aitisiInfoService = aitisiInfoService;

            this.apofasiSitisi2Service = apofasiSitisi2Service;
            this.epidomaSitisi2Service = epidomaSitisi2Service;
        }


        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            return View();
        }

        #region ΣΤΟΙΧΕΙΑ ΣΧΟΛΕΙΟΥ

        public ActionResult SchoolEdit()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            SchoolsViewModel school = GetSchoolViewModelFromDB();
            if (school == null)
            {
                return HttpNotFound();
            }

            SchoolsViewModel schoolData = GetSchoolViewModelFromDB();
            return View(schoolData);
        }

        [HttpPost]
        public ActionResult SchoolEdit(SchoolsViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (ModelState.IsValid)
            {
                ΣΥΣ_ΣΧΟΛΕΣ modschool = db.ΣΥΣ_ΣΧΟΛΕΣ.Find(schoolId);

                modschool.SCHOOL_NAME = model.SCHOOL_NAME.Trim();
                modschool.SCHOOL_TYPE = model.SCHOOL_TYPE;
                modschool.ΓΡΑΜΜΑΤΕΙΑ = model.ΓΡΑΜΜΑΤΕΙΑ.Trim();
                modschool.ΔΙΕΥΘΥΝΤΗΣ = model.ΔΙΕΥΘΥΝΤΗΣ.Trim();
                modschool.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = model.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                modschool.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = model.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ.Trim();
                modschool.ΤΗΛΕΦΩΝΑ = model.ΤΗΛΕΦΩΝΑ.Trim();
                modschool.ΦΑΞ = model.ΦΑΞ.Trim();
                modschool.EMAIL = model.EMAIL.Trim();
                modschool.ΚΙΝΗΤΟ = model.ΚΙΝΗΤΟ;
                modschool.ΥΠΟΔΙΕΥΘΥΝΤΗΣ = model.ΥΠΟΔΙΕΥΘΥΝΤΗΣ.HasValue() ? model.ΥΠΟΔΙΕΥΘΥΝΤΗΣ.Trim() : model.ΥΠΟΔΙΕΥΘΥΝΤΗΣ;
                modschool.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = model.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                modschool.ΠΕΡΙΦΕΡΕΙΑΚΗ = model.ΠΕΡΙΦΕΡΕΙΑΚΗ;

                db.Entry(modschool).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(model);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(model);
        }

        public SchoolsViewModel GetSchoolViewModelFromDB()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = (from s in db.ΣΥΣ_ΣΧΟΛΕΣ
                        where s.SCHOOL_ID == schoolId
                        select new SchoolsViewModel
                        {
                            SCHOOL_ID = s.SCHOOL_ID,
                            SCHOOL_NAME = s.SCHOOL_NAME,
                            SCHOOL_TYPE = s.SCHOOL_TYPE,
                            ΔΙΕΥΘΥΝΤΗΣ = s.ΔΙΕΥΘΥΝΤΗΣ,
                            ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                            ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = s.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = s.ΤΗΛΕΦΩΝΑ,
                            ΓΡΑΜΜΑΤΕΙΑ = s.ΓΡΑΜΜΑΤΕΙΑ,
                            ΦΑΞ = s.ΦΑΞ,
                            EMAIL = s.EMAIL,
                            ΚΙΝΗΤΟ = s.ΚΙΝΗΤΟ,
                            ΥΠΟΔΙΕΥΘΥΝΤΗΣ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ,
                            ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = s.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = s.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).FirstOrDefault();
            return data;
        }

        #endregion


        #region ΜΑΘΗΤΕΣ ΚΑΙ ΑΙΤΗΣΕΙΣ

        #region ΜΑΘΗΤΕΣ

        public ActionResult StudentData(int studentId = 0, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            if (!Kerberos.EidikotitesExist())
            {
                string msg = "Για να καταχωρηθούν μαθητές πρέπει πρώτα να ορίσετε τις ειδικότητες στις Ρυθμίσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateEidikotites(schoolId);
            PopulateSchools();
            PopulateSchoolYears();
            PopulateTakseis();
            PopulateStudents(schoolId);
            return View();
        }

        #region ΜΑΘΗΤΕΣ GRID CRUD FUNCTIONS

        public ActionResult Student_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = studentService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Create([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newData = new StudentGridViewModel();

            if (!Common.CheckAFM(data.ΑΦΜ)) 
                ModelState.AddModelError("", "Το ΑΦΜ που καταχωρήσατε δεν είναι έγκυρο.");
            if (!Kerberos.ValidatePrimaryKeyStudent(data.ΑΦΜ, schoolId, (int)data.ΕΙΔΙΚΟΤΗΤΑ)) 
                ModelState.AddModelError("", "Το ΑΦΜ και η ειδικότητα που δώσατε είναι ήδη καταχωρημένα για το σχολείο αυτό.");

            if (data != null && ModelState.IsValid)
            {
                studentService.Create(data, schoolId);
                studentService.CopyStudentData(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ, data.ΑΦΜ);

                newData = studentService.Refresh(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Update([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int? oldEidikotita = 0;
            bool editEidikotitaOK = true;

            var newData = new StudentGridViewModel();

            var src = (from d in db.ΜΑΘΗΤΗΣ where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();
            if (src != null)  oldEidikotita = src.ΕΙΔΙΚΟΤΗΤΑ;
            if (oldEidikotita != data.ΕΙΔΙΚΟΤΗΤΑ) editEidikotitaOK = Common.CanEditEidikotita(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);

            if (!editEidikotitaOK) 
            { 
                ModelState.AddModelError("", "Δεν μπορεί να γίνει αλλαγή της ειδικότητας διότι υπάρχουν αιτήσεις του μαθητή με αυτή.");
            }

            if (data != null && ModelState.IsValid)
            {
                studentService.Update(data, schoolId);
                newData = studentService.Refresh(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Student_Destroy([DataSourceRequest] DataSourceRequest request, StudentGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteStudent(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ))
                {
                    studentService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή του μαθητή διότι έχει συσχετισμένες αιτήσεις.");
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

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
                    PopulateStudents(schoolId);
                }
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

                if (!Common.CanEditAitisi(data.ΑΙΤΗΣΗ_ΚΩΔ))
                    ModelState.AddModelError("","Δεν μπορεί να γίνει επεξεργασία της αίτησης διότι υπάρχει σε απόφαση.");

                if (data != null && ModelState.IsValid)
                {
                    aitisiService.Update(data, studentId);
                    newData = aitisiService.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
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

        public ActionResult StudentEdit(int studentId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            StudentViewModel student = studentService.GetRecord(studentId);
            if (student == null)
            {
                string msg = "Δεν βρέθηκε μαθητής. Πρέπει πρώτα να γίνει ενημέρωση των στοιχείων από το πλέγμα.";
                return RedirectToAction("StudentData", "School", new { notify = msg });
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentEdit(int studentId, StudentViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)GetLoginSchool() .USER_SCHOOLID;

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
                studentService.UpdateRecord(model, studentId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StudentViewModel newStudent = studentService.GetRecord(studentId);
                return View(newStudent);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων χρήστη στην καταχώρηση.");
            return View(model);
        }

        #endregion


        #region AITISI DATA FORM

        public ActionResult AitisiEdit(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (!(aitisiId > 0))
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("StudentData", "School", new { notify = msg });
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
                return RedirectToAction("Index", "School", new { notify });
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
        public ActionResult AitisiEdit(int aitisiId, AitisiViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            PopulateSocialGroups();

            data = SetAitisiFields(data, aitisiId);

            qrySTUDENT_INFO SelectedStudent = Common.GetStudentInfo(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            StudentViewModel student = studentService.GetRecord(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης μαθητή.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            if (!Common.CanEditAitisi(aitisiId))
            {
                this.ShowMessage(MessageType.Warning, "Δεν μπορεί να γίνει επεξεργασία της αίτησης διότι είναι σε απόφαση. ");
                return View(data);
            }

            string ErrorMsg = ValidateAitisiFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                aitisiService.UpdateRecord(data, student, aitisiId);

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

        // Χρειάζονται διαφορετικοί κανόνες επικύρωσης από αυτούς των διαχειριστών
        // Τροποποιήθηκε 17/11/2022
        public string ValidateAitisiFields(AitisiViewModel aitisi)
        {
            string ErrMsg = "";

            if (!(aitisi.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ > 0))
            {
                ErrMsg += "-> Πρέπει να επιλεγεί ένα είδος επιδόματος.";
            }
            if (!Common.ValidateAitisiDate(aitisi))
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

        #endregion AITISI DATA FORM

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

        public ActionResult StudentInfoList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentInfoViewModel> data = studentInfoService.Read(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
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
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = studentInfoService.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AitiseisInfo_Read(int studentId, [DataSourceRequest] DataSourceRequest request)
        {
            var data = studentInfoService.ReadAitiseis(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method will return PartialView with Student Model
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public PartialViewResult GetStudentRecord(int studentId)
        {
            var data = studentInfoService.GetRecord(studentId);

            return PartialView("StudentInfoPartial", data);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΑΙΤΗΣΕΩΝ

        public ActionResult AitiseisInfoList(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            int schoolyearId = 0;   // display all aitiseis of school
            IEnumerable<sqlAitiseisViewModel> data = aitisiInfoService.Read(schoolId, schoolyearId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            sqlAitiseisViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public ActionResult Aitiseis_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = aitisiInfoService.Read(schoolId, schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiRecord(int aitisiId)
        {
            var aitisi = aitisiInfoService.GetRecord(aitisiId);

            return PartialView("AitiseisInfoPartial", aitisi);
        }

        #endregion


        #region ΑΠΟΦΑΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΩΝ

        #region ΜΑΘΗΤΕΣ ΚΑΙ ΑΙΤΗΣΕΙΣ ΣΧΟΛΕΙΟΥ

        public ActionResult StudentData2(int studentId = 0, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!Kerberos.EidikotitesExist())
            {
                string msg = "Για να καταχωρηθούν μαθητές πρέπει πρώτα να ορίσετε τις ειδικότητες στις Ρυθμίσεις.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }

            PopulateEidikotites(schoolId);
            PopulateSchools();
            PopulateSchoolYears();
            PopulateTakseis();
            PopulateStudents(schoolId);
            return View();
        }

        #region ΑΙΤΗΣΕΙΣ2 GRID CRUD FUNCTIONS

        public ActionResult Aitisi2_Read([DataSourceRequest] DataSourceRequest request, int studentId = 0)
        {
            var data = aitisi2Service.Read(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2_Create([DataSourceRequest] DataSourceRequest request, Aitisi2GridViewModel data, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            Aitisi2GridViewModel newData = new Aitisi2GridViewModel();
            if (studentId > 0)
            {
                if (!Common.DateInSchoolYear((DateTime)data.ΗΜΝΙΑ_ΑΙΤΗΣΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                    ModelState.AddModelError("", "Η ημερομηνία αίτησης είναι εκτός σχολικού έτους.");

                if (Kerberos.StudentAitisi2Exists(data, studentId))
                    ModelState.AddModelError("", "Υπάρχεί ήδη αίτηση του μαθητή γι' αυτό το σχολικό έτος και τάξη. Η καταχώρηση ακυρώθηκε.");

                if (data != null && ModelState.IsValid)
                {
                    aitisi2Service.Create(data, studentId, schoolId);
                    newData = aitisi2Service.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μαθητή. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2_Update([DataSourceRequest] DataSourceRequest request, Aitisi2GridViewModel data, int studentId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            Aitisi2GridViewModel newData = new Aitisi2GridViewModel();
            if (studentId > 0)
            {
                if (!Common.DateInSchoolYear((DateTime)data.ΗΜΝΙΑ_ΑΙΤΗΣΗ, (int)data.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                    ModelState.AddModelError("", "Η ημερομηνία αίτησης είναι εκτός σχολικού έτους.");

                if (data != null && ModelState.IsValid)
                {
                    aitisi2Service.Update(data, studentId, schoolId);
                    newData = aitisi2Service.Refresh(data.ΑΙΤΗΣΗ_ΚΩΔ);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε μαθητή. Η καταχώρηση ακυρώθηκε.");
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2_Destroy([DataSourceRequest] DataSourceRequest request, Aitisi2GridViewModel data)
        {
            if (data != null)
            {
                if (!Kerberos.CanDeleteAitisi2(data.ΑΙΤΗΣΗ_ΚΩΔ))
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της αίτησης διότι έχει συσχετισμένα κοινωνικά κριτήρια.");
                    return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
                try
                {
                    aitisi2Service.Destroy(data);
                }
                catch
                {
                    ModelState.AddModelError("", "Προέκυψε κάποιο σφάλμα κατά τη διαγραφή.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΟΙΝΩΝΙΚΑ ΚΡΙΤΗΡΙΑ ΑΙΤΗΣΗΣ2

        public ActionResult Aitisi2Social_Read([DataSourceRequest] DataSourceRequest request, int aitisiId)
        {
            var data = aitisi2SocialService.Read(aitisiId);
            
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2Social_Create([DataSourceRequest] DataSourceRequest request, Aitisi2SocialGroupViewModel data, int aitisiId)
        {
            var newData = new Aitisi2SocialGroupViewModel();

            if (data != null && ModelState.IsValid)
            {
                aitisi2SocialService.Create(data, aitisiId);
                newData = aitisi2SocialService.Refresh(data.AITISI_SOCIALID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2Social_Update([DataSourceRequest] DataSourceRequest request, Aitisi2SocialGroupViewModel data, int aitisiId)
        {
            var newData = new Aitisi2SocialGroupViewModel();

            if (data != null & ModelState.IsValid)
            {
                aitisi2SocialService.Update(data, aitisiId);
                newData = aitisi2SocialService.Refresh(data.AITISI_SOCIALID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi2Social_Destroy([DataSourceRequest] DataSourceRequest request, Aitisi2SocialGroupViewModel data)
        {
            if (data != null)
            {
                aitisi2SocialService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region AITISI2 DATA FORM

        public ActionResult Aitisi2Edit(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            if (!(aitisiId > 0))
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("StudentData2", "School", new { notify = msg });
            }

            Aitisi2ViewModel aitisi = aitisi2Service.GetRecord(aitisiId);
            if (aitisi == null)
            {
                string msg = "Ο κωδικός αίτησης δεν είναι έγκυρος. Η εγγραφή πρέπει να έχει αποθηκευτεί πρώτα.";
                return RedirectToAction("StudentData2", "School", new { notify = msg });
            }
            int studentId = (int)aitisi.ΜΑΘΗΤΗΣ_ΚΩΔ;

            qrySTUDENT_INFO SelectedStudent = Common.GetStudentInfo(studentId);

            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης μαθητή.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }
            PopulateSocialGroups();
            return View(aitisi);
        }

        [HttpPost]
        public ActionResult Aitisi2Edit(int aitisiId, Aitisi2ViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            PopulateSocialGroups();
            data = SetAitisi2Fields(data, aitisiId);

            qrySTUDENT_INFO SelectedStudent = Common.GetStudentInfo(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            StudentViewModel student = studentService.GetRecord(data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            if (SelectedStudent == null)
            {
                string notify = "Παρουσιάστηκε σφάλμα εύρεσης μαθητή.";
                return RedirectToAction("Index", "School", new { notify });
            }
            else
            {
                ViewBag.StudentData = SelectedStudent;
            }

            string ErrorMsg = Common.ValidateAitisi2Fields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                aitisi2Service.UpdateRecord(data, student, aitisiId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                Aitisi2ViewModel newAitisi = aitisi2Service.GetRecord(aitisiId);
                return View(newAitisi);
            }
            else
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
                return View(data);
            }
        }

        #endregion AITISI DATA FORM

        #endregion


        #region ΜΗΤΡΩΟ ΜΑΘΗΤΩΝ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΟΥ

        public ActionResult StudentInfo2List(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            IEnumerable<StudentInfo2ViewModel> data = studentInfo2Service.Read(schoolId);
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένοι σπουδαστές για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            StudentInfo2ViewModel student = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            PopulateSchools();
            return View(student);
        }

        public ActionResult StudentInfo2_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            IEnumerable<StudentInfo2ViewModel> data = studentInfo2Service.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AitiseisInfo2_Read(int studentId, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<sqlAitiseis2ViewModel> data = studentInfo2Service.ReadAitiseis(studentId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetStudentRecord2(int studentId)
        {
            var data = studentInfo2Service.GetRecord(studentId);

            return PartialView("StudentInfo2Partial", data);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΑΙΤΗΣΕΩΝ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΟΥ

        public ActionResult AitiseisInfo2List(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            IEnumerable<sqlAitiseis2ViewModel> data = GetAitiseis2ViewFromDB();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αιτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "School", new { notify = msg });
            }
            sqlAitiseis2ViewModel aitisi = data.First();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            return View(aitisi);
        }

        public ActionResult Aitiseis2_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = GetAitiseis2ViewFromDB(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAitisiRecord2(int aitisiId)
        {
            var data = GetAitiseis2RegistryFromDB(aitisiId);

            return PartialView("AitiseisInfo2Partial", data);
        }

        public sqlAitiseis2ViewModel GetAitiseis2RegistryFromDB(int aitisiId)
        {
            var data = (from d in db.sqlSTUDENT_AITISEIS2 where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId select d).FirstOrDefault();
            var sdata = (from d in db.sqlSTUDENT_INFO2 where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ select d).FirstOrDefault();

            var xdata = (from d in db.sqlSTUDENT_AITISEIS2
                         where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                         orderby d.SCHOOLYEAR_TEXT, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                         select new sqlAitiseis2ViewModel
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
                             ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = sdata.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                             ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = sdata.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                             ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                             ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ = sdata.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ,
                             ΠΑΡΑΤΗΡΗΣΕΙΣ = sdata.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                             ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = sdata.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                             ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = sdata.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                             ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = sdata.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                             ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = sdata.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                             ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ = sdata.ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ
                         }).FirstOrDefault();

            return (xdata);
        }

        public List<sqlAitiseis2ViewModel> GetAitiseis2ViewFromDB(int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<sqlAitiseis2ViewModel> data = new List<sqlAitiseis2ViewModel>();
            if (schoolyearId > 0)
            {
                data = (from d in db.sqlSTUDENT_AITISEIS2
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΤΑΞΗ_ΛΕΚΤΙΚΟ
                        select new sqlAitiseis2ViewModel
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
                data = (from d in db.sqlSTUDENT_AITISEIS2
                        where d.ΣΧΟΛΗ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new sqlAitiseis2ViewModel
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
            return (data);
        }

        #endregion


        #region ΠΛΕΓΜΑ ΑΠΟΦΑΣΕΩΝ ΣΧΟΛΕΙΟΥ

        public ActionResult ApofaseisSitisi2(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateMonths();
            PopulateYears();
            // Populators for child grid
            PopulateStudents(schoolId);
            PopulateEidikotites(schoolId);
            PopulateTakseis();

            return View();
        }

        public ActionResult ApofasiSitisi2_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var data = apofasiSitisi2Service.Read(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi2_Create([DataSourceRequest] DataSourceRequest request, ApofasiSitisi2ViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            ApofasiSitisi2ViewModel newData = new ApofasiSitisi2ViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSitisi2Service.Create(data, schoolId);
                newData = apofasiSitisi2Service.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi2_Update([DataSourceRequest] DataSourceRequest request, ApofasiSitisi2ViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            ApofasiSitisi2ViewModel newData = new ApofasiSitisi2ViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSitisi2Service.Update(data, schoolId);
                newData = apofasiSitisi2Service.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi2_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSitisi2ViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSitisi2(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSitisi2Service.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει επισυναπτόμενες επιδοτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS


        #region EPIDOMA GRID CRUD FUNCTIONS

        public ActionResult EpidomaSitisi2_Read([DataSourceRequest] DataSourceRequest request, EpidomaParameters2 ep)
        {
            var data = epidomaSitisi2Service.Read(ep);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi2_Create([DataSourceRequest] DataSourceRequest request, EpidomaSitisi2ViewModel data, EpidomaParameters2 ep)
        {
            EpidomaSitisi2ViewModel newData = new EpidomaSitisi2ViewModel();

            // Για να δημιουργηθεί επιδότηση πρέπει να υπάρχει αντίστοιχη αίτηση
            ep.synexeia = false;    // this is not set in jquery
            var relatedAitisi = epidomaSitisi2Service.GetRelatedAitisi2((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ, ep);
            if (relatedAitisi == null)
            {
                ModelState.AddModelError("", "Δεν μπορεί να καταχωρηθεί νέα επιδότηση χωρίς αντίστοιχη αίτηση.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            if (ep.apofasiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epidomaSitisi2Service.Create(data, ep, relatedAitisi);
                    newData = epidomaSitisi2Service.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

                    return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.AddModelError("", "Δεν έχει γίνει επιλογή κάποιας απόφασης. Η δημιουργία επιδότησης ακυρώθηκε.");
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi2_Update([DataSourceRequest] DataSourceRequest request, EpidomaSitisi2ViewModel data, EpidomaParameters2 ep)
        {
            EpidomaSitisi2ViewModel newData = new EpidomaSitisi2ViewModel();
            ep.synexeia = false;    // not set in jquery

            if (data != null && ModelState.IsValid)
            {
                epidomaSitisi2Service.Update(data, ep);
                newData = epidomaSitisi2Service.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi2_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaSitisi2ViewModel data)
        {
            if (data != null)
            {
                epidomaSitisi2Service.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion EPIDOMA GRID CRUD FUNCTIONS


        #region ΚΑΡΤΕΛΑ ΑΠΟΦΑΣΗΣ ΣΙΤΙΣΗΣ

        public ActionResult ApofasiSitisi2Edit(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            ApofasiSitisi2ViewModel data = apofasiSitisi2Service.GetRecord(apofasiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            // Set default field values
            data = SetDefaultSitisi2Fields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiSitisi2Edit(int apofasiId, ApofasiSitisi2ViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            string ErrorMsg = Common.ValidateApofasiSitisi2Fields(model);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(model);
            }

            if (ModelState.IsValid)
            {
                apofasiSitisi2Service.UpdateRecord(model, apofasiId, schoolId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSitisi2ViewModel newApofasi = apofasiSitisi2Service.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης του χρήστη.");
            return View(model);
        }

        public ApofasiSitisi2ViewModel SetDefaultSitisi2Fields(ApofasiSitisi2ViewModel data, int apofasiId)
        {
            var totals = (from d in db.sumΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2_ΜΗΝΑΣ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).FirstOrDefault();
            if (totals != null)
            {
                data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = totals.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
                data.ΠΛΗΘΟΣ = (short)totals.ΠΛΗΘΟΣ;
                data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = Common.StudentNumberToText((int)totals.ΠΛΗΘΟΣ);
            }
            var data2 = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΣΙΤΙΣΗ_ΠΟΣΟ = data2.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ * 30.0m;
            }
            int school_type = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            var data3 = (from d in db.APOFASI_PARAMETERS2 select d).FirstOrDefault();
            if (data3 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data3.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data3.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                data.ΕΓΚΥΚΛΙΟΙ_Α2 = data3.ΕΓΚΥΚΛΙΟΙ_Α2;
            }
            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ";
            if (data.ΠΛΗΘΟΣ == null) data.ΠΛΗΘΟΣ = 0;

            return (data);
        }

        #endregion


        #region ΕΠΙΣΥΝΑΨΗ ΑΙΤΗΣΕΩΝ

        public bool ApofasiSitisi2Attach(EpidomaParameters2 ep)
        {
            //string message = null;
            ep.synexeia = false;

            var source = (from d in db.sqlΑΙΤΗΣΕΙΣ2_ΕΠΙΣΥΝΑΨΗ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId && d.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                return false;
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη αίτηση προς αποφυγή διπλοεγγραφών.
                int epidotisiCount = (from e in db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 
                                      where e.AITISI_ID == d.ΑΙΤΗΣΗ_ΚΩΔ && e.ΣΧΟΛΙΚΟ_ΕΤΟΣ == d.ΣΧΟΛΙΚΟ_ΕΤΟΣ && e.ΜΗΝΑΣ == ep.monthId 
                                      select e).Count();
                if (epidotisiCount == 0)
                {
                    ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 target = new ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2()
                    {
                        ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId,
                        ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ",
                        ΜΗΝΑΣ = apofasi.ΜΗΝΑΣ,
                        ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = sintaksi_date,
                        ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                        ΣΥΝΕΧΙΣΗ = d.ΣΥΝΕΧΕΙΑ,
                        AITISI_ID = d.ΑΙΤΗΣΗ_ΚΩΔ,
                        ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                        ΣΧΟΛΕΙΟ = d.ΣΧΟΛΗ,
                        ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                        ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                        ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΑΦΜ,
                        ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                        ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                        ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                        ΜΑΘΗΤΗΣ_ΤΑΞΗ = d.ΤΑΞΗ,
                        ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                        ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                    };
                    db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Add(target);
                    db.SaveChanges();
                }
            }
            return true;
        }

        #endregion


        #region ΚΑΡΤΕΛΑ ΕΠΙΔΟΤΗΣΗΣ (Currently not used)

        public ActionResult EpidomaSitisi2Edit(int epidotisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            EpidomaSitisi2ViewModel data = epidomaSitisi2Service.GetRecord(epidotisiId);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult EpidomaSitisi2Edit(int epidotisiId, EpidomaSitisi2ViewModel model)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            if (ModelState.IsValid)
            {
                ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 entity = db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Find(epidotisiId);

                entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = model.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ;
                entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = model.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ;
                entity.ΗΜΝΙΑ_ΑΠΟ = model.ΗΜΝΙΑ_ΑΠΟ;
                entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = model.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
                entity.ΜΑΘΗΤΗΣ_ΑΦΜ = model.ΜΑΘΗΤΗΣ_ΑΦΜ;
                entity.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = model.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ;
                entity.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = model.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ;
                entity.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = model.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ;
                entity.ΜΑΘΗΤΗΣ_ΤΑΞΗ = model.ΜΑΘΗΤΗΣ_ΤΑΞΗ;
                entity.ΣΙΤΙΣΗ_ΠΟΣΟ = model.ΣΙΤΙΣΗ_ΠΟΣΟ;
                entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = model.ΣΤΕΓΑΣΗ_ΠΟΣΟ;
                entity.ΣΥΝΕΧΙΣΗ = model.ΣΥΝΕΧΙΣΗ;
                entity.ΣΧΟΛΕΙΟ = schoolId;
                entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = model.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                entity.ΜΗΝΑΣ = model.ΜΗΝΑΣ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");

                EpidomaSitisi2ViewModel newData = epidomaSitisi2Service.GetRecord(epidotisiId);

                return View(newData);
            }

            return View(model);
        }

        #endregion


        #region ΕΚΤΥΠΩΣΗ ΑΠΟΦΑΣΗΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΟΥ

        public ActionResult ApofasiSitisi2Print(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            var data = (from d in db.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiSitisi2ViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΜΗΝΑΣ = d.ΜΗΝΑΣ
                        }).FirstOrDefault();
            
            return View(data);
        }


        #endregion

        #endregion ΑΠΟΦΑΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΩΝ


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΟΥ

        public ActionResult EpidomaSitisi2(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<sqlEpidomaSitisiSchoolViewModel> data = GetEpidotiseisSitisi2FromDB(schoolId);

            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaSitisiSchoolViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiSitisi2_Read([DataSourceRequest] DataSourceRequest request)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            List<sqlEpidomaSitisiSchoolViewModel> data = GetEpidotiseisSitisi2FromDB(schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlEpidomaSitisiSchoolViewModel> GetEpidotiseisSitisi2FromDB(int schoolId)
        {
            var data = (from d in db.sqlEPIDOMA_SITISI2
                        where d.ΣΧΟΛΕΙΟ == schoolId
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
            return data;
        }

        public sqlEpidomaSitisiSchoolViewModel GetEpidotisiSitisi2ById(int epidomaId)
        {
            var data = (from d in db.sqlEPIDOMA_SITISI2
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

        public PartialViewResult GetEpidotisiSitisi2Record(int epidomaId)
        {
            var data = GetEpidotisiSitisi2ById(epidomaId);

            return PartialView("EpidomaSitisi2Partial", data);
        }

        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΟΥ


        #region ΣΤΑΤΙΣΤΙΚΑ

        public ActionResult AitiseisAgesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        public ActionResult AitiseisGenderPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        public ActionResult AitiseisSocialPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
                return View();
            }
        }

        #endregion

    }
}