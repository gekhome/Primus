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
    public class SchoolsViewModel
    {
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string SCHOOL_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ταχ. Διεύθυνση")]
        public string ΤΑΧ_ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Διευθυντή")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Γραμματείας")]
        public string ΓΡΑΜΜΑΤΕΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Φαξ")]
        public string ΦΑΞ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Διευθυντής")]
        public string ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο διευθυντή")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Κινητό διευθυντή")]
        public string ΚΙΝΗΤΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Δομή")]
        public Nullable<int> SCHOOL_TYPE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public Nullable<int> ΠΕΡΙΦΕΡΕΙΑΚΗ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Υποδιευθυντής")]
        public string ΥΠΟΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Φύλο υποδιευθυντή")]
        public Nullable<int> ΥΠΟΔΙΕΥΘΥΝΤΗΣ_ΦΥΛΟ { get; set; }

    }

    public class SchoolsGridViewModel
    {
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Επωνυμία")]
        public string SCHOOL_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Δομή")]
        public Nullable<int> SCHOOL_TYPE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Διευθυντής")]
        public string ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλ. Διευθυντή")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }
    }

    public class EidikotitesViewModel
    {
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος σχολής")]
        public Nullable<int> SCHOOL_TYPE { get; set; }

    }

    public class EidikotitaYearSchoolViewModel
    {
        public int SYE_ID { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public int SCHOOLYEAR_ID { get; set; }

        [Display(Name = "Σχολή")]
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public int EIDIKOTITA_ID { get; set; }

    }

    // Χρησιμοποιείται μόνο για ενημέρωση (καταχώρηση από διαχειριστή).
    // Στην εφαρμογή το σχολείο καταχωρεί για κάθε σχολικό έτος τις ειδικότητες
    // που λειτουργεί.
    public class YearsSchoolsEidikotitesViewModel
    {
        public int SSE_ID { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public int SCHOOLYEAR_ID { get; set; }

        [Display(Name = "Σχολή")]
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public int EIDIKOTITA_ID { get; set; }

    }

    // ΜΟΝΤΕΛΑ ΓΙΑ ΑΠΟΦΑΣΕΙΣ ΣΧΟΛΕΙΩΝ
    public class ApofasiParameters2ViewModel
    {
        public int ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Υπουργική απόφαση")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "ΦΕΚ για Διοικητή")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Απόφαση ΔΣ ΟΑΕΔ")]
        public string ΕΓΚΥΚΛΙΟΙ_Α2 { get; set; }

    }

    public class ApofasiSitisi2ViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Υπουργική")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Display(Name = "ΦΕΚ για Διοικητή")]
        public string ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Απόφαση ΔΣ ΟΑΕΔ")]
        public string ΕΓΚΥΚΛΙΟΙ_Α2 { get; set; }

        [Display(Name = "Πρακτικά σχολείου")]
        public string ΠΡΑΚΤΙΚΑ_ΣΧΟΛΕΙΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

        [Display(Name = "Λεκτικό πλήθους")]
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Μήνας")]
        public Nullable<int> ΜΗΝΑΣ { get; set; }

        [Display(Name = "Έτος")]
        public string ΕΤΟΣ { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

    }

    public class EpidomaSitisi2ViewModel
    {
        public int ΕΠΙΔΟΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Κωδ.Αίτησης")]
        public Nullable<int> AITISI_ID { get; set; }

        [Display(Name = "Μαθητής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΜΑΘΗΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΜΑΘΗΤΗΣ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Τάξη")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΤΑΞΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία από")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [Display(Name = "Επίδομα")]
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση ποσό")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση ποσό")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΙΣΗ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΕΙΟ { get; set; }

        [Display(Name = "Κωδ.Απόφασης")]
        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Μήνας")]
        public Nullable<int> ΜΗΝΑΣ { get; set; }

    }

    public class Aitisi2ViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Display(Name = "Μαθητής")]
        public int ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public int? ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Τάξη")]
        public int? ΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αίτησης")]
        public DateTime? ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία μισθωτήριου")]
        public DateTime? ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Ποσό μισθωτήριου")]
        public decimal? ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }

        [Display(Name = "Τέκνα οικογένειας")]
        public int? ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εισόδημα")]
        public decimal? ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }

        [Display(Name = "Ηλικία")]
        public int? ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Κοινωνική ομάδα")]
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }

        [Display(Name = "Για επίδομα")]
        public int? ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα στέγασης")]
        public decimal? ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Επίδομα σίτισης")]
        public decimal? ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια επιδότησης")]
        public bool ΣΥΝΕΧΕΙΑ { get; set; }

        [Display(Name = "Χιλιομετρική απόσταση")]
        public decimal? ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }

        [Display(Name = "Αξιολόγηση")]
        public int? ΑΞΙΟΛΟΓΗΣΗ { get; set; }

        [Display(Name = "Αιτία απόρριψης")]
        public int? ΑΠΟΡΡΙΨΗ { get; set; }

        public virtual ΜΑΘΗΤΗΣ ΜΑΘΗΤΗΣ { get; set; }

    }

    public class Aitisi2GridViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Μαθητής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

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

        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

    }

    public class Aitisi2SocialGroupViewModel
    {
        public int AITISI_SOCIALID { get; set; }

        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Κοινωνικό κριτήριο")]
        public int ΚΡΙΤΗΡΙΟ_ΚΩΔ { get; set; }
    }

    // ΕΠΙΔΟΤΗΣΕΙΣ ΣΙΤΙΣΗΣ ΣΧΟΛΕΙΩΝ
    public class sqlEpidomaSitisi2ViewModel
    {
        public int ΕΠΙΔΟΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΜΑΘΗΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Περιφερειακή Δ/νση")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία από")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/έτος")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/έτος")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/μήνα")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/μήνα")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΕΙΟ { get; set; }
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class sqlAitiseis2ViewModel
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

    public class StudentInfo2ViewModel
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
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

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

        public Nullable<int> ΣΧΟΛΗ { get; set; }

    }

    public class EpidomaParameters2
    {
        public int apofasiId { get; set; }
        public int schoolyearId { get; set; }
        public int monthId { get; set; }
        public int schoolId { get; set; }
        public int epidomaId { get; set; }
        public bool synexeia { get; set; }
    }

}