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
    public class SettingsController : DataControllers.ControllerUnit
    {
        private USER_SCHOOLS loggedSchool;
        private readonly PrimusDBEntities db;

        private readonly IEidikotitesSchoolService eidikotitesSchoolService;
        private readonly ISchoolYearService schoolYearService;
        private readonly IEidikotitesService eidikotitesService;
        private readonly IEpidomaTypeService epidomaTypeService;
        private readonly IEpidomaPosoService epidomaPosoService;
        private readonly ISocialGroupService socialGroupService;
        private readonly IAporipsiAitiaService aporipsiAitiaService;

        public SettingsController(PrimusDBEntities entities, IEidikotitesSchoolService eidikotitesSchoolService,
            ISchoolYearService schoolYearService, IEidikotitesService eidikotitesService, IEpidomaTypeService epidomaTypeService,
            IEpidomaPosoService epidomaPosoService, ISocialGroupService socialGroupService, IAporipsiAitiaService aporipsiAitiaService) : base(entities)
        {
            db = entities;

            this.eidikotitesSchoolService = eidikotitesSchoolService;
            this.schoolYearService = schoolYearService;
            this.eidikotitesService = eidikotitesService;
            this.epidomaTypeService = epidomaTypeService;
            this.epidomaPosoService = epidomaPosoService;
            this.socialGroupService = socialGroupService;
            this.aporipsiAitiaService = aporipsiAitiaService;
        }


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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
            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΙΔΙΚΟΤΗΤΕΣ ΣΧΟΛΩΝ ΑΝΑ ΣΧΟΛΙΚΟ ΕΤΟΣ

        public ActionResult EidikotitesYearsSchools()
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

            PopulateSchools();
            PopulateEidikotites(schoolId);
            return View();
        }

        public ActionResult SchoolEidikotita_Read([DataSourceRequest] DataSourceRequest request, int schoolyearId = 0)
        {
            loggedSchool = GetLoginSchool();
            int schoolId = (int)loggedSchool.USER_SCHOOLID;

            var data = eidikotitesSchoolService.Read(schoolyearId, schoolId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Create([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newData = new EidikotitaYearSchoolViewModel();

            if (schoolyearId > 0)
            {
                var existingRecord = db.ΕΙΔΙΚΟΤΗΤΕΣ_ΕΤΟΣ_ΣΧΟΛΗ.Where(s => s.SCHOOLYEAR_ID == schoolyearId && s.SCHOOL_ID == schoolId && s.EIDIKOTITA_ID == data.EIDIKOTITA_ID).Count();
                if (existingRecord > 0)
                    ModelState.AddModelError("", "Η ειδικότητα είναι ήδη καταχωρημένη για αυτό το σχολείο και έτος.");

                if (data != null && ModelState.IsValid)
                {
                    eidikotitesSchoolService.Create(data, schoolyearId, schoolId);
                    newData = eidikotitesSchoolService.Refresh(data.SYE_ID);
                }
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Update([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data, int schoolyearId = 0)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var newData = new EidikotitaYearSchoolViewModel();

            if (schoolyearId > 0)
            {
                if (data != null & ModelState.IsValid)
                {
                    eidikotitesSchoolService.Update(data, schoolyearId, schoolId);
                    newData = eidikotitesSchoolService.Refresh(data.SYE_ID);
                }
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolEidikotita_Destroy([DataSourceRequest] DataSourceRequest request, EidikotitaYearSchoolViewModel data)
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            if (data != null)
            {
                if (Kerberos.CanDeleteSchoolEidikotita(data.EIDIKOTITA_ID, schoolId))
                {
                    eidikotitesSchoolService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η ειδικότητα δεν μπορεί να διαγραφεί διότι υπάρχουν μαθητές με αυτή.");
                }
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
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            PopulateSchoolTypes();
            return View();
        }

        public ActionResult Eidikotita_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = eidikotitesService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EidikotitesPrint()
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
            return View();
        }

        #endregion


        #region ΕΙΔΗ ΕΠΙΔΟΜΑΤΩΝ

        public ActionResult SysEpidomaTypes()
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
            return View();
        }

        public ActionResult Epidoma_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = epidomaTypeService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΟΣΑ ΕΠΙΔΟΜΑΤΩΝ

        public ActionResult SysEpidomaPosa()
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
            PopulateSchoolYears();
            return View();
        }

        public ActionResult EpidomaPoso_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = epidomaPosoService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΚΟΙΝΩΝΙΚΕΣ ΟΜΑΔΕΣ

        public ActionResult SysSocialGroups()
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
            return View();
        }

        public ActionResult SocialGroup_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = socialGroupService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΙΤΙΕΣ ΑΠΟΡΡΙΨΗΣ ΑΙΤΗΣΕΩΝ

        public ActionResult SysAporipsiAities()
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
            return View();
        }

        public ActionResult Aporipsi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = aporipsiAitiaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΑΡΑΜΕΤΡΟΙ ΑΠΟΦΑΣΕΩΝ

        public ActionResult ApofasiParameters2()
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
            return View();
        }

        public ActionResult Parameters_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = vmApofasiParametersFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Parameters_Update([DataSourceRequest] DataSourceRequest request, ApofasiParameters2ViewModel data)
        {
            var newData = new ApofasiParameters2ViewModel();

            if (data != null & ModelState.IsValid)
            {
                APOFASI_PARAMETERS2 entity = db.APOFASI_PARAMETERS2.Find(data.ΚΩΔΙΚΟΣ);
                entity.ΚΩΔΙΚΟΣ = data.ΚΩΔΙΚΟΣ;
                entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
                entity.ΕΓΚΥΚΛΙΟΙ_Α2 = data.ΕΓΚΥΚΛΙΟΙ_Α2;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                newData = RefreshApofasiParametersFromDB(entity.ΚΩΔΙΚΟΣ);
            }
            return Json(new[] { newData }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ApofasiParameters2ViewModel RefreshApofasiParametersFromDB(int recordId)
        {
            var data = (from d in db.APOFASI_PARAMETERS2
                        where d.ΚΩΔΙΚΟΣ == recordId
                        select new ApofasiParameters2ViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΕΓΚΥΚΛΙΟΙ_Α2 = d.ΕΓΚΥΚΛΙΟΙ_Α2
                        }).FirstOrDefault();
            return data;
        }

        public List<ApofasiParameters2ViewModel> vmApofasiParametersFromDB()
        {
            var data = (from d in db.APOFASI_PARAMETERS2
                        select new ApofasiParameters2ViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΕΓΚΥΚΛΙΟΙ_Α2 = d.ΕΓΚΥΚΛΙΟΙ_Α2
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
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }

            return View();
        }

        public ActionResult Periferies_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = (from d in db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑ
                        orderby d.PERIFERIA_NAME
                        select new SysPeriferiaViewModel
                        {
                            PERIFERIA_ID = d.PERIFERIA_ID,
                            PERIFERIA_NAME = d.PERIFERIA_NAME
                        }).ToList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PeriferiakiEnotita_Read([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var periferiaEnotites = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ
                .Where(p => p.PERIFERIA_ID == periferiaId)
                .Select(p => new SysPeriferiakiEnotitaViewModel
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
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_SCHOOL");
            }
            else
            {
                loggedSchool = GetLoginSchool();
            }
            return View();
        }

        #endregion


        #region ΧΑΡΤΕΣ GOOGLE

        public ActionResult GoogleMaps()
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