using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.Models
{
    public class Transaction
    {
        public Transaction() {
        }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public string Usr { get; set; }
        public string DateUpdated { get; set; }
    }
}