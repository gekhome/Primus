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
    public class ApofaseisController : ControllerUnit
    {
        private USER_ADMINS loggedAdmin;
        private readonly PrimusDBEntities db;

        private readonly IApofasiXorigisiService apofasiXorigisiService;
        private readonly IEpidomaXorigisiService epidomaXorigisiService;
        private readonly IApofasiSynexiaService apofasiSynexiaService;
        private readonly IEpidomaSynexiaService epidomaSynexiaService;
        private readonly IApofasiStegasiService apofasiStegasiService;
        private readonly IEpidomaStegasiService epidomaStegasiService;
        private readonly IApofasiSitisiService apofasiSitisiService;
        private readonly IEpidomaSitisiService epidomaSitisiService;

        private readonly IApofaseisRegistryService apofaseisRegistryService;
        private readonly IEpidotisiRegistryService epidotisiRegistryService;

        public ApofaseisController(PrimusDBEntities entities, 
            IApofasiXorigisiService apofasiXorigisiService, IEpidomaXorigisiService epidomaXorigisiService, 
            IApofasiSynexiaService apofasiSynexiaService, IEpidomaSynexiaService epidomaSynexiaService, 
            IApofasiStegasiService apofasiStegasiService, IEpidomaStegasiService epidomaStegasiService, 
            IApofasiSitisiService apofasiSitisiService, IEpidomaSitisiService epidomaSitisiService, 
            IApofaseisRegistryService apofaseisRegistryService, IEpidotisiRegistryService epidotisiRegistryService) : base(entities)
        {
            db = entities;

            this.apofasiXorigisiService = apofasiXorigisiService;
            this.epidomaXorigisiService = epidomaXorigisiService;
            this.apofasiSynexiaService = apofasiSynexiaService;
            this.epidomaSynexiaService = epidomaSynexiaService;
            this.apofasiStegasiService = apofasiStegasiService;
            this.epidomaStegasiService = epidomaStegasiService;
            this.apofasiSitisiService = apofasiSitisiService;
            this.epidomaSitisiService = epidomaSitisiService;

            this.apofaseisRegistryService = apofaseisRegistryService;
            this.epidotisiRegistryService = epidotisiRegistryService;
        }


        #region ΑΠΟΦΑΣΕΙΣ ΧΟΡΗΓΗΣΗ

        public ActionResult ApofaseisXorigisi(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes();
            PopulateSchools();
            // Populators for child grid
            PopulateStudents();
            PopulateEidikotites();
            PopulateTakseis();
            PopulateEpidomata();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiXorigisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = apofasiXorigisiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiXorigisi_Create([DataSourceRequest] DataSourceRequest request, ApofasiXorigisiGridViewModel data)
        {
            ApofasiXorigisiGridViewModel newdata = new ApofasiXorigisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiXorigisiService.Create(data);
                newdata = apofasiXorigisiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiXorigisi_Update([DataSourceRequest] DataSourceRequest request, ApofasiXorigisiGridViewModel data)
        {
            ApofasiXorigisiGridViewModel newdata = new ApofasiXorigisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiXorigisiService.Update(data);
                newdata = apofasiXorigisiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiXorigisi_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiXorigisiGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiXorigisi(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiXorigisiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει επισυναπτόμενες επιδοτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΕΠΙΣΥΝΑΨΗ ΑΙΤΗΣΕΩΝ

        public bool ApofasiXorigisiAttach(EpidomaParameters ep)
        {
            ep.synexeia = false;

            var source = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΠΙΣΥΝΑΨΗ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId && d.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΗ_ΧΟΡΗΓΗΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                return false;
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη αίτηση προς αποφυγή διπλοεγγραφών.
                var epidotisi = (from e in db.ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ where e.AITISI_ID == d.ΑΙΤΗΣΗ_ΚΩΔ select e).Count();
                if (epidotisi == 0)
                {
                    ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ target = new ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ()
                    {
                        ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId,
                        ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΧΟΡΗΓΗΣΗ",
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
                    db.ΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ.Add(target);
                    db.SaveChanges();

                    SetAitisiApofasiCode(d, ep.apofasiId);
                }
            }
            return true;
        }

        #endregion

        #region EPIDOMA GRID CRUD FUNCTIONS

        public ActionResult EpidomaXorigisi_Read([DataSourceRequest] DataSourceRequest request, EpidomaParameters ep)
        {
            IEnumerable<EpidomaXorigisiViewModel> data = epidomaXorigisiService.Read(ep);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaXorigisi_Create([DataSourceRequest] DataSourceRequest request, EpidomaXorigisiViewModel data, EpidomaParameters ep)
        {
            EpidomaXorigisiViewModel newdata = new EpidomaXorigisiViewModel();

            // Για να δημιουργηθεί επιδότηση πρέπει να υπάρχει αντίστοιχη αίτηση
            ep.synexeia = false;    // this is not set in jquery
            AitisiViewModel relatedAitisi = Common.GetRelatedAitisi((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ, ep);
            if (relatedAitisi == null)
            {
                ModelState.AddModelError("", "Δεν μπορεί να καταχωρηθεί νέα επιδότηση χωρίς αντίστοιχη αίτηση.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            if (ep.apofasiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epidomaXorigisiService.Create(data, ep, relatedAitisi);
                    newdata = epidomaXorigisiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

                    return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.AddModelError("", "Δεν έχει γίνει επιλογή κάποιας απόφασης. Η δημιουργία επιδότησης ακυρώθηκε.");
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaXorigisi_Update([DataSourceRequest] DataSourceRequest request, EpidomaXorigisiViewModel data, EpidomaParameters ep)
        {
            EpidomaXorigisiViewModel newdata = new EpidomaXorigisiViewModel();

            if (data != null && ModelState.IsValid)
            {
                epidomaXorigisiService.Update(data, ep);
                newdata = epidomaXorigisiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaXorigisi_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaXorigisiViewModel data)
        {
            if (data != null)
            {
                epidomaXorigisiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion EPIDOMA GRID CRUD FUNCTIONS

        #region ΑΠΟΦΑΣΗ ΧΟΡΗΓΗΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiXorigisiEdit(int apofasiId)
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

            ApofasiXorigisiViewModel data = apofasiXorigisiService.GetRecord(apofasiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            // Set default field values
            data = SetDefaultXorigisiFields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiXorigisiEdit(int apofasiId, ApofasiXorigisiViewModel data)
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

            string ErrorMsg = ValidateApofasiXorigisiFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiXorigisiService.UpdateRecord(data, apofasiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiXorigisiViewModel newApofasi = apofasiXorigisiService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiXorigisiFields(ApofasiXorigisiViewModel apofasi)
        {
            string errMsg = "";
            bool predicate = apofasi.ΠΡΟΙΣΤΑΜΕΝΟΣ == null || apofasi.ΔΙΕΥΘΥΝΤΗΣ == null || apofasi.ΓΕΝΙΚΟΣ == null || apofasi.ΔΙΟΙΚΗΤΗΣ == null || apofasi.ΑΝΤΙΠΡΟΕΔΡΟΣ == null;

            if (predicate) errMsg += "-> Βρέθηκε τουλάχιστον μία κενή τιμή για τους υπογράφοντες.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && string.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return errMsg;
        }

        public ApofasiXorigisiViewModel SetDefaultXorigisiFields(ApofasiXorigisiViewModel data, int apofasiId)
        {
            var totals = (from d in db.sumΕΠΙΔΟΜΑ_ΧΟΡΗΓΗΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).FirstOrDefault();
            if (totals != null)
            {
                data.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = totals.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ;
                data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = totals.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
                data.ΠΛΗΘΟΣ = (short)totals.ΠΛΗΘΟΣ;
                data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = Common.StudentNumberToText((int)totals.ΠΛΗΘΟΣ);
            }
            var data2 = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data2.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
                data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data2.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
                data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;
                data.ΚΑΕ = data2.ΚΑΕ;
            }
            int school_type = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            var data3 = (from d in db.APOFASI_PARAMETERS where d.ΣΧΟΛΗ_ΕΙΔΟΣ == school_type select d).FirstOrDefault();
            if (data3 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data3.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΑΠΟΦΑΣΗ_ΔΣ = data3.ΑΠΟΦΑΣΗ_ΔΣ;
                data.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data3.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
                data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data3.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            }

            var proistamenos = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (proistamenos != null) data.ΠΡΟΙΣΤΑΜΕΝΟΣ = proistamenos.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
            var director = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (director != null) data.ΔΙΕΥΘΥΝΤΗΣ = director.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
            var genikos = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (genikos != null) data.ΓΕΝΙΚΟΣ = genikos.ΓΕΝΙΚΟΣ_ΚΩΔ;
            var dioikitis = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (dioikitis != null) data.ΔΙΟΙΚΗΤΗΣ = dioikitis.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
            var aproedros = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (aproedros != null) data.ΑΝΤΙΠΡΟΕΔΡΟΣ = aproedros.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;

            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΧΟΡΗΓΗΣΗ";

            return (data);
        }


        #endregion

        #region ΕΠΙΔΟΤΗΣΗ ΧΟΡΗΓΗΣΗ ΚΑΡΤΕΛΑ

        public ActionResult EpidomaXorigisiEdit(int epidotisiId)
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

            EpidomaXorigisiViewModel data = epidomaXorigisiService.GetRecord(epidotisiId);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult EpidomaXorigisiEdit(int epidotisiId, EpidomaXorigisiViewModel data)
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

            if (ModelState.IsValid)
            {
                epidomaXorigisiService.UpdateRecord(data, epidotisiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                EpidomaXorigisiViewModel newData = epidomaXorigisiService.GetRecord(epidotisiId);
                return View(newData);
            }
            return View(data);
        }

        #endregion 

        #region ΕΚΤΥΠΩΣΗ ΑΠΟΦΑΣΗΣ

        public ActionResult ApofasiXorigisiPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                var data = (from d in db.ΑΠΟΦΑΣΗ_ΧΟΡΗΓΗΣΗ
                            where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                            select new ApofasiXorigisiViewModel
                                {
                                    ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                                    ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                                    ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                                }).FirstOrDefault();

                return View(data);
            }
        }

        #endregion

        #endregion ΑΠΟΦΑΣΕΙΣ ΧΟΡΗΓΗΣΗ


        #region ΑΠΟΦΑΣΕΙΣ ΣΥΝΕΧΕΙΑ

        public ActionResult ApofaseisSynexeia(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes();
            PopulateSchools();
            // Populators for child grid
            PopulateStudents();
            PopulateEidikotites();
            PopulateTakseis();
            PopulateEpidomata();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiSynexeia_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = apofasiSynexiaService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSynexeia_Create([DataSourceRequest] DataSourceRequest request, ApofasiSynexeiaGridViewModel data)
        {
            ApofasiSynexeiaGridViewModel newdata = new ApofasiSynexeiaGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSynexiaService.Create(data);
                newdata = apofasiSynexiaService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSynexeia_Update([DataSourceRequest] DataSourceRequest request, ApofasiSynexeiaGridViewModel data)
        {
            ApofasiSynexeiaGridViewModel newdata = new ApofasiSynexeiaGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSynexiaService.Update(data);
                newdata = apofasiSynexiaService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSynexeia_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSynexeiaGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSynexeia(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSynexiaService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει επισυναπτόμενες επιδοτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΕΠΙΣΥΝΑΨΗ ΑΙΤΗΣΕΩΝ

        public bool ApofasiSynexeiaAttach(EpidomaParameters ep)
        {
            ep.synexeia = true;

            var source = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΠΙΣΥΝΑΨΗ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId && d.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                return false;
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη αίτηση προς αποφυγή διπλοεγγραφών.
                var epidotisi = (from e in db.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ where e.AITISI_ID == d.ΑΙΤΗΣΗ_ΚΩΔ select e).Count();
                if (epidotisi == 0)
                {
                    ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ target = new ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ()
                    {
                        ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId,
                        ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ",
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
                    db.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Add(target);
                    db.SaveChanges();

                    SetAitisiApofasiCode(d, ep.apofasiId);
                }
            }
            return true;
        }

        #endregion

        #region EPIDOMA GRID CRUD FUNCTIONS

        public ActionResult EpidomaSynexeia_Read([DataSourceRequest] DataSourceRequest request, EpidomaParameters ep)
        {
            IEnumerable<EpidomaSynexeiaViewModel> data = epidomaSynexiaService.Read(ep);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSynexeia_Create([DataSourceRequest] DataSourceRequest request, EpidomaSynexeiaViewModel data, EpidomaParameters ep)
        {
            EpidomaSynexeiaViewModel newdata = new EpidomaSynexeiaViewModel();

            // Για να δημιουργηθεί επιδότηση πρέπει να υπάρχει αντίστοιχη αίτηση
            ep.synexeia = true;    // this is not set in jquery
            AitisiViewModel relatedAitisi = Common.GetRelatedAitisi((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ, ep);
            if (relatedAitisi == null)
            {
                ModelState.AddModelError("", "Δεν μπορεί να καταχωρηθεί νέα επιδότηση χωρίς αντίστοιχη αίτηση.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            if (ep.apofasiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epidomaSynexiaService.Create(data, ep, relatedAitisi);

                    newdata = epidomaSynexiaService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
                    return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.AddModelError("", "Δεν έχει γίνει επιλογή κάποιας απόφασης. Η δημιουργία επιδότησης ακυρώθηκε.");
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSynexeia_Update([DataSourceRequest] DataSourceRequest request, EpidomaSynexeiaViewModel data, EpidomaParameters ep)
        {
            EpidomaSynexeiaViewModel newdata = new EpidomaSynexeiaViewModel();

            if (data != null && ModelState.IsValid)
            {
                epidomaSynexiaService.Update(data, ep);

                newdata = epidomaSynexiaService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSynexeia_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaSynexeiaViewModel data)
        {
            if (data != null)
            {
                epidomaSynexiaService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion EPIDOMA GRID CRUD FUNCTIONS

        #region ΑΠΟΦΑΣΗ ΣΥΝΕΧΕΙΑ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiSynexeiaEdit(int apofasiId)
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

            ApofasiSynexeiaViewModel data = apofasiSynexiaService.GetRecord(apofasiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            // Set default field values
            data = SetDefaultSynexeiaFields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiSynexeiaEdit(int apofasiId, ApofasiSynexeiaViewModel data)
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

            string ErrorMsg = ValidateApofasiSynexeiaFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiSynexiaService.UpdateRecord(data, apofasiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSynexeiaViewModel newApofasi = apofasiSynexiaService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiSynexeiaFields(ApofasiSynexeiaViewModel apofasi)
        {
            string errMsg = "";
            bool predicate = apofasi.ΠΡΟΙΣΤΑΜΕΝΟΣ == null || apofasi.ΔΙΕΥΘΥΝΤΗΣ == null || apofasi.ΓΕΝΙΚΟΣ == null || apofasi.ΔΙΟΙΚΗΤΗΣ == null || apofasi.ΑΝΤΙΠΡΟΕΔΡΟΣ == null;
            if (predicate) 
                errMsg += "-> Βρέθηκε τουλάχιστον μία κενή τιμή για τους υπογράφοντες.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiSynexeiaViewModel SetDefaultSynexeiaFields(ApofasiSynexeiaViewModel data, int apofasiId)
        {
            var totals = (from d in db.sumΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).FirstOrDefault();
            if (totals != null)
            {
                data.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = totals.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ;
                data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = totals.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
                data.ΠΛΗΘΟΣ = (short)totals.ΠΛΗΘΟΣ;
                data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = Common.StudentNumberToText((int)totals.ΠΛΗΘΟΣ);
            }
            var data2 = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data2.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
                data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data2.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
                data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;
                data.ΚΑΕ = data2.ΚΑΕ;
            }
            int school_type = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            var data3 = (from d in db.APOFASI_PARAMETERS where d.ΣΧΟΛΗ_ΕΙΔΟΣ == school_type select d).FirstOrDefault();
            if (data3 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data3.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΑΠΟΦΑΣΗ_ΔΣ = data3.ΑΠΟΦΑΣΗ_ΔΣ;
                data.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data3.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
                data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data3.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            }

            var proistamenos = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (proistamenos != null) data.ΠΡΟΙΣΤΑΜΕΝΟΣ = proistamenos.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
            var director = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (director != null) data.ΔΙΕΥΘΥΝΤΗΣ = director.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
            var genikos = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (genikos != null) data.ΓΕΝΙΚΟΣ = genikos.ΓΕΝΙΚΟΣ_ΚΩΔ;
            var dioikitis = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (dioikitis != null) data.ΔΙΟΙΚΗΤΗΣ = dioikitis.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
            var aproedros = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (aproedros != null) data.ΑΝΤΙΠΡΟΕΔΡΟΣ = aproedros.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;

            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ";

            return (data);
        }

        #endregion ΑΠΟΦΑΣΗ ΣΥΝΕΧΕΙΑ ΚΑΡΤΕΛΑ

        #region ΕΠΙΔΟΤΗΣΗ ΣΥΝΕΧΕΙΑ ΚΑΡΤΕΛΑ

        public ActionResult EpidomaSynexeiaEdit(int epidotisiId)
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

            EpidomaSynexeiaViewModel data = epidomaSynexiaService.GetRecord(epidotisiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult EpidomaSynexeiaEdit(int epidotisiId, EpidomaSynexeiaViewModel data)
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

            if (ModelState.IsValid)
            {
                epidomaSynexiaService.UpdateRecord(data, epidotisiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                EpidomaSynexeiaViewModel newData = epidomaSynexiaService.GetRecord(epidotisiId);
                return View(newData);
            }
            return View(data);
        }

        #endregion 

        #region ΕΚΤΥΠΩΣΗ ΑΠΟΦΑΣΗΣ

        public ActionResult ApofasiSynexeiaPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                var data = (from d in db.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ
                            where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                            select new ApofasiSynexeiaViewModel
                            {
                                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                            }).FirstOrDefault();

                return View(data);
            }
        }

        #endregion

        #endregion ΑΠΟΦΑΣΕΙΣ ΣΥΝΕΧΕΙΑ


        #region ΑΠΟΦΑΣΕΙΣ ΣΤΕΓΑΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ

        public ActionResult ApofaseisStegasi(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes();
            PopulateSchools();
            // Populators for child grid
            PopulateStudents();
            PopulateEidikotites();
            PopulateTakseis();
            PopulateEpidomata();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiStegasi_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApofasiStegasiGridViewModel> data = apofasiStegasiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiStegasi_Create([DataSourceRequest] DataSourceRequest request, ApofasiStegasiGridViewModel data)
        {
            ApofasiStegasiGridViewModel newdata = new ApofasiStegasiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiStegasiService.Create(data);

                newdata = apofasiStegasiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiStegasi_Update([DataSourceRequest] DataSourceRequest request, ApofasiStegasiGridViewModel data)
        {
            ApofasiStegasiGridViewModel newdata = new ApofasiStegasiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiStegasiService.Update(data);

                newdata = apofasiStegasiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiStegasi_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiStegasiGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiStegasi(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiStegasiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει επισυναπτόμενες επιδοτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΕΠΙΣΥΝΑΨΗ ΑΙΤΗΣΕΩΝ

        public bool ApofasiStegasiAttach(EpidomaParameters ep)
        {
            ep.synexeia = false;

            var source = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΠΙΣΥΝΑΨΗ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId && d.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΗ_ΣΤΕΓΑΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                return false;
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη αίτηση προς αποφυγή διπλοεγγραφών.
                var epidotisi = (from e in db.ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ where e.AITISI_ID == d.ΑΙΤΗΣΗ_ΚΩΔ select e).Count();
                if (epidotisi == 0)
                {
                    ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ target = new ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ()
                    {
                        ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId,
                        ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΤΕΓΑΣΗ",
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
                    db.ΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ.Add(target);
                    db.SaveChanges();

                    SetAitisiApofasiCode(d, ep.apofasiId);
                }
            }
            return true;
        }

        #endregion

        #region EPIDOMA GRID CRUD FUNCTIONS

        public ActionResult EpidomaStegasi_Read([DataSourceRequest] DataSourceRequest request, EpidomaParameters ep)
        {
            IEnumerable<EpidomaStegasiViewModel> data = epidomaStegasiService.Read(ep);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaStegasi_Create([DataSourceRequest] DataSourceRequest request, EpidomaStegasiViewModel data, EpidomaParameters ep)
        {
            EpidomaStegasiViewModel newdata = new EpidomaStegasiViewModel();

            // Για να δημιουργηθεί επιδότηση πρέπει να υπάρχει αντίστοιχη αίτηση
            ep.synexeia = false;    // this is not set in jquery
            AitisiViewModel relatedAitisi = Common.GetRelatedAitisi((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ, ep);
            if (relatedAitisi == null)
            {
                ModelState.AddModelError("", "Δεν μπορεί να καταχωρηθεί νέα επιδότηση χωρίς αντίστοιχη αίτηση.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            if (ep.apofasiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epidomaStegasiService.Create(data, ep, relatedAitisi);

                    newdata = epidomaStegasiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
                    return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.AddModelError("", "Δεν έχει γίνει επιλογή κάποιας απόφασης. Η δημιουργία επιδότησης ακυρώθηκε.");
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaStegasi_Update([DataSourceRequest] DataSourceRequest request, EpidomaStegasiViewModel data, EpidomaParameters ep)
        {
            EpidomaStegasiViewModel newdata = new EpidomaStegasiViewModel();

            if (data != null && ModelState.IsValid)
            {
                epidomaStegasiService.Update(data, ep);

                newdata = epidomaStegasiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaStegasi_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaStegasiViewModel data)
        {
            if (data != null)
            {
                epidomaStegasiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }


        #endregion EPIDOMA GRID CRUD FUNCTIONS

        #region ΑΠΟΦΑΣΗ ΣΤΕΓΑΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiStegasiEdit(int apofasiId)
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

            ApofasiStegasiViewModel data = apofasiStegasiService.GetRecord(apofasiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            // Set default field values
            data = SetDefaultStegasiFields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiStegasiEdit(int apofasiId, ApofasiStegasiViewModel data)
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

            string ErrorMsg = ValidateApofasiStegasiFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiStegasiService.UpdateRecord(data, apofasiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiStegasiViewModel newApofasi = apofasiStegasiService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiStegasiFields(ApofasiStegasiViewModel apofasi)
        {
            string errMsg = "";
            bool predicate = apofasi.ΠΡΟΙΣΤΑΜΕΝΟΣ == null || apofasi.ΔΙΕΥΘΥΝΤΗΣ == null || apofasi.ΓΕΝΙΚΟΣ == null || apofasi.ΔΙΟΙΚΗΤΗΣ == null || apofasi.ΑΝΤΙΠΡΟΕΔΡΟΣ == null;
            if (predicate) 
                errMsg += "-> Βρέθηκε τουλάχιστον μία κενή τιμή για τους υπογράφοντες.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                errMsg += "-> Βρέθηκαν κενές τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiStegasiViewModel SetDefaultStegasiFields(ApofasiStegasiViewModel data, int apofasiId)
        {
            var totals = (from d in db.sumΕΠΙΔΟΜΑ_ΣΤΕΓΑΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).FirstOrDefault();
            if (totals != null)
            {
                data.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = totals.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ;
                data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = totals.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
                data.ΠΛΗΘΟΣ = (short)totals.ΠΛΗΘΟΣ;
                data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = Common.StudentNumberToText((int)totals.ΠΛΗΘΟΣ);
            }
            var data2 = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data2.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
                data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data2.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
                data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;
                data.ΚΑΕ = data2.ΚΑΕ;
            }
            int school_type = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            var data3 = (from d in db.APOFASI_PARAMETERS where d.ΣΧΟΛΗ_ΕΙΔΟΣ == school_type select d).FirstOrDefault();
            if (data3 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data3.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΑΠΟΦΑΣΗ_ΔΣ = data3.ΑΠΟΦΑΣΗ_ΔΣ;
                data.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data3.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
                data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data3.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            }

            var proistamenos = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (proistamenos != null) data.ΠΡΟΙΣΤΑΜΕΝΟΣ = proistamenos.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
            var director = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (director != null) data.ΔΙΕΥΘΥΝΤΗΣ = director.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
            var genikos = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (genikos != null) data.ΓΕΝΙΚΟΣ = genikos.ΓΕΝΙΚΟΣ_ΚΩΔ;
            var dioikitis = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (dioikitis != null) data.ΔΙΟΙΚΗΤΗΣ = dioikitis.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
            var aproedros = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (aproedros != null) data.ΑΝΤΙΠΡΟΕΔΡΟΣ = aproedros.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;

            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΤΕΓΑΣΗ";

            return (data);
        }


        #endregion

        #region ΕΠΙΔΟΤΗΣΗ ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΤΕΓΑΣΗ ΚΑΡΤΕΛΑ

        public ActionResult EpidomaStegasiEdit(int epidotisiId)
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

            EpidomaStegasiViewModel data = epidomaStegasiService.GetRecord(epidotisiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult EpidomaStegasiEdit(int epidotisiId, EpidomaStegasiViewModel data)
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

            if (ModelState.IsValid)
            {
                epidomaStegasiService.UpdateRecord(data, epidotisiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                EpidomaStegasiViewModel newData = epidomaStegasiService.GetRecord(epidotisiId);
                return View(newData);
            }
            return View(data);
        }

        #endregion 

        #region ΕΚΤΥΠΩΣΗ ΑΠΟΦΑΣΗΣ

        public ActionResult ApofasiStegasiPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                var data = (from d in db.ΑΠΟΦΑΣΗ_ΣΤΕΓΑΣΗ
                            where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                            select new ApofasiStegasiViewModel
                            {
                                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                            }).FirstOrDefault();

                return View(data);
            }
        }

        #endregion

        #endregion ΑΠΟΦΑΣΕΙΣ ΣΤΕΓΑΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ


        #region ΑΠΟΦΑΣΕΙΣ ΣΙΤΙΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ

        public ActionResult ApofaseisSitisi(string notify = null)
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

            // Populators for master grid foreign keys
            PopulateSchoolYears();
            PopulateDiaxiristes();
            PopulateSchools();
            // Populators for child grid
            PopulateStudents();
            PopulateEidikotites();
            PopulateTakseis();
            PopulateEpidomata();
            PopulateDocTypes();

            return View();
        }

        #region APOFASI GRID CRUD FUNCTIONS

        public ActionResult ApofasiSitisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApofasiSitisiGridViewModel> data = apofasiSitisiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi_Create([DataSourceRequest] DataSourceRequest request, ApofasiSitisiGridViewModel data)
        {
            ApofasiSitisiGridViewModel newdata = new ApofasiSitisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSitisiService.Create(data);

                newdata = apofasiSitisiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi_Update([DataSourceRequest] DataSourceRequest request, ApofasiSitisiGridViewModel data)
        {
            ApofasiSitisiGridViewModel newdata = new ApofasiSitisiGridViewModel();

            if (data != null && ModelState.IsValid)
            {
                apofasiSitisiService.Update(data);

                newdata = apofasiSitisiService.Refresh(data.ΑΠΟΦΑΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApofasiSitisi_Destroy([DataSourceRequest] DataSourceRequest request, ApofasiSitisiGridViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteApofasiSitisi(data.ΑΠΟΦΑΣΗ_ΚΩΔ))
                {
                    apofasiSitisiService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Δεν μπορεί να γίνει διαγραφή της απόφασης διότι έχει επισυναπτόμενες επιδοτήσεις.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion APOFASI GRID CRUD FUNCTIONS

        #region ΕΠΙΣΥΝΑΨΗ ΑΙΤΗΣΕΩΝ

        public bool ApofasiSitisiAttach(EpidomaParameters ep)
        {
            ep.synexeia = false;

            var source = (from d in db.sqlΑΙΤΗΣΕΙΣ_ΕΠΙΣΥΝΑΨΗ
                          where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId && d.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                          select d).ToList();

            var apofasi = (from d in db.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();
            DateTime sintaksi_date = (DateTime)apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;

            if (source.Count == 0)
            {
                return false;
            }
            foreach (var d in source)
            {
                // Έλεγχος εάν ήδη υπάρχει καταχωρημένη η συγκεκριμένη αίτηση προς αποφυγή διπλοεγγραφών.
                var epidotisi = (from e in db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ where e.AITISI_ID == d.ΑΙΤΗΣΗ_ΚΩΔ select e).Count();
                if (epidotisi == 0)
                {
                    ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ target = new ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ()
                    {
                        ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId,
                        ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΙΤΙΣΗ",
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
                    db.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ.Add(target);
                    db.SaveChanges();

                    SetAitisiApofasiCode(d, ep.apofasiId);
                }
            }
            return true;
        }

        #endregion

        #region EPIDOMA GRID CRUD FUNCTIONS

        public ActionResult EpidomaSitisi_Read([DataSourceRequest] DataSourceRequest request, EpidomaParameters ep)
        {
            IEnumerable<EpidomaSitisiViewModel> data = epidomaSitisiService.Read(ep);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi_Create([DataSourceRequest] DataSourceRequest request, EpidomaSitisiViewModel data, EpidomaParameters ep)
        {
            EpidomaSitisiViewModel newdata = new EpidomaSitisiViewModel();

            // Για να δημιουργηθεί επιδότηση πρέπει να υπάρχει αντίστοιχη αίτηση
            ep.synexeia = false;    // this is not set in jquery
            AitisiViewModel relatedAitisi = Common.GetRelatedAitisi((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ, ep);
            if (relatedAitisi == null)
            {
                ModelState.AddModelError("", "Δεν μπορεί να καταχωρηθεί νέα επιδότηση χωρίς αντίστοιχη αίτηση.");
                return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            if (ep.apofasiId > 0)
            {
                if (data != null && ModelState.IsValid)
                {
                    epidomaSitisiService.Create(data, ep, relatedAitisi);

                    newdata = epidomaSitisiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
                    return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.AddModelError("", "Δεν έχει γίνει επιλογή κάποιας απόφασης. Η δημιουργία επιδότησης ακυρώθηκε.");
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi_Update([DataSourceRequest] DataSourceRequest request, EpidomaSitisiViewModel data, EpidomaParameters ep)
        {
            EpidomaSitisiViewModel newdata = new EpidomaSitisiViewModel();

            if (data != null && ModelState.IsValid)
            {
                epidomaSitisiService.Update(data, ep);

                newdata = epidomaSitisiService.Refresh(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EpidomaSitisi_Destroy([DataSourceRequest] DataSourceRequest request, EpidomaSitisiViewModel data)
        {
            if (data != null)
            {
                epidomaSitisiService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion EPIDOMA GRID CRUD FUNCTIONS

        #region ΑΠΟΦΑΣΗ ΣΙΤΙΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ ΚΑΡΤΕΛΑ

        public ActionResult ApofasiSitisiEdit(int apofasiId)
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

            ApofasiSitisiViewModel data = apofasiSitisiService.GetRecord(apofasiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            // Set default field values
            data = SetDefaultSitisiFields(data, apofasiId);

            return View(data);
        }

        [HttpPost]
        public ActionResult ApofasiSitisiEdit(int apofasiId, ApofasiSitisiViewModel data)
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

            string ErrorMsg = ValidateApofasiSitisiFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                apofasiSitisiService.UpdateRecord(data, apofasiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                ApofasiSitisiViewModel newApofasi = apofasiSitisiService.GetRecord(apofasiId);
                return View(newApofasi);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση ακυρώθηκε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public string ValidateApofasiSitisiFields(ApofasiSitisiViewModel apofasi)
        {
            string errMsg = "";

            bool predicate = apofasi.ΠΡΟΙΣΤΑΜΕΝΟΣ == null || apofasi.ΔΙΕΥΘΥΝΤΗΣ == null || apofasi.ΓΕΝΙΚΟΣ == null || apofasi.ΔΙΟΙΚΗΤΗΣ == null || apofasi.ΑΝΤΙΠΡΟΕΔΡΟΣ == null;
            if (predicate) 
                errMsg += "-> Βρέθηκε τουλάχιστον μία κενή τιμή για τους υπογράφοντες.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && apofasi.ΗΜΕΡΟΜΗΝΙΑ == null) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ == 2 && String.IsNullOrEmpty(apofasi.ΠΡΩΤΟΚΟΛΛΟ)) 
                errMsg += "-> Για διαβιβαστκό πρέπει να καταχωρηθεί ημερομηνία.";

            if (apofasi.ΣΤΟ_ΟΡΘΟ == true && apofasi.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ == null) 
                errMsg += "-> Για ορθή επανάληψη πρέπει να καταχωρηθεί ημερομηνία.";

            if (!Common.ValidSignatures(apofasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)) 
                errMsg += "-> Βρέθηκαν μη καταχωρημένες τιμές για τους υπογράφοντες για αυτό το σχολικό έτος.";

            return (errMsg);
        }

        public ApofasiSitisiViewModel SetDefaultSitisiFields(ApofasiSitisiViewModel data, int apofasiId)
        {
            var totals = (from d in db.sumΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId select d).FirstOrDefault();
            if (totals != null)
            {
                data.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ = totals.ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ;
                data.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ = totals.ΣΙΤΙΣΗ_ΣΥΝΟΛΟ;
                data.ΠΛΗΘΟΣ = (short)totals.ΠΛΗΘΟΣ;
                data.ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ = Common.StudentNumberToText((int)totals.ΠΛΗΘΟΣ);
            }
            var data2 = (from d in db.ΕΠΙΔΟΤΗΣΕΙΣ_ΠΟΣΑ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (data2 != null)
            {
                data.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ = data2.ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ;
                data.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ;
                data.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ = data2.ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ;
                data.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ = data2.ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ;
                data.ΚΑΕ = data2.ΚΑΕ;
            }
            int school_type = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            var data3 = (from d in db.APOFASI_PARAMETERS where d.ΣΧΟΛΗ_ΕΙΔΟΣ == school_type select d).FirstOrDefault();
            if (data3 != null)
            {
                data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data3.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
                data.ΑΠΟΦΑΣΗ_ΔΣ = data3.ΑΠΟΦΑΣΗ_ΔΣ;
                data.ΑΠΟΦΑΣΕΙΣ_ΔΣ = data3.ΑΠΟΦΑΣΕΙΣ_ΔΣ;
                data.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ = data3.ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ;
            }

            var proistamenos = (from d in db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (proistamenos != null) data.ΠΡΟΙΣΤΑΜΕΝΟΣ = proistamenos.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
            var director = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (director != null) data.ΔΙΕΥΘΥΝΤΗΣ = director.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
            var genikos = (from d in db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (genikos != null) data.ΓΕΝΙΚΟΣ = genikos.ΓΕΝΙΚΟΣ_ΚΩΔ;
            var dioikitis = (from d in db.Δ_ΔΙΟΙΚΗΤΕΣ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (dioikitis != null) data.ΔΙΟΙΚΗΤΗΣ = dioikitis.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ;
            var aproedros = (from d in db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == data.ΣΧΟΛΙΚΟ_ΕΤΟΣ select d).FirstOrDefault();
            if (aproedros != null) data.ΑΝΤΙΠΡΟΕΔΡΟΣ = aproedros.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;

            data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΙΤΙΣΗ";

            return (data);
        }


        #endregion

        #region ΕΠΙΔΟΤΗΣΗ ΚΑΤ ΕΞΑΙΡΕΣΗ ΣΙΤΙΣΗ ΚΑΡΤΕΛΑ

        public ActionResult EpidomaSitisiEdit(int epidotisiId)
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

            EpidomaSitisiViewModel data = epidomaSitisiService.GetRecord(epidotisiId);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult EpidomaSitisiEdit(int epidotisiId, EpidomaSitisiViewModel data)
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

            if (ModelState.IsValid)
            {
                epidomaSitisiService.UpdateRecord(data, epidotisiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                EpidomaSitisiViewModel newData = epidomaSitisiService.GetRecord(epidotisiId);
                return View(newData);
            }

            return View(data);
        }

        #endregion 

        #region ΕΚΤΥΠΩΣΗ ΑΠΟΦΑΣΗΣ

        public ActionResult ApofasiSitisiPrint(int apofasiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMIN");
            }
            else
            {
                var data = (from d in db.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ
                            where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                            select new ApofasiSitisiViewModel
                            {
                                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
                            }).FirstOrDefault();

                return View(data);
            }
        }

        #endregion

        #endregion ΑΠΟΦΑΣΕΙΣ ΣΙΤΙΣΗ ΚΑΤ' ΕΞΑΙΡΕΣΗ


        #region ΜΗΤΡΩΟ ΑΠΟΦΑΣΕΩΝ

        public ActionResult ApofaseisRegistry(string notify = null)
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

            IEnumerable<ApofasiRegistryViewModel> data = apofaseisRegistryService.Read();

            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες αποφάσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            ApofasiRegistryViewModel apofasi = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(apofasi);
        }

        public ActionResult ApofaseisRegistry_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ApofasiRegistryViewModel> data = apofaseisRegistryService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetApofasiRegistryRecord(int apofasiId, string apofasiType, string schoolyear, string school)
        {
            ApofasiParameters ap = new ApofasiParameters();
            ap.apofasiId = apofasiId;
            ap.apofasiType = apofasiType;
            ap.schoolyear = schoolyear;
            ap.school = school;

            ApofasiRegistryViewModel data = apofaseisRegistryService.GetRecord(ap);

            return PartialView("ApofaseisRegistryPartial", data);
        }

        public ActionResult EpidomaInfo_Read([DataSourceRequest] DataSourceRequest request, int apofasiId, string apofasiType, string schoolyear, string school)
        {
            ApofasiParameters ap = new ApofasiParameters
            {
                apofasiId = apofasiId,
                apofasiType = apofasiType,
                schoolyear = schoolyear,
                school = school
            };

            List<EpidomaRegistryViewModel> data = apofaseisRegistryService.GetEpidomata(ap);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΧΟΡΗΓΗΣΗΣ

        public ActionResult EpidomaXorigisi(string notify = null)
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

            List<sqlEpidomaXorigisiViewModel> data = epidotisiRegistryService.ReadXorigisi();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaXorigisiViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiXorigisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlEpidomaXorigisiViewModel> data = epidotisiRegistryService.ReadXorigisi();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiXorigisiRecord(int epidomaId)
        {
            var data = epidotisiRegistryService.GetRecordXorigisi(epidomaId);

            return PartialView("EpidomaXorigisiPartial", data);
        }

        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΧΟΡΗΓΗΣΗΣ


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΣΥΝΕΧΕΙΑΣ

        public ActionResult EpidomaSynexeia(string notify = null)
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

            List<sqlEpidomaSynexeiaViewModel> data = epidotisiRegistryService.ReadSynexia();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaSynexeiaViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiSynexeia_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlEpidomaSynexeiaViewModel> data = epidotisiRegistryService.ReadSynexia();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiSynexeiaRecord(int epidomaId)
        {
            var data = epidotisiRegistryService.GetRecordSynexia(epidomaId);

            return PartialView("EpidomaSynexeiaPartial", data);
        }

        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΣΥΝΕΧΕΙΑΣ


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΣΤΕΓΑΣΗΣ ΚΑΤ' ΕΞΑΙΡΕΣΗ

        public ActionResult EpidomaStegasi(string notify = null)
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

            List<sqlEpidomaStegasiViewModel> data = epidotisiRegistryService.ReadStegasi();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaStegasiViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiStegasi_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlEpidomaStegasiViewModel> data = epidotisiRegistryService.ReadStegasi();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiStegasiRecord(int epidomaId)
        {
            var data = epidotisiRegistryService.GetRecordStegasi(epidomaId);

            return PartialView("EpidomaStegasiPartial", data);
        }

        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΣΤΕΓΑΣΗΣ ΚΑΤ' ΕΞΑΙΡΕΣΗ


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΚΑΤ' ΕΞΑΙΡΕΣΗ

        public ActionResult EpidomaSitisi(string notify = null)
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

            List<sqlEpidomaSitisiViewModel> data = epidotisiRegistryService.ReadSitisi();
            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaSitisiViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiSitisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlEpidomaSitisiViewModel> data = epidotisiRegistryService.ReadSitisi();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiSitisiRecord(int epidomaId)
        {
            var data = epidotisiRegistryService.GetRecordSitisi(epidomaId);

            return PartialView("EpidomaSitisiPartial", data);
        }

        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΚΑΤ' ΕΞΑΙΡΕΣΗ


        #region ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΩΝ

        public ActionResult xEpidomaSitisi2(string notify = null)
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

            List<sqlEpidomaSitisiSchoolViewModel> data = epidotisiRegistryService.ReadSitisi2();

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
            List<sqlEpidomaSitisiSchoolViewModel> data = epidotisiRegistryService.ReadSitisi2();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiSitisi2Record(int epidomaId)
        {
            var data = epidotisiRegistryService.GetRecordSitisi2(epidomaId);

            return PartialView("xEpidomaSitisi2Partial", data);
        }


        public ActionResult xEpidomaSitisi2Print()
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


        #endregion ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΩΝ


        #region ΟΛΙΚΟ ΜΗΤΡΩΟ ΕΠΙΔΟΤΗΣΕΩΝ

        public ActionResult EpidomaOliko(string notify = null)
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

            List<sqlEpidomaAllDetailViewModel> data = epidotisiRegistryService.ReadOliko();

            if (data.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }
            sqlEpidomaAllDetailViewModel epidoma = data.First();

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(epidoma);
        }

        public ActionResult EpidotisiOliko_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<sqlEpidomaAllDetailViewModel> data = epidotisiRegistryService.ReadOliko();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEpidotisiOlikoRecord(int recordId)
        {
            var data = epidotisiRegistryService.GetRecordOliko(recordId);

            return PartialView("EpidomaOlikoPartial", data);
        }

        #endregion ΟΛΙΚΟ ΜΗΤΡΩΟ ΕΠΙΔΟΤΗΣΕΩΝ


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ ΜΕ ΕΊΔΟΣ ΕΠΙΔΟΜΑΤΟΣ

        public ActionResult SumEpidomaDetail(string notify = null)
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

            List<SumEpidomaDetailViewModel> evm = GetSumEpidomaDetailFromDB();

            if (evm.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(evm);
        }

        public ActionResult SumEpidomaDetail_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SumEpidomaDetailViewModel> data = GetSumEpidomaDetailFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SumEpidomaDetailViewModel> GetSumEpidomaDetailFromDB()
        {
            var data = (from d in db.sqlSUM_EPIDOMA_DETAIL
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME
                        select new SumEpidomaDetailViewModel
                        {
                            ID = d.ID,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = d.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΜΗΝΑΣ = d.ΣΙΤΙΣΗ_ΜΗΝΑΣ,
                            ΣΤΕΓΑΣΗ_ΜΗΝΑΣ = d.ΣΤΕΓΑΣΗ_ΜΗΝΑΣ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();
            return data;
        }

        #endregion ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ ΜΕ ΕΊΔΟΣ ΕΠΙΔΟΜΑΤΟΣ


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ - ΟΛΑ ΤΑ ΕΠΙΔΟΜΑΤΑ

        public ActionResult SumEpidomaAllTypes(string notify = null)
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

            List<SumEpidomaAllTypesViewModel> evm = GetSumEpidomaAllTypesFromDB();

            if (evm.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(evm);
        }

        public ActionResult SumEpidomaAllTypes_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SumEpidomaAllTypesViewModel> data = GetSumEpidomaAllTypesFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SumEpidomaAllTypesViewModel> GetSumEpidomaAllTypesFromDB()
        {
            var data = (from d in db.sqlSUM_EPIDOMA_ALLTYPES
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.SCHOOL_NAME
                        select new SumEpidomaAllTypesViewModel
                        {
                            ID = d.ID,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            PERIFERIAKI_TEXT = d.PERIFERIAKI_TEXT,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΜΗΝΑΣ = d.ΣΙΤΙΣΗ_ΜΗΝΑΣ,
                            ΣΤΕΓΑΣΗ_ΜΗΝΑΣ = d.ΣΤΕΓΑΣΗ_ΜΗΝΑΣ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();
            return data;
        }

        #endregion ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ - ΟΛΑ ΤΑ ΕΠΙΔΟΜΑΤΑ


        #region ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ - ΑΝΑ ΣΧΟΛΙΚΟ ΕΤΟΣ

        public ActionResult SumEpidomaYear(string notify = null)
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

            List<SumEpidomaYearViewModel> evm = GetSumEpidomaYearFromDB();

            if (evm.Count() == 0)
            {
                string msg = "Δεν υπάρχουν καταχωρημένες επιδοτήσεις για την εμφάνιση του μητρώου.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View(evm);
        }

        public ActionResult SumEpidomaYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<SumEpidomaYearViewModel> data = GetSumEpidomaYearFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<SumEpidomaYearViewModel> GetSumEpidomaYearFromDB()
        {
            var data = (from d in db.sqlSUM_EPIDOMA_YEAR
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending
                        select new SumEpidomaYearViewModel
                        {
                            ID = d.ID,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            ΠΛΗΘΟΣ = d.ΠΛΗΘΟΣ,
                            ΣΙΤΙΣΗ_ΕΤΟΣ = d.ΣΙΤΙΣΗ_ΕΤΟΣ,
                            ΣΤΕΓΑΣΗ_ΕΤΟΣ = d.ΣΤΕΓΑΣΗ_ΕΤΟΣ,
                            ΣΙΤΙΣΗ_ΜΗΝΑΣ = d.ΣΙΤΙΣΗ_ΜΗΝΑΣ,
                            ΣΤΕΓΑΣΗ_ΜΗΝΑΣ = d.ΣΤΕΓΑΣΗ_ΜΗΝΑΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();
            return data;
        }

        #endregion ΣΥΓΚΕΝΤΡΩΤΙΚΑ ΣΤΟΙΧΕΙΑ - ΑΝΑ ΣΧΟΛΙΚΟ ΕΤΟΣ

    }
}