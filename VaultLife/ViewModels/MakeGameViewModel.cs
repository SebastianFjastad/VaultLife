using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.ViewModels
{
    public class MakeGameViewModel
    {
        public Models.Game Game { get; set; }

        public IEnumerable<Models.GameRule> GameRules { get; set; }

   //     public IEnumerable<ViewModels.ProductInGameViewModel> ProductInGameViewModels { get; set; }
    }
}