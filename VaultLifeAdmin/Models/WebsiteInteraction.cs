//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VaultLifeAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebsiteInteraction
    {
        public int WebsiteInteractionID { get; set; }
        public int MemberID { get; set; }
        public int InteractionTypeID { get; set; }
        public System.DateTime DateInserted { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string USR { get; set; }
    
        public virtual InteractionType InteractionType { get; set; }
        public virtual Member Member { get; set; }
    }
}
