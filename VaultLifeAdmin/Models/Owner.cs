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
    
    public partial class Owner
    {
        public Owner()
        {
            this.GameTypeOwneds = new HashSet<GameTypeOwned>();
            this.MemberAcquisitionCampaigns = new HashSet<MemberAcquisitionCampaign>();
            this.MemberOwneds = new HashSet<MemberOwned>();
            this.Products = new HashSet<Product>();
            this.Territories = new HashSet<Territory>();
            this.Members = new HashSet<Member>();
        }
    
        public int OwnerID { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerName { get; set; }
        public string OwnerType { get; set; }
        public string BankingDetailBank { get; set; }
        public string BankingDetailAccountNumber { get; set; }
        public string BankingDetailAccountType { get; set; }
        public string BankingDetailBranchCode { get; set; }
        public string BankingDetailBranchName { get; set; }
        public string BankingDetailDefaultReference { get; set; }
        public string EmailAddress { get; set; }
        public string ContactPerson { get; set; }
        public string TelephoneOffice { get; set; }
        public string TelephoneMobile { get; set; }
        public int AddressID { get; set; }
        public System.DateTime DateInserted { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string USR { get; set; }
        public Nullable<int> CurrencyID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<GameTypeOwned> GameTypeOwneds { get; set; }
        public virtual ICollection<MemberAcquisitionCampaign> MemberAcquisitionCampaigns { get; set; }
        public virtual ICollection<MemberOwned> MemberOwneds { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
