using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class LoginViewModel
    {
        [Required]
      
        [Display(Name = "User name")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    
    }
}