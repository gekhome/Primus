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
    
    public partial class ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2()
        {
            this.ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 = new HashSet<ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2>();
        }
    
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
        public Nullable<int> ΣΧΟΛΗ { get; set; }
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }
        public Nullable<int> ΤΑΞΗ { get; set; }
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΙΤΗΣΗ { get; set; }
        public Nullable<System.DateTime> ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ { get; set; }
        public Nullable<decimal> ΜΙΣΘΩΤΗΡΙΟ_ΠΟΣΟ { get; set; }
        public Nullable<int> ΟΙΚΟΓΕΝΕΙΑ_ΤΕΚΝΑ { get; set; }
        public Nullable<decimal> ΟΙΚΟΓΕΝΕΙΑ_ΕΙΣΟΔΗΜΑ { get; set; }
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }
        public string ΚΟΙΝΩΝΙΚΗ_ΟΜΑΔΑ { get; set; }
        public Nullable<int> ΕΠΙΔΟΜΑ_ΕΙΔΟΣ { get; set; }
        public Nullable<decimal> ΣΤΕΓΑΣΗ_ΠΟΣΟ { get; set; }
        public Nullable<decimal> ΣΙΤΙΣΗ_ΠΟΣΟ { get; set; }
        public bool ΣΥΝΕΧΕΙΑ { get; set; }
        public Nullable<decimal> ΔΙΑΜΟΝΗ_ΑΠΟΣΤΑΣΗ { get; set; }
        public Nullable<int> ΑΞΙΟΛΟΓΗΣΗ { get; set; }
        public Nullable<int> ΑΠΟΡΡΙΨΗ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2> ΜΑΘΗΤΗΣ_ΚΟΙΝΩΝΙΚΑ2 { get; set; }
        public virtual ΜΑΘΗΤΗΣ ΜΑΘΗΤΗΣ { get; set; }
    }
}
