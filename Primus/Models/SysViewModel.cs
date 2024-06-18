using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Primus.Models;
using Primus.DAL;
using System.Web.Mvc;

namespace Primus.Models
{
    public class SysGenderViewModel
    {
        public int GENDER_ID { get; set; }
        public string GENDER { get; set; }

    }

    public class SysTaxisViewModel
    {
        public int ΤΑΞΗ_ΚΩΔ { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class SysSchoolUnitViewModel
    {
        public int ΔΟΜΗ_ΚΩΔ { get; set; }

        [Display(Name = "Σχολική Δομή")]
        public string ΔΟΜΗ_ΚΕΙΜΕΝΟ { get; set; }
    }

    public class SysSchoolYearViewModel
    {
        public int SCHOOLYEAR_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(9, ErrorMessage = "Πρέπει να είναι μέχρι 9 χαρακτήρες (π.χ.2015-2016).")]
        [Display(Name = "Σχολικό Έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία Έναρξης")]
        public Nullable<System.DateTime> DATE_START { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία Λήξης")]
        public Nullable<System.DateTime> DATE_END { get; set; }
    }

    public class SysSchoolTypeViewModel
    {
        public int SCHOOLTYPE_ID { get; set; }

        [Display(Name = "Είδος σχολής")]
        public string SCHOOLTYPE_TEXT { get; set; }

    }

    public class SysSchoolsViewModel
    {
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string SCHOOL_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική δομή")]
        public Nullable<int> SCHOOL_TYPE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public Nullable<int> PERIFERIAKI { get; set; }

    }

    public class SysPeriferiaViewModel
    {
        public int PERIFERIA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφέρεια")]
        public string PERIFERIA_NAME { get; set; }

    }

    public class SysPeriferiakiEnotitaViewModel
    {
        public int PERIFERIA_ENOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Περιφερειακή ενότητα")]
        public string PERIFERIA_ENOTITA_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφέρεια")]
        public Nullable<int> PERIFERIA_ID { get; set; }

        public virtual ICollection<ΣΥΣ_ΔΗΜΟΣ> ΣΥΣ_ΔΗΜΟΣ { get; set; }
        public virtual ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑ ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }
    }

    public class SysDimosViewModel
    {
        public int DIMOS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(100, ErrorMessage = "Πρέπει να είναι μέχρι 100 χαρακτήρες.")]
        [Display(Name = "Δήμος")]
        public string DIMOS { get; set; }

        [Display(Name = "Περιφέρεια")]
        public Nullable<int> DIMOS_PERIFERIA { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public virtual ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ { get; set; }

    }

    public class SysPerferiakiOaedViewModel
    {
        public int PERIFERIAKI_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιφερειακή Διεύθυνση")]
        public string PERIFERIAKI_TEXT { get; set; }

    }

    public class SysEpidomaTypeViewModel
    {
        public int ΕΠΙΔΟΜΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Είδος επιδόματος")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιγραφή επιδόματος")]
        public string ΕΠΙΔΟΜΑ_ΠΕΡΙΓΡΑΦΗ { get; set; }


    }

    public class SysEgrafoTypeViewModel
    {
        public int ΕΓΓΡΑΦΟ_ΕΙΔΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Είδος εγγράφου")]
        public string ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

    }

    public class AporipsiAitiaViewModel
    {
        public int ΑΠΟΡΡΙΨΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αιτιολογία απόρριψης")]
        public string ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ { get; set; }

    }

    public class AxiologisiViewModel
    {
        public int ΑΞΙΟΛΟΓΗΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αξιολόγηση")]
        public string ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ { get; set; }

    }

    public class SocialGroupViewModel
    {
        public int ΚΟΙΝΩΝΙΚΟ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Κοινωνική ομάδα")]
        public string ΚΟΙΝΩΝΙΚΟ_ΛΕΚΤΙΚΟ { get; set; }

    }

    public class SysApofasiParametersViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική δομή")]
        public Nullable<int> ΣΧΟΛΗ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Υπουργική")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εγκύκλιος Διοίκησης")]
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Δ.Σ.")]
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφάσεις Δ.Σ.")]
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κ.Α.Ε.")]
        public string ΚΑΕ { get; set; }

    }

    public class EpidomaPosoViewModel
    {
        public int ΕΠΙΔΟΤΗΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Στέγαση μηνιαίο")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Σίτιση ημερήσιο")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Στέγαση λεκτικό")]
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σίτιση λεκτικό")]
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "ΚΑΕ")]
        public string ΚΑΕ { get; set; }

    }

    public class DimoiParameters
    {
        public int PERIFERIA_ID { get; set; }
    }

    public class YearViewModel
    {
        public int YEAR_ID { get; set; }

        [Display(Name = "Έτος")]
        public string YEAR_TEXT { get; set; }
    }

    public class MonthViewModel
    {
        public int MONTH_ID { get; set; }

        [Display(Name = "Μήνας")]
        public string MONTH_TEXT { get; set; }
    }

    public class SchoolLoginsViewModel
    {
        public int LOGIN_ID { get; set; }

        [Display(Name = "Εκπαιδευτική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Τελευταία είσοδος")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public Nullable<System.DateTime> LOGIN_DATETIME { get; set; }

    }

}