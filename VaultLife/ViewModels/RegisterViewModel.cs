using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class RegisterViewModel
    {
        public Models.Member member { get; set; }
        public Models.AspNetUser AspNetUser {get; set; }    
        
    }
}