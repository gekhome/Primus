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
    
    public partial class ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ()
        {
            this.ΕΤΗ_ΣΧΟΛΕΣ_ΕΙΔΙΚΟΤΗΤΕΣ = new HashSet<ΕΤΗ_ΣΧΟΛΕΣ_ΕΙΔΙΚΟΤΗΤΕΣ>();
        }
    
        public int SCHOOLYEAR_ID { get; set; }
        public string SCHOOLYEAR_TEXT { get; set; }
        public Nullable<System.DateTime> DATE_START { get; set; }
        public Nullable<System.DateTime> DATE_END { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΕΤΗ_ΣΧΟΛΕΣ_ΕΙΔΙΚΟΤΗΤΕΣ> ΕΤΗ_ΣΧΟΛΕΣ_ΕΙΔΙΚΟΤΗΤΕΣ { get; set; }
    }
}
