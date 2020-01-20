using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vaultlife.Models
{
    public class DisplayNames
    {
    }

    public partial class Member
    {
        [NotMapped]
        public string DisplayName
        {
            get { return String.Format("{0} {1} ({2})", this.FirstName, this.LastName, this.EmailAddress); }

        }
    }
}