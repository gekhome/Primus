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
    public class StudentViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σχολική Δομή")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρ.Μητρ.")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΑΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "ΑΜΚΑ")]
        public string ΑΜΚΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "ΑΔΤ")]
        public string ΑΔΤ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφέρεια")]
        public Nullable<int> ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Δήμος")]
        public Nullable<int> ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιοχή")]
        public string ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Διεύθυνση")]
        public string ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφέρεια")]
        public Nullable<int> ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Δήμος")]
        public Nullable<int> ΔΙΑΜΟΝΗ_ΔΗΜΟΣ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιοχή")]
        public string ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Διεύθυνση")]
        public string ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }
    }

    public class StudentGridViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική Μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Σχολική Δομή")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

    }

    public class AitisiViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Μαθητής")]
        public int ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]   //auto filled
        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        //[Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]   // auto filled
        [Display(Name = "Τάξη")]
        public Nullable<int> ΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Ποσό μισθωτήριου")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [Display(Name = "Τέκνα οικογένειας")]
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Κοινωνική ομάδα")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [Display(Name = "Για επίδομα")]
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια επιδότησης")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Χιλιομετρική απόσταση")]
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public Nullable<int> ΑΞΙΟΛΟΓΗΣΗ { get; set; }

        [Display(Name = "Αιτία απόρριψης")]
        public Nullable<int> ΑΠΟΡΡΙΨΗ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΗΣ ΜΑΘΗΤΗΣ { get; set; }
    }

    public class AitisiGridViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Μαθητής")]
        public int ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τάξη")]
        public Nullable<int> ΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

    }

    public class AitisiSocialGroupViewModel
    {
        public int AITISI_SOCIALID { get; set; }

        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Κοινωνικό κριτήριο")]
        public int ΚΡΙΤΗΡΙΟ_ΚΩΔ { get; set; }
    }

    public class sqlAitiseisViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Μαθητής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public string ΣΥΝΕΧΕΙΑ { get; set; }

        public Nullable<int> ΣΧΟΛΗ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογ.εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μισθωτήριο")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ποσό σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ποσό στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        // Extension
        [Display(Name = "Διαμονή:Τηλέφωνο")]
        public string ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ { get; set; }

        [Display(Name = "Διαμονή:Δήμος")]
        public string ΔΙΑΜΟΝΗ_ΔΗΜΟΣ { get; set; }

        [Display(Name = "Διαμονή:Περιφέρεια")]
        public string ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαμονή:Διεύθυνση")]
        public string ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Διαμονή:Περιοχή")]
        public string ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        [Display(Name = "Κατοικία:Δήμος")]
        public string ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ { get; set; }

        [Display(Name = "Κατοικία:Περιφέρεια")]
        public string ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Κατοικία:Διεύθυνση")]
        public string ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Κατοικία:Περιοχή")]
        public string ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ { get; set; }

        [Display(Name = "Κατοικία:Τηλέφωνο")]
        public string ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ { get; set; }


    }

    public class StudentInfoViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public DateTime? ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Κατοικία:Δήμος")]
        public string ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ { get; set; }

        [Display(Name = "Κατοικία:Περιφέρεια")]
        public string ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Κατοικία:Διεύθυνση")]
        public string ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Κατοικία:Περιοχή")]
        public string ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ { get; set; }

        [Display(Name = "Διαμονή:Δήμος")]
        public string ΔΙΑΜΟΝΗ_ΔΗΜΟΣ { get; set; }

        [Display(Name = "Διαμονή:Περιφέρεια")]
        public string ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ { get; set; }

        [Display(Name = "Διαμονή:Διεύθυνση")]
        public string ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Διαμονή:Περιοχή")]
        public string ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ { get; set; }

        [Display(Name = "Κατοικία:Τηλέφωνο")]
        public string ΚΑΤΟΙΚΙΑ_ΤΗΛΕΦΩΝΟ { get; set; }

        [Display(Name = "Διαμονή:Τηλέφωνο")]
        public string ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΟ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        public int? ΣΧΟΛΗ { get; set; }

    }

    public class DuplStudentViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "ΑΔΤ")]
        public string ΑΔΤ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int EIDIKOTITA_ID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }
    }
}