//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vaultlife.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Imagedetail
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageContent { get; set; }
        public int ProductId { get; set; }
        public string ContentType { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
