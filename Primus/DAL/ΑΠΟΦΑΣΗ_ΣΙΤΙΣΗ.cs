//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Primus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ()
        {
            this.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ = new HashSet<ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ>();
        }
    
        public int ΑΠΟΦΑΣΗ_ΚΩΔ { get; set; }
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΔΙΑΧΕΙΡΙΣΤΗΣ { get; set; }
        public Nullable<int> ΣΧΟΛΗ_ΤΥΠΟΣ { get; set; }
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public string ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ { get; set; }
        public string ΕΓΚΥΚΛΙΟΣ_ΔΙΟΙΚΗΣΗ { get; set; }
        public string ΑΠΟΦΑΣΗ_ΔΣ { get; set; }
        public string ΑΠΟΦΑΣΕΙΣ_ΔΣ { get; set; }
        public string ΣΧΟΛΗ_ΕΓΓΡΑΦΟ { get; set; }
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }
        public string ΠΡΩΤΟΚΟΛΛΟ { get; set; }
        public string ΑΔΑ { get; set; }
        public Nullable<int> ΕΓΓΡΑΦΟ_ΕΙΔΟΣ { get; set; }
        public Nullable<int> ΠΡΟΙΣΤΑΜΕΝΟΣ { get; set; }
        public Nullable<int> ΔΙΕΥΘΥΝΤΗΣ { get; set; }
        public Nullable<int> ΓΕΝΙΚΟΣ { get; set; }
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ { get; set; }
        public Nullable<int> ΑΝΤΙΠΡΟΕΔΡΟΣ { get; set; }
        public bool ΥΠΟΓΡΑΦΩΝ { get; set; }
        public bool ΣΤΟ_ΟΡΘΟ { get; set; }
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ { get; set; }
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΜΗΝΙΑΙΟ { get; set; }
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΣΥΝΟΛΟ { get; set; }
        public Nullable<decimal> ΣΙΤΙΣΗ_ΗΜΕΡΗΣΙΟ { get; set; }
        public Nullable<decimal> ΣΙΤΙΣΗ_ΣΥΝΟΛΟ { get; set; }
        public string ΣΤΕΓΑΣΗ_ΛΕΚΤΙΚΟ { get; set; }
        public string ΣΙΤΙΣΗ_ΛΕΚΤΙΚΟ { get; set; }
        public Nullable<short> ΠΛΗΘΟΣ { get; set; }
        public string ΠΛΗΘΟΣ_ΛΕΚΤΙΚΟ { get; set; }
        public string ΚΑΕ { get; set; }
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΣΥΝΤΑΞΗ { get; set; }
        public string ΑΠΟΦΑΣΗ_ΕΙΔΟΣ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ> ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ { get; set; }
    }
}
