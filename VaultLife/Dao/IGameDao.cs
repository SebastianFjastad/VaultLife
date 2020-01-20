using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaultlife.Models;

namespace Vaultlife.Dao
{
    interface IGameDao
    {
        IEnumerable<Game> findGlobalGames(int? MemberId);
    }
}
