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
    
    public partial class ΣΥΣ_ΔΗΜΟΣ
    {
        public int DIMOS_ID { get; set; }
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
    
        public virtual ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΗ_ΕΝΟΤΗΤΑ { get; set; }
    }
}
