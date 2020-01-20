using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class HomeAuthenticatedViewModel
    {
        public IEnumerable<ViewModels.MyVaultItemViewModel> TopSection { get; set; }
        public IEnumerable<ViewModels.MyVaultItemViewModel> UpcomingVaultItems { get; set; }
        public IEnumerable<ViewModels.ComingSoonViewModel> ComingSoon { get; set; }
        public List<dynamic> recentWinners { get; set; }
       
    }
}