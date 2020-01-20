using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class GameViewModel
    {
       
       public Models.Game Game { get; set; }

        public IEnumerable<ProductInGame> ProductInGames { get; set; }

        public IEnumerable<GameRule> GameRules { get; set; }

    }
}