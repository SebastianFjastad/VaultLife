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
    
    public partial class ProductInfo
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<decimal> PriceInGame { get; set; }
        public Nullable<decimal> ReferencePrice { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string Address { get; set; }
        public string DeliveryAgentName { get; set; }
        public int GameID { get; set; }
        public string terms { get; set; }
        public string link { get; set; }
        public int ImageID { get; set; }
        public System.DateTime ExcecuteTime { get; set; }
    }
}
