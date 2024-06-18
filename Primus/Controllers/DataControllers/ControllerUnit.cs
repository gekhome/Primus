using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Primus.DAL;
using Primus.Models;
using Primus.BPM;
using Primus.Notification;
using Primus.Services;

namespace Primus.Controllers.DataControllers
{
    public class ControllerUnit : Controller
    {
        private USER_ADMINS loggedAdmin;
        private USER_SCHOOLS loggedSchool;

        private readonly PrimusDBEntities db;

        public ControllerUnit(PrimusDBEntities entities)
        {
            db = entities;
        }


        #region APOFASEIS FORM GETTERS and COMMON FUNCTIONS

        public void SetAitisiApofasiCode(sqlΑΙΤΗΣΕΙΣ_ΕΠΙΣΥΝΑΨΗ data, int apofasiId)
        {
            ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ entity = db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
            if (entity != null)
            {
                entity.ΑΠΟΦΑΣΗ_ΚΩΔ = apofasiId;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public JsonResult GetDiaxiristes()
        {
            var data = db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(m => new DiaxiristisViewModel
            {
                ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = m.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = m.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ
            }).OrderBy(d => d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEgrafoTypes()
        {
            var data = db.ΣΥΣ_ΕΓΓΡΑΦΑ_ΕΙΔΗ.Select(m => new SysEgrafoTypeViewModel
            {
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ_ΚΩΔ = m.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ_ΚΩΔ,
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = m.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProistamenos()
        {
            var data = db.Δ_ΠΡΟΙΣΤΑΜΕΝΟΙ.Select(m => new ProistamenosViewModel
            {
                ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = m.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ,
                ΠΡΟΙΣΤΑΜΕΝΟΣ = m.ΠΡΟΙΣΤΑΜΕΝΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDirectors()
        {
            var data = db.Δ_ΔΙΕΥΘΥΝΤΕΣ.Select(m => new DirectorViewModel
            {
                ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = m.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ,
                ΔΙΕΥΘΥΝΤΗΣ = m.ΔΙΕΥΘΥΝΤΗΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGenikos()
        {
            var data = db.Δ_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Select(m => new DirectorGeneralViewModel
            {
                ΓΕΝΙΚΟΣ_ΚΩΔ = m.ΓΕΝΙΚΟΣ_ΚΩΔ,
                ΓΕΝΙΚΟΣ = m.ΓΕΝΙΚΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDioikitis()
        {
            var data = db.Δ_ΔΙΟΙΚΗΤΕΣ.Select(m => new DioikitisViewModel
            {
                ΔΙΟΙΚΗΤΗΣ_ΚΩΔ = m.ΔΙΟΙΚΗΤΗΣ_ΚΩΔ,
                ΔΙΟΙΚΗΤΗΣ = m.ΔΙΟΙΚΗΤΗΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAntiproedros()
        {
            var data = db.Δ_ΑΝΤΙΠΡΟΕΔΡΟΙ.Select(m => new AntiproedrosViewModel
            {
                ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = m.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ,
                ΑΝΤΙΠΡΟΕΔΡΟΣ = m.ΑΝΤΙΠΡΟΕΔΡΟΣ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region POPULATORS

        public void PopulateDocTypes()
        {
            var data = (from d in db.ΣΥΣ_ΕΓΓΡΑΦΑ_ΕΙΔΗ select d).ToList();

            ViewData["doctypes"] = data;
        }

        public void PopulateSocialGroups()
        {
            var data = (from d in db.ΚΟΙΝΩΝΙΚΑ_ΚΡΙΤΗΡΙΑ select d).ToList();

            ViewData["socialgroups"] = data;
        }

        public void PopulateStudents()
        {
            var data = (from d in db.qrySTUDENT_SELECTOR
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select d).ToList();

            ViewData["students"] = data;
        }

        public void PopulateGenders()
        {
            var data = (from d in db.ΣΥΣ_ΦΥΛΑ select d).ToList();
            ViewData["genders"] = data;
        }

        public void PopulateSchoolYears()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ orderby d.SCHOOLYEAR_TEXT select d).ToList();
            ViewData["schoolYears"] = data;
            ViewData["defaultSchoolYear"] = data.First().SCHOOLYEAR_ID;
        }

        public void PopulateSchools()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ orderby d.ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ.ΔΟΜΗ_ΚΕΙΜΕΝΟ, d.SCHOOL_NAME select d).ToList();
            ViewData["schools"] = data;
        }

        public void PopulateEidikotites()
        {
            var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ orderby d.SCHOOL_TYPE, d.EIDIKOTITA_TEXT select d).ToList();
            ViewData["eidikotites"] = data;
        }

        public void PopulateEidikotites(int schoolId)
        {
            int school_type = GetSchoolType(schoolId);

            var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ where d.SCHOOL_TYPE == school_type orderby d.SCHOOL_TYPE, d.EIDIKOTITA_TEXT select d).ToList();
            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateSchoolDomes()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ select d).ToList();
            ViewData["schoolDomes"] = data;
        }

        public void PopulateSchoolTypes()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ_ΕΙΔΗ select d).ToList();
            ViewData["schoolTypes"] = data;
        }

        public void PopulateTakseis()
        {
            var classes = (from d in db.ΣΥΣ_ΤΑΞΕΙΣ select d).ToList();

            ViewData["takseis"] = classes;
        }

        public void PopulateDiaxiristes()
        {
            var data = (from d in db.Δ_ΔΙΑΧΕΙΡΙΣΤΕΣ orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ select d).ToList();

            ViewData["diaxiristes"] = data;
            ViewData["defaultDiaxiristis"] = data.First().ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ;
        }

        public void PopulateEpidomata()
        {
            var data = (from d in db.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ select d).ToList();

            ViewData["epidomata"] = data;
        }

        public void PopulateYears()
        {
            var data = (from d in db.ΣΥΣ_ΕΤΗ select d).ToList();

            ViewData["years"] = data;
        }

        public void PopulateMonths()
        {
            var data = (from d in db.ΣΥΣ_ΜΗΝΕΣ select d).ToList();

            ViewData["months"] = data;
        }

        public void PopulateStudents(int schoolId)
        {
            var data = (from d in db.qrySTUDENT_SELECTOR
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        where d.ΣΧΟΛΗ == schoolId
                        select d).ToList();

            ViewData["students"] = data;
        }

        public void PopulateShoolYears()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ orderby d.SCHOOLYEAR_TEXT select d).ToList();
            ViewData["schoolYears"] = data;
        }

        public void populateUserSchools()
        {
            var schools = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ select d).ToList();

            ViewData["schools"] = schools;
        }

        #endregion POPULATORS


        #region LOCAL FUNCTIONS (GETTERS, SETTERS)

        public AitisiViewModel SetAitisiFields(AitisiViewModel data, int aitisiId)
        {
            var fields = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new { d.ΜΑΘΗΤΗΣ_ΚΩΔ, d.ΣΧΟΛΙΚΟ_ΕΤΟΣ, d.ΤΑΞΗ }).FirstOrDefault();

            data.ΜΑΘΗΤΗΣ_ΚΩΔ = (int)fields.ΜΑΘΗΤΗΣ_ΚΩΔ;
            data.ΣΧΟΛΙΚΟ_ΕΤΟΣ = fields.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            data.ΤΑΞΗ = fields.ΤΑΞΗ;

            return data;
        }

        public Aitisi2ViewModel SetAitisi2Fields(Aitisi2ViewModel data, int aitisiId)
        {
            var fields = (from d in db.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                          where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                          select new { d.ΜΑΘΗΤΗΣ_ΚΩΔ, d.ΣΧΟΛΙΚΟ_ΕΤΟΣ, d.ΤΑΞΗ }).FirstOrDefault();

            data.ΜΑΘΗΤΗΣ_ΚΩΔ = (int)fields.ΜΑΘΗΤΗΣ_ΚΩΔ;
            data.ΣΧΟΛΙΚΟ_ΕΤΟΣ = fields.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            data.ΤΑΞΗ = fields.ΤΑΞΗ;

            return data;
        }

        public JsonResult GetSchoolTypes(string text)
        {
            var data = db.ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ.Select(p => new SysSchoolUnitViewModel
            {
                ΔΟΜΗ_ΚΩΔ = p.ΔΟΜΗ_ΚΩΔ,
                ΔΟΜΗ_ΚΕΙΜΕΝΟ = p.ΔΟΜΗ_ΚΕΙΜΕΝΟ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΔΟΜΗ_ΚΕΙΜΕΝΟ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchools()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        orderby d.SCHOOL_TYPE, d.SCHOOL_NAME
                        select new SchoolsViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEidikotites()
        {
            var data = db.ΕΙΔΙΚΟΤΗΤΕΣ.Select(m => new EidikotitesViewModel
            {
                EIDIKOTITA_ID = m.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = m.EIDIKOTITA_TEXT
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolYears()
        {
            var data = db.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Select(m => new SysSchoolYearViewModel
            {
                SCHOOLYEAR_ID = m.SCHOOLYEAR_ID,
                SCHOOLYEAR_TEXT = m.SCHOOLYEAR_TEXT
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTakseis()
        {
            var data = db.ΣΥΣ_ΤΑΞΕΙΣ.Select(m => new SysTaxisViewModel
            {
                ΤΑΞΗ_ΚΩΔ = m.ΤΑΞΗ_ΚΩΔ,
                ΤΑΞΗ_ΛΕΚΤΙΚΟ = m.ΤΑΞΗ_ΛΕΚΤΙΚΟ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEpidomaTypes()
        {
            var data = db.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Select(m => new SysEpidomaTypeViewModel
            {
                ΕΠΙΔΟΜΑ_ΚΩΔ = m.ΕΠΙΔΟΜΑ_ΚΩΔ,
                ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = m.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EidikotitesRead(int schoolId)
        {
            int? school_type = 0;

            var type = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
            if (type != null) school_type = type.SCHOOL_TYPE;

            if (school_type > 0)
            {
                var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ
                            where d.SCHOOL_TYPE == school_type
                            orderby d.EIDIKOTITA_TEXT
                            select new EidikotitesViewModel
                            {
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ
                            where d.SCHOOL_TYPE == 1            //default EPAS
                            select new EidikotitesViewModel
                            {
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetGenders(string text)
        {
            var genders = db.ΣΥΣ_ΦΥΛΑ.Select(p => new SysGenderViewModel
            {
                GENDER = p.GENDER,
                GENDER_ID = p.GENDER_ID
            });

            if (!string.IsNullOrEmpty(text))
            {
                genders = genders.Where(p => p.GENDER.Contains(text));
            }

            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolsByType(int schoolType)
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.SCHOOL_TYPE == schoolType
                        select new SchoolsViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEidikotitesByType(int schoolType)
        {
            var data = db.ΕΙΔΙΚΟΤΗΤΕΣ.Where(m => m.SCHOOL_TYPE == schoolType).Select(m => new EidikotitesViewModel
            {
                EIDIKOTITA_ID = m.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = m.EIDIKOTITA_TEXT
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeriferiesEnotites()
        {
            var data = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ.Select(m => new SysPeriferiakiEnotitaViewModel
            {
                PERIFERIA_ENOTITA_ID = m.PERIFERIA_ENOTITA_ID,
                PERIFERIA_ENOTITA_NAME = m.PERIFERIA_ENOTITA_NAME
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDimoi(int periferia)
        {
            var data = db.ΣΥΣ_ΔΗΜΟΣ.Where(m => m.DIMOS_PERIFERIA == periferia).Select(m => new SysDimosViewModel
            {
                DIMOS_ID = m.DIMOS_ID,
                DIMOS = m.DIMOS
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAxiologiseis()
        {
            var data = db.ΣΥΣ_ΑΞΙΟΛΟΓΗΣΗ.Select(m => new AxiologisiViewModel
            {
                ΑΞΙΟΛΟΓΗΣΗ_ΚΩΔ = m.ΑΞΙΟΛΟΓΗΣΗ_ΚΩΔ,
                ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ = m.ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAporipseis()
        {
            var data = db.ΣΥΣ_ΑΠΟΡΡΙΨΗ_ΑΙΤΙΑ.Select(m => new AporipsiAitiaViewModel
            {
                ΑΠΟΡΡΙΨΗ_ΚΩΔ = m.ΑΠΟΡΡΙΨΗ_ΚΩΔ,
                ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ = m.ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public int GetSchoolType(int schoolId)
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
            int school_type = (int)data.SCHOOL_TYPE;

            return (school_type);
        }

        // Used by logged school in eidikotites editor template
        public JsonResult Eidikotites_Read()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            int? school_type = 0;

            var type = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
            if (type != null) school_type = type.SCHOOL_TYPE;

            if (school_type > 0)
            {
                var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ
                            where d.SCHOOL_TYPE == school_type
                            select new EidikotitesViewModel
                            {
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from d in db.ΕΙΔΙΚΟΤΗΤΕΣ
                            select new EidikotitesViewModel
                            {
                                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMonths()
        {
            var data = db.ΣΥΣ_ΜΗΝΕΣ.Select(m => new MonthViewModel
            {
                MONTH_ID = m.MONTH_ID,
                MONTH_TEXT = m.MONTH_TEXT
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string GetSchoolDirector()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;
            string director = "";

            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ where d.SCHOOL_ID == schoolId select d).FirstOrDefault();
            director = data.ΔΙΕΥΘΥΝΤΗΣ;

            return (director);
        }

        public JsonResult GetPeriferiakes(string text)
        {
            var periferiakes = db.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Select(p => new SysPerferiakiOaedViewModel
            {
                PERIFERIAKI_ID = p.PERIFERIAKI_ID,
                PERIFERIAKI_TEXT = p.PERIFERIAKI_TEXT
            });

            if (!string.IsNullOrEmpty(text))
            {
                int possibleInt;
                if (int.TryParse(text, out possibleInt))
                {
                    periferiakes = periferiakes.Where(p => p.PERIFERIAKI_ID.Equals(possibleInt));
                }
                else
                {
                    periferiakes = periferiakes.Where(p => p.PERIFERIAKI_TEXT.Contains(text));
                }
                periferiakes = periferiakes.Where(p => p.PERIFERIAKI_TEXT.Contains(text));
            }
            return Json(periferiakes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDomes(string text)
        {
            var data = db.ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ.Select(p => new SysSchoolUnitViewModel
            {
                ΔΟΜΗ_ΚΩΔ = p.ΔΟΜΗ_ΚΩΔ,
                ΔΟΜΗ_ΚΕΙΜΕΝΟ = p.ΔΟΜΗ_ΚΕΙΜΕΝΟ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΔΟΜΗ_ΚΕΙΜΕΝΟ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchoolsEpas()
        {
            var data = (from d in db.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.SCHOOL_TYPE == 1
                        orderby d.SCHOOL_NAME
                        select new SchoolsViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEpidomaTypes2()
        {
            var data = db.ΣΥΣ_ΕΠΙΔΟΜΑΤΑ.Where(m => m.ΕΠΙΔΟΜΑ_ΚΩΔ == 2).Select(m => new SysEpidomaTypeViewModel
            {
                ΕΠΙΔΟΜΑ_ΚΩΔ = m.ΕΠΙΔΟΜΑ_ΚΩΔ,
                ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ = m.ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEidikotites2()
        {
            var data = db.ΕΙΔΙΚΟΤΗΤΕΣ.Where(m => m.SCHOOL_TYPE == 1).Select(m => new EidikotitesViewModel
            {
                EIDIKOTITA_ID = m.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = m.EIDIKOTITA_TEXT
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }

        public USER_SCHOOLS GetLoginSchool()
        {
            loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
            var _school = (from s in db.sqlUSER_SCHOOL
                           where s.USER_SCHOOLID == SchoolID
                           select new { s.SCHOOL_NAME }).FirstOrDefault();

            ViewBag.loggedUser = _school.SCHOOL_NAME;
            return loggedSchool;
        }

        #endregion
    }
}