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
    
    public partial class ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΣΥΣ_ΣΧΟΛΙΚΕΣ_ΔΟΜΕΣ()
        {
            this.ΣΥΣ_ΣΧΟΛΕΣ = new HashSet<ΣΥΣ_ΣΧΟΛΕΣ>();
        }
    
        public int ΔΟΜΗ_ΚΩΔ { get; set; }
        public string ΔΟΜΗ_ΚΕΙΜΕΝΟ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΣΥΣ_ΣΧΟΛΕΣ> ΣΥΣ_ΣΧΟΛΕΣ { get; set; }
    }
}
