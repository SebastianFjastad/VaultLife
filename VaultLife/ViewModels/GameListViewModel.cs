using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaultlife.ViewModels
{
    public class GameListViewModel
    {

        public GameListViewModel(int gameID, int imageID)
        {
                this.gameID = gameID;
                this.imageID = imageID;
            }

        public int gameID { get; set; }
        public int imageID { get; set; }

        }



}