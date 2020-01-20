using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class ComingSoonViewModel
    {

        
        public List<ComingSoonGameViewModel> ComingSoonGameVM {get; set;}    
        public MemberSubscriptionType MemberSubscriptionType { get; set; }
        public ComingSoonViewModel() { ComingSoonGameVM = new List<ComingSoonGameViewModel>();}    
    }
}