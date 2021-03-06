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
    
    public partial class Contract
    {
        public Contract()
        {
            this.GameTypeOwneds = new HashSet<GameTypeOwned>();
            this.MemberAcquisitionCampaigns = new HashSet<MemberAcquisitionCampaign>();
            this.Products = new HashSet<Product>();
            this.Territories = new HashSet<Territory>();
        }
    
        public int ContractID { get; set; }
        public string ContractCode { get; set; }
        public string ContractDetail { get; set; }
        public System.DateTime DateInserted { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string USR { get; set; }
    
        public virtual ICollection<GameTypeOwned> GameTypeOwneds { get; set; }
        public virtual ICollection<MemberAcquisitionCampaign> MemberAcquisitionCampaigns { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
