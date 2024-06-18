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
    public class AitiseisAcceptViewModel
    {
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Μαθητής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογ.εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Τέκνα")]
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μισθωτήριο")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Απόσταση (χλμ)")]
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public int PERIFERIAKI_ID { get; set; }
        public int ΤΑΞΗ_ΚΩΔ { get; set; }
        public int ΕΠΙΔΟΜΑ_ΚΩΔ { get; set; }
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        // Extension - Student data
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

    }

    public class AitiseisRejectViewModel
    {
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Μαθητής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογ.εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Απόρριψη")]
        public string ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Τέκνα")]
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μισθωτήριο")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Απόσταση (χλμ)")]
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public int PERIFERIAKI_ID { get; set; }
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public int ΤΑΞΗ_ΚΩΔ { get; set; }
        public int ΕΠΙΔΟΜΑ_ΚΩΔ { get; set; }
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        // Extension - Student data
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

    }

    public class AitiseisPendingViewModel
    {
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Μαθητής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογ.εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Τέκνα")]
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μισθωτήριο")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Απόσταση (χλμ)")]
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public int PERIFERIAKI_ID { get; set; }
        public int ΤΑΞΗ_ΚΩΔ { get; set; }
        public int ΕΠΙΔΟΜΑ_ΚΩΔ { get; set; }
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        // Extension - Student data
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

    }

    public class AitiseisRegistryViewModel
    {
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Μαθητής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Περιφερειακή")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Κοινωνικά κριτήρια")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Οικογ.εισόδημα")]
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μισθωτήριο")]
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public string ΑΞΙΟΛΟΓΗΣΗ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Απόρριψη")]
        public string ΑΠΟΡΡΙΨΗ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Τέκνα")]
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [Display(Name = "Απόσταση (χλμ)")]
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }
   
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
        public int ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }
        public int ΕΠΙΔΟΜΑ_ΚΩΔ { get; set; }

        // Extension - Student data
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

    }

}