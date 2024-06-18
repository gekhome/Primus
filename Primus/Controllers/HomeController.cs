using Primus.DAL;
using Primus.Models;
using Primus.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Primus.Controllers
{
    [ErrorHandlerFilter]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            string userTxt = "(χωρίς σύνδεση)";
            try
            {
                bool AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            ViewBag.loggedUser = userTxt;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AppStatusOff()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ErrorConnect()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Σύντομη περιγραφή της εφαρμογής.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Στοιχεία επικοινωνίας.";

            return View();
        }

        public bool GetApplicationStatus()
        {
            using (var db = new PrimusDBEntities())
            {
                var data = (from d in db.APP_STATUS select d).FirstOrDefault();
                bool status = data.STATUS_VALUE ?? false;
                return status;
            }
        }


        #region States of Grids

        [ValidateInput(false)]
        public ActionResult Save(string data)
        {
            Session["data"] = data;

            string temp = data;
            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult Load()
        {
            string data;

            if (Session["data"] != null)
            {
                data = Session["data"].ToString();
            }
            return Json(Session["data"], JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveRow(string data)
        {
            Session["row"] = data;

            return new EmptyResult();
        }

        [AllowAnonymous]
        public ActionResult LoadRow()
        {
            if (Session["row"] != null)
            {
                string data = Session["row"].ToString();
            }
            return Json(Session["row"], JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}