using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaultlife.Models;

namespace Vaultlife.ViewModels
{
    public class GameViewModel
    {
       
       public Models.Game Game { get; set; }

        public IEnumerable<ProductInGame> ProductInGames { get; set; }

        public IEnumerable<GameRule> GameRules { get; set; }



    }
}