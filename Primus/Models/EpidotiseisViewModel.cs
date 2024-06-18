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
    public class EpidomaXorigisiViewModel
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

        // may not be required
        public virtual ΑΠΟΦΑΣΗ_ΧΟΡΗΓΗΣΗ ΑΠΟΦΑΣΗ_ΧΟΡΗΓΗΣΗ { get; set; }
    }

    public class EpidomaSynexeiaViewModel
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

        // may not be required
        public virtual ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ { get; set; }
    }

    public class EpidomaStegasiViewModel
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

        // may not be required
        public virtual ΑΠΟΦΑΣΗ_ΣΤΕΓΑΣΗ ΑΠΟΦΑΣΗ_ΣΤΕΓΑΣΗ { get; set; }
    }

    public class EpidomaSitisiViewModel
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

        // may not be required
        public virtual ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ { get; set; }
    }

    public class EpidomaRegistryViewModel
    {
        public int ID { get; set; }
        public int ΕΠΙΔΟΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Απόφαση είδος")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Περιφερειακή Δ/νση")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΜΑΘΗΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Τάξη")]
        public string ΤΑΞΗ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία από")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση ποσό")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση ποσό")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }

        [Display(Name = "Συνέχεια")]
        public bool ΣΥΝΕΧΙΣΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        public Nullable<int> ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }
        public Nullable<int> ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ { get; set; }
        public Nullable<int> ΜΑΘΗΤΗΣ_ΤΑΞΗ { get; set; }
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΕΙΟ { get; set; }
        public Nullable<int> AITISI_ID { get; set; }
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
        public int PERIFERIAKI_ID { get; set; }

    }

    // Αυτές χρησιμοποιούνται στην επισύναψη αιτήσεων
    public class EpidomaParameters
    {
        public int apofasiId { get; set; }
        public int schoolyearId { get; set; }
        public int schoolId { get; set; }
        public int epidomaId { get; set; }
        public bool synexeia { get; set; }
    }


    // ΜΗΤΡΩΑ ΕΠΙΔΟΤΗΣΕΩΝ (ΟΛΑ SQL Views)
    public class sqlEpidomaXorigisiViewModel
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

    public class sqlEpidomaSynexeiaViewModel
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

    public class sqlEpidomaStegasiViewModel
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

    public class sqlEpidomaSitisiViewModel
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

    public class sqlEpidomaAllDetailViewModel
    {
        public int ID { get; set; }

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

    public class SumEpidomaDetailViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Περιφερειακή Δ/νση")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Επίδομα")]
        public string ΕΠΙΔΟΜΑ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Μαθητές")]
        public Nullable<int> ΠΛΗΘΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/έτος")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/έτος")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/μήνα")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΑΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/μήνα")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΜΗΝΑΣ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΕΙΟ { get; set; }
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }

    }

    public class SumEpidomaAllTypesViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Περιφερειακή Δ/νση")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Μαθητές")]
        public Nullable<int> ΠΛΗΘΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/έτος")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/έτος")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/μήνα")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΑΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/μήνα")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΜΗΝΑΣ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public Nullable<int> ΣΧΟΛΕΙΟ { get; set; }

    }

    public class SumEpidomaYearViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Μαθητές")]
        public Nullable<int> ΠΛΗΘΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/έτος")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/έτος")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΕΤΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Στέγαση/μήνα")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΑΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Σίτιση/μήνα")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΜΗΝΑΣ { get; set; }

        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

    }

    // ΜΗΤΡΩΟ ΕΠΙΔΟΤΗΣΕΩΝ ΣΧΟΛΕΙΩΝ (ΣΙΤΙΣΗΣ) - data source = sqlEPIDOMA_SITISI2
    public class sqlEpidomaSitisiSchoolViewModel
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

}