using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VaultLifeAdmin.Models;

namespace VaultLifeAdmin.ViewModels
{
    public class GameAdminViewModel
    {

        public Models.Game Game { get; set; }

        public Models.ProductInGame ProductInGames { get; set; }

        public Models.ProductLocation ProductLocations { get; set; }

        public IEnumerable<GameRule> GameRules { get; set; }

        

    }
}