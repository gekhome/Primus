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

namespace Primus.Controllers.SysControllers
{
    [ErrorHandlerFilter]
    public class SetupController : DataControllers.ControllerUnit
    {
        private USER_ADMINS loggedAdmin;

        private readonly PrimusDBEntities db;

        private readonly IUserSchoolService userSchoolService;
        private readonly ISchoolYearService schoolYearService;
        private readonly IEidikotitesService eidikotitesService;
        private readonly ISchoolDataService schoolDataService;
        private readonly IEidikotitesSchoolService eidikotitesSchoolService;
        private readonly IEpidomaTypeService epidomaTypeService;
        private readonly IEpidomaPosoService epidomaPosoService;
        private readonly ISocialGroupService socialGroupService;
        private readonly IAporipsiAitiaService aporipsiAitiaService;
        private readonly IParametersService parametersService;
        private readonly IDiaxriristesService diaxriristesService;
        private readonly IProistamenoiService proistamenoiService;
        private readonly IDirectorsService directorsService;
        private readonly IGenikoiService genikoiService;
        private readonly IAntiproedroiService antiproedroiService;
        private readonly IDioikitesService dioikitesService;

        public SetupController(PrimusDBEntities entities, IUserSchoolService userSchoolService, ISchoolYearService schoolYearService,
            IEidikotitesService eidikotitesService, ISchoolDataService schoolDataService, IEidikotitesSchoolService eidikotitesSchoolService,
            IEpidomaTypeService epidomaTypeService, IEpidomaPosoService epidomaPosoService, ISocialGroupService socialGroupService,
            IAporipsiAitiaService aporipsiAitiaService, IParametersService parametersService, IDiaxriristesService diaxriristesService,
            IProistamenoiService proistamenoiService, IDirectorsService directorsService, IGenikoiService genikoiService,
            IAntiproedroiService antiproedroiService, IDioikitesService dioikitesService) : base(entities)
        {
            db = entities;

            this.userSchoolService = userSchoolService;
            this.schoolYearService = schoolYearService;
            this.eidikotitesService = eidikotitesService;
            this.schoolDataService = schoolDataService;
            this.eidikotitesSchoolService = eidikotitesSchoolService;
            this.epidomaTypeService = epidomaTypeService;
            this.epidomaPosoService = epidomaPosoService;
            this.socialGroupService = socialGroupService;
            this.aporipsiAitiaService = aporipsiAitiaService;
            this.parametersService = parametersService;
            this.diaxriristesService = diaxriristesService;
            this.proistamenoiService = proistamenoiService;
            this.directorsService = directorsService;
            this.genikoiService = genikoiService;
            this.antiproedroiService = antiproedroiService;
            this.dioikitesService = dioikitesService;
        }


        #region SCHOOL ACCOUNTS (USER_SCHOOL)

        public ActionResult UserSchoolList(string notify = null)
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
            if (notify != null) this.ShowMessage(MessageType.Info, notify);

            populateUserSchools();
            return View();
        }

        public ActionResult CreatePasswords()
        {
            var schools = (from s in db.USER_SCHOOLS select s).ToList();

            Random rnd = new Random();
            foreach (var school in schools)
            {
                school.PASSWORD = Common.GeneratePassword(rnd) + string.Format("{0:000}", school.USER_SCHOOLID);
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
            }

            string notify = "Η δημιουργία νέων κωδικών σχολείων ολοκληρώθηκε.";
            return RedirectToAction("UserSchoolList", "Setup", new { notify });
        }

        #region GRID CRUD FUNCTIONS

        [HttpPost]
        public ActionResult UserSchool_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = userSchoolService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserSchool_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            var results = new List<UserSchoolViewModel>();
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    userSchoolService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            if (data != null)
            {
                foreach (var item in data)
                {
                    userSchoolService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserSchool_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserSchoolViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    userSchoolService.Destroy(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SchoolAccountsPrint()
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


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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
            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Create([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            var newData = new SysSchoolYearViewModel();

            var existingSchoolYears = db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Where(s => s.SCHOOLYEAR_TEXT == data.SCHOOLYEAR_TEXT).Count();
            if (existingSchoolYears > 0)
                ModelState.AddModelError("", "Το σχολικό έτος είναι ήδη καταχωρημένο. Η διαδικασία ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolYearService.Create(data);
                newData = schoolYearService.Refresh(data.SCHOOLYEAR_ID); 
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Update([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            var newData = new SysSchoolYearViewModel();

            if (data != null & ModelState.IsValid)
            {
                schoolYearService.Update(data);
                newData = schoolYearService.Refresh(data.SCHOOLYEAR_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Destroy([DataSourceRequest] DataSourceRequest request, SysSchoolYearViewModel data)
        {
            if (data != null)
            {
                schoolYearService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΕΠΑΣ, ΣΕΚ-ΠΣΕΚ

        public ActionResult EidikotitesList()
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

            PopulateSchoolTypes();
            return View();
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = eidikotitesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Create([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            var newData = new EidikotitesViewModel();

            var existingData = db.ΕΙΔΙΚΟΤΗΤΕΣ.Where(s => s.EIDIKOTITA_TEXT == data.EIDIKOTITA_TEXT).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Η ειδικότητα αυτή είναι ήδη καταχωρημένη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                eidikotitesService.Create(data);
                newData = eidikotitesService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Update([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            var newData = new EidikotitesViewModel();

            if (data != null & ModelState.IsValid)
            {
                eidikotitesService.Update(data);
                newData = eidikotitesService.Refresh(data.EIDIKOTITA_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Eidikotita_Destroy([DataSourceRequest] DataSourceRequest request, EidikotitesViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteEidikotita(data.EIDIKOTITA_ID))
                {
                    eidikotitesService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η ειδικότητα διότι υπάρχουν μαθητές και σχολεία συσχετισμένα με αυτή.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EidikotitesPrint()
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


        #region ΣΧΟΛΕΣ (ΣΤΟΙΧΕΙΑ)

        public ActionResult SchoolsList()
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

            PopulateSchoolDomes();
            return View();
        }

        public ActionResult School_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolDataService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Create([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            var newData = new SchoolsGridViewModel();

            var existingSchool = db.ΣΥΣ_ΣΧΟΛΕΣ.Where(s => s.SCHOOL_TYPE == data.SCHOOL_TYPE && s.SCHOOL_NAME == data.SCHOOL_NAME).Count();
            if (existingSchool > 0)
                ModelState.AddModelError("", "Η σχολή αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolDataService.Create(data);
                newData = schoolDataService.Refresh(data.SCHOOL_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Update([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            var newData = new SchoolsGridViewModel();

            if (data != null & ModelState.IsValid)
            {
                schoolDataService.Update(data);
                newData = schoolDataService.Refresh(data.SCHOOL_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult School_Destroy([DataSourceRequest] DataSourceRequest request, SchoolsGridViewModel data)
        {
            if (data != null)
            {
                schoolDataService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #region DATA FORM

        public ActionResult xSchoolEdit(int schoolId)
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

            SchoolsViewModel schoolData = schoolDataService.GetRecord(schoolId);
            return View(schoolData);
        }

        [HttpPost]
        public ActionResult xSchoolEdit(int schoolId, SchoolsViewModel data)
        {
            if (ModelState.IsValid)
            {
                ΣΥΣ_ΣΧΟΛΕΣ entity = db.ΣΥΣ_ΣΧΟΛΕΣ.Find(schoolId);

                entity.SCHOOL_NAME = data.SCHOOL_NAME.Trim();
                entity.SCHOOL_TYPE = data.SCHOOL_TYPE;
                entity.ΓΡΑΜΜΑΤΕΙΑ = data.ΓΡΑΜΜΑΤΕΙΑ.Trim();
                entity.ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ.Trim();
                entity.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = data.ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                entity.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = data.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ.Trim();
                entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ.Trim();
                entity.ΦΑΞ = data.ΦΑΞ.Trim();
                entity.EMAIL = data.EMAIL.Trim();
                entity.ΚΙΝΗΤΟ = data.ΚΙΝΗΤΟ;

                if (data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ != null) entity.ΥΠΟΔΙΕΥΘΥΝΤΗΣ = data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ.Trim();
                entity.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ = data.ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ;
                entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                // Notify here
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        #endregion

        #endregion ΣΧΟΛΕΣ


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΣΧΟΛΩΝ ΑΝΑ ΣΧΟΛΙΚΟ ΕΤΟΣ

        public ActionResult EidikotitesYearsSchools()
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

            PopulateSchools();
            PopulateEidikotites();
            return View();
        }

        public ActionResult SchoolEidikotita_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            var data = eidikotitesSchoolService.Read(schoolyearId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Create([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data, int schoolyearId = 0)
        {
            var newData = new EidikotitaYearSchoolViewModel();

            if (schoolyearId > 0)
            {
                var existingRecord = db.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Where(s => s.SCHOOLYEAR_ID == schoolyearId && s.SCHOOL_ID == data.SCHOOL_ID && s.EIDIKOTITA_ID == data.EIDIKOTITA_ID).Count();
                if (existingRecord > 0)
                    ModelState.AddModelError("", "Η ειδικότητα είναι ήδη καταχωρημένη για αυτό το σχολείο και έτος.");

                if (data != null && ModelState.IsValid)
                {
                    eidikotitesSchoolService.Create(data, schoolyearId);
                    newData = eidikotitesSchoolService.Refresh(data.SYE_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε σχολικό έτος. Η καταχώρηση ακυρώθηκε.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Update([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data, int schoolyearId = 0)
        {
            var newData = new EidikotitaYearSchoolViewModel();

            if (schoolyearId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    eidikotitesSchoolService.Update(data, schoolyearId);
                    newData = eidikotitesSchoolService.Refresh(data.SYE_ID);
                }
            }
            else
            {
                ModelState.AddModelError("", "Πρέπει πρώτα να επιλέξετε σχολικό έτος. Η καταχώρηση ακυρώθηκε.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Destroy([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteSchoolEidikotita(data.EIDIKOTITA_ID))
                {
                    eidikotitesSchoolService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή διότι υπάρχουν μαθητές με αυτή την ειδικότητα.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΗ ΕΠΙΔΟΜΑΤΩΝ

        public ActionResult SysEpidomaTypes()
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
            return View();
        }

        public ActionResult Epidoma_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = epidomaTypeService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epidoma_Create([DataSourceRequest] DataSourceRequest request, SysEpidomaTypeViewModel data)
        {
            var newData = new SysEpidomaTypeViewModel();

            var existingEpidoma = db.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Where(s => s.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ == data.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ).Count();
            if (existingEpidoma > 0)
                ModelState.AddModelError("", "Το επίδομα αυτό υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                epidomaTypeService.Create(data);
                newData = epidomaTypeService.Refresh(data.ΕΠΙΔΟΜΑ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epidoma_Update([DataSourceRequest] DataSourceRequest request, SysEpidomaTypeViewModel data)
        {
            var newData = new SysEpidomaTypeViewModel();

            if (data != null & ModelState.IsValid)
            {
                epidomaTypeService.Update(data);
                newData = epidomaTypeService.Refresh(data.ΕΠΙΔΟΜΑ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Epidoma_Destroy([DataSourceRequest] DataSourceRequest request, SysEpidomaTypeViewModel data)
        {
            if (data != null)
            {
                epidomaTypeService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΟΣΑ ΕΠΙΔΟΜΑΤΩΝ

        public ActionResult SysEpidomaPosa()
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
            PopulateShoolYears();
            return View();
        }

        public ActionResult EpidomaPoso_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = epidomaPosoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaPoso_Create([DataSourceRequest] DataSourceRequest request, EpidomaPosoViewModel data)
        {
            var newData = new EpidomaPosoViewModel();

            var existingEpidoma = db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingEpidoma > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη δεδομένα γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                epidomaPosoService.Create(data);
                newData = epidomaPosoService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaPoso_Update([DataSourceRequest] DataSourceRequest request, EpidomaPosoViewModel data)
        {
            var newData = new EpidomaPosoViewModel();

            if (data != null & ModelState.IsValid)
            {
                epidomaPosoService.Update(data);
                newData = epidomaPosoService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaPoso_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaPosoViewModel data)
        {
            if (data != null)
            {
                epidomaPosoService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΟΙΝΩΝΙΚΕΣ ΟΜΑΔΕΣ

        public ActionResult SysSocialGroups()
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
            return View();
        }

        public ActionResult SocialGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = socialGroupService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SocialGroup_Create([DataSourceRequest] DataSourceRequest request, SocialGroupViewModel data)
        {
            var newData = new SocialGroupViewModel();

            var existingData = db.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ.Where(s => s.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ == data.ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ).Count();
            if (existingData > 0)
                ModelState.AddModelError("", "Αυτό το κοινωνικό κριτήριο υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                socialGroupService.Create(data);
                newData = socialGroupService.Refresh(data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SocialGroup_Update([DataSourceRequest] DataSourceRequest request, SocialGroupViewModel data)
        {
            var newData = new SocialGroupViewModel();

            if (data != null & ModelState.IsValid)
            {
                socialGroupService.Update(data);
                newData = socialGroupService.Refresh(data.ΚΟΙΝΩΝΙΚΟ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SocialGroup_Destroy([DataSourceRequest] DataSourceRequest request, SocialGroupViewModel data)
        {
            if (data != null)
            {
                socialGroupService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΙΤΙΕΣ ΑΠΟΡΡΙΨΗΣ ΑΙΤΗΣΕΩΝ

        public ActionResult SysAporipsiAities()
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
            return View();
        }

        public ActionResult Aporipsi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = aporipsiAitiaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aporipsi_Create([DataSourceRequest] DataSourceRequest request, AporipsiAitiaViewModel data)
        {
            var newData = new AporipsiAitiaViewModel();

            var existingdata = db.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Where(s => s.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ == data.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Η αιτία αυτή υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                aporipsiAitiaService.Create(data);
                newData = aporipsiAitiaService.Refresh(data.ΑΠΟΡΡΙΨΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aporipsi_Update([DataSourceRequest] DataSourceRequest request, AporipsiAitiaViewModel data)
        {
            var newData = new AporipsiAitiaViewModel();

            if (data != null & ModelState.IsValid)
            {
                aporipsiAitiaService.Update(data);
                newData = aporipsiAitiaService.Refresh(data.ΑΠΟΡΡΙΨΗ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aporipsi_Destroy([DataSourceRequest] DataSourceRequest request, AporipsiAitiaViewModel data)
        {
            if (data != null)
            {
                aporipsiAitiaService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΑΡΑΜΕΤΡΟΙ ΑΠΟΦΑΣΕΩΝ (ΑΠΟΦΑΣΕΙΣ ΔΣ, ΥΠΟΥΡΓΙΚΕΣ, ΕΓΚΥΚΛΙΟΙ)

        public ActionResult SysApofasiParameters()
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
            PopulateSchoolTypes();
            return View();
        }

        public ActionResult Parameters_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = parametersService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Parameters_Create([DataSourceRequest] DataSourceRequest request, SysApofasiParametersViewModel data)
        {
            var newData = new SysApofasiParametersViewModel();

            if (data != null && ModelState.IsValid)
            {
                parametersService.Create(data);
                newData = parametersService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Parameters_Update([DataSourceRequest] DataSourceRequest request, SysApofasiParametersViewModel data)
        {
            var newData = new SysApofasiParametersViewModel();

            if (data != null & ModelState.IsValid)
            {
                parametersService.Update(data);
                newData = parametersService.Refresh(data.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Parameters_Destroy([DataSourceRequest] DataSourceRequest request, SysApofasiParametersViewModel data)
        {
            if (data != null)
            {
                parametersService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΔΙΟΙΚΗΣΗ - ΥΠΟΓΡΑΦΟΝΤΕΣ ΑΠΟΦΑΣΕΙΣ

        public ActionResult Administrators()
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
            PopulateShoolYears();
            PopulateGenders();
            return View();
        }

        #region ΔΙΑΧΕΙΡΙΣΤΕΣ

        public ActionResult Diaxiristis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = diaxriristesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Create([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            var newData = new DiaxiristisViewModel();

            var existingdata = db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ.Where(s => s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ == data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Ο διαχειριστής αυτός υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                diaxriristesService.Create(data);
                newData = diaxriristesService.Refresh(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Update([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            var newData = new DiaxiristisViewModel();

            if (data != null & ModelState.IsValid)
            {
                diaxriristesService.Update(data);
                newData = diaxriristesService.Refresh(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Diaxiristis_Destroy([DataSourceRequest] DataSourceRequest request, DiaxiristisViewModel data)
        {
            if (data != null)
            {
                diaxriristesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΔΙΑΧΕΙΡΙΣΤΕΣ

        #region ΠΡΟΪΣΤΑΜΕΝΟΙ

        public ActionResult Proistamenos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = proistamenoiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Create([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            var newData = new ProistamenosViewModel();

            var existingdata = db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                proistamenoiService.Create(data);
                newData = proistamenoiService.Refresh(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Update([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            var newData = new ProistamenosViewModel();

            if (data != null & ModelState.IsValid)
            {
                proistamenoiService.Update(data);
                newData = proistamenoiService.Refresh(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Proistamenos_Destroy([DataSourceRequest] DataSourceRequest request, ProistamenosViewModel data)
        {
            if (data != null)
            {
                proistamenoiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΠΡΟΪΣΤΑΜΕΝΟΙ

        #region ΔΙΕΥΘΥΝΤΕΣ

        public ActionResult Director_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = directorsService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Create([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            var newData = new DirectorViewModel();

            var existingdata = db.Δ_ΔΙΕΥΘΥΝΤΕΣ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                directorsService.Create(data);
                newData = directorsService.Refresh(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Update([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            var newData = new DirectorViewModel();

            if (data != null & ModelState.IsValid)
            {
                directorsService.Update(data);
                newData = directorsService.Refresh(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Destroy([DataSourceRequest] DataSourceRequest request, DirectorViewModel data)
        {
            if (data != null)
            {
                directorsService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΔΙΕΥΘΥΝΤΕΣ

        #region ΓΕΝΙΚΟΙ ΔΙΕΥΘΥΝΤΕΣ

        public ActionResult Genikos_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = genikoiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Create([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            var newData = new DirectorGeneralViewModel();

            var existingdata = db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                genikoiService.Create(data);
                newData = genikoiService.Refresh(data.ΓΕΝΙΚΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Update([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            var newData = new DirectorGeneralViewModel();

            if (data != null & ModelState.IsValid)
            {
                genikoiService.Update(data);
                newData = genikoiService.Refresh(data.ΓΕΝΙΚΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genikos_Destroy([DataSourceRequest] DataSourceRequest request, DirectorGeneralViewModel data)
        {
            if (data != null)
            {
                genikoiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΓΕΝΙΚΟΙ ΔΙΕΥΘΥΝΤΕΣ

        #region ΑΝΤΙΠΡΟΕΔΡΟΙ

        public ActionResult Antiproedros_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = antiproedroiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Create([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            var newData = new AntiproedrosViewModel();

            var existingdata = db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                antiproedroiService.Create(data);
                newData = antiproedroiService.Refresh(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Update([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            var newData = new AntiproedrosViewModel();

            if (data != null & ModelState.IsValid)
            {
                antiproedroiService.Update(data);
                newData = antiproedroiService.Refresh(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Antiproedros_Destroy([DataSourceRequest] DataSourceRequest request, AntiproedrosViewModel data)
        {
            if (data != null)
            {
                antiproedroiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΑΝΤΙΠΡΟΕΔΡΟΙ

        #region ΔΙΟΙΚΗΤΕΣ

        public ActionResult Dioikitis_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = dioikitesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Create([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            var newData = new DioikitisViewModel();

            var existingdata = db.Δ_ΔΙΟΙΚΗΤΕΣ.Where(s => s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ).Count();
            if (existingdata > 0)
                ModelState.AddModelError("", "Υπάρχουν ήδη στοιχεία γι' αυτό το σχολικό έτος. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                dioikitesService.Create(data);
                newData = dioikitesService.Refresh(data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Update([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            var newData = new DioikitisViewModel();

            if (data != null & ModelState.IsValid)
            {
                dioikitesService.Update(data);
                newData = dioikitesService.Refresh(data.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dioikitis_Destroy([DataSourceRequest] DataSourceRequest request, DioikitisViewModel data)
        {
            if (data != null)
            {
                dioikitesService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion ΔΙΟΙΚΗΤΕΣ

        #endregion


        #region ΕΙΣΟΔΟΙ ΣΧΟΛΕΙΩΝ

        public ActionResult SchoolLogins()
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
            return View();
        }

        public ActionResult Logins_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetSchoolLoginsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SchoolLoginsViewModel> GetSchoolLoginsFromDB()
        {
            var data = (from d in db.sqlSCHOOL_LOGINS
                        orderby d.SCHOOL_NAME
                        orderby d.LOGIN_DATETIME descending, d.SCHOOL_NAME
                        select new SchoolLoginsViewModel
                        {
                            LOGIN_ID = d.LOGIN_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            LOGIN_DATETIME = d.LOGIN_DATETIME
                        }).ToList();

            return data;
        }

        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
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
            return View();
        }

        public ActionResult Periferies_Read([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = (from d in db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑ
                              orderby d.PERIFERIA_NAME
                              select new SysPeriferiaViewModel
                                  {
                                      PERIFERIA_ID = d.PERIFERIA_ID,
                                      PERIFERIA_NAME = d.PERIFERIA_NAME
                                  }).ToList();

            return Json(periferies.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriferiakiEnotita_Read([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var periferiaEnotites = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ.Where(p => p.PERIFERIA_ID == periferiaId).Select(p => new SysPeriferiakiEnotitaViewModel
            {
                PERIFERIA_ENOTITA_ID = p.PERIFERIA_ENOTITA_ID,
                PERIFERIA_ENOTITA_NAME = p.PERIFERIA_ENOTITA_NAME,
                PERIFERIA_ID = p.PERIFERIA_ID
            }).OrderBy(o => o.PERIFERIA_ENOTITA_NAME);
            return Json(periferiaEnotites.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dimos_Read([DataSourceRequest] DataSourceRequest request, int periferiaEnotitaId)
        {
            var dimoi = db.ΣΥΣ_ΔΗΜΟΣ.Where(o => o.DIMOS_PERIFERIA == periferiaEnotitaId).Select(p => new SysDimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            }).OrderBy(s => s.DIMOS);
            return Json(dimoi.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriferiesPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                return View();
            }
        }

        #endregion


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult xGoogleMaps()
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
            return View();
        }

        public ActionResult GoogleMapsTest()
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
            return View();
        }

        #endregion
    }
}