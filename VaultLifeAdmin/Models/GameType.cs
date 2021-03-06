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
    
    public partial class GameType
    {
        public GameType()
        {
            this.GameTypeOwneds = new HashSet<GameTypeOwned>();
            this.GameTemplates = new HashSet<GameTemplate>();
            this.Games = new HashSet<Game>();
        }
    
        public int GameTypeID { get; set; }
        public string GameTypeCode { get; set; }
        public string GameTypeName { get; set; }
        public string GameTypeDescription { get; set; }
        public System.DateTime DateInserted { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public string USR { get; set; }
    
        public virtual ICollection<GameTypeOwned> GameTypeOwneds { get; set; }
        public virtual ICollection<GameTemplate> GameTemplates { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
