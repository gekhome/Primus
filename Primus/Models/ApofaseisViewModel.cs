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
    public class ApofasiXorigisiViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public int? ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public int? ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public int? ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public int? ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφαση Διοικητή")]
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Δ.Σ. ΟΑΕΔ")]
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφάσεις Δ.Σ.")]
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Α.Δ.Α.")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public int? ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public int? ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public int? ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public int? ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public int? ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public int? ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει o Υποδιοικητής α.α")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μηνιαίο επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ημερήσιο επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος στέγασης")]
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος σίτισης")]
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

        [Display(Name = "Λεκτικό πλήθους")]
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Κ.Α.Ε.")]
        public string ΚΑΕ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiXorigisiGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiSynexeiaViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή")]
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Δ.Σ. ΟΑΕΔ")]
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφάσεις Δ.Σ.")]
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }

        [Display(Name = "Σχετικές Αποφάσεις*")]
        public string ΑΠΟΦΑΣΕΙΣ_ΣΧΕΤΙΚΕΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Α.Δ.Α.")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }


        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει ο Υποδιοικητής α.α")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μηνιαίο επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ημερήσιο επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος στέγασης")]
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος σίτισης")]
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

        [Display(Name = "Λεκτικό πλήθους")]
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Κ.Α.Ε.")]
        public string ΚΑΕ { get; set; }

        [Display(Name = "Εμφάνιση κειμένου*")]
        public bool SHOW_STAR { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiSynexeiaGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

    }

    public class ApofasiStegasiViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή")]
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Δ.Σ. ΟΑΕΔ")]
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφάσεις Δ.Σ.")]
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Α.Δ.Α.")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει ο Υποδιοικητής α.α")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μηνιαίο επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ημερήσιο επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος στέγασης")]
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος σίτισης")]
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

        [Display(Name = "Λεκτικό πλήθους")]
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Κ.Α.Ε.")]
        public string ΚΑΕ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiStegasiGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiSitisiViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Υπουργού")]
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Διοικητή")]
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απόφαση Δ.Σ. ΟΑΕΔ")]
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αποφάσεις Δ.Σ.")]
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγγραφο σχολής")]
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Display(Name = "Α.Δ.Α.")]
        public string ΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Προϊστάμενος")]
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διευθυντής")]
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }

        [Display(Name = "Γενικός")]
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διοικητής")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Display(Name = "Υποδιοικητής")]
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }

        [Display(Name = "Υπογράφει ο Υποδιοικητής α.α")]
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }

        [Display(Name = "Ορθή επανάληψη")]
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία ορθού")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Μηνιαίο επίδομα στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Ημερήσιο επίδομα σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος στέγασης")]
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Λεκτικό επιδόματος σίτισης")]
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

        [Display(Name = "Λεκτικό πλήθους")]
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Κ.Α.Ε.")]
        public string ΚΑΕ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiSitisiGridViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Διαχειριστής")]
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }

        [Display(Name = "Τύπος σχολής")]
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολική μονάδα")]
        public Nullable<int> ΣΧΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος εγγράφου")]
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    }

    public class ApofasiRegistryViewModel
    {
        public int ID { get; set; }
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Είδος απόφασης")]
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [Display(Name = "Σχολική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Περιφερειακή Δ/νση")]
        public string PERIFERIAKI_TEXT { get; set; }

        [Display(Name = "Διαχειριστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Είδος εγγράφου")]
        public string ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία σύνταξης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία απόφασης")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Πρωτόκολλο")]
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη στέγασης")]
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2} €")]
        [Display(Name = "Συνολική δαπάνη σίτισης")]
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }

        [Display(Name = "Πλήθος μαθητών")]
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }

    }

    public class ApofasiParameters
    {
        public int apofasiId { get; set; }
        public string apofasiType { get; set; }
        public string schoolyear { get; set; }
        public string school { get; set; }
    }

}