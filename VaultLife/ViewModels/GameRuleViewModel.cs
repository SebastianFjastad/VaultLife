using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class GameRuleViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();


        public Models.Game Game { get; set; }
        public IEnumerable<Models.GameRule> GameRules { get; set; }

    }
}