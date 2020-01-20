using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class GameRuleViewModel
    {
        private VaultLifeApplicationEntities db = new VaultLifeApplicationEntities();


        public Models.Game Game { get; set; }
        public IEnumerable<Models.GameRule> GameRules { get; set; }

    }
}