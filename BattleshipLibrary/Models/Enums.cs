using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Models
{
    // 0 = empty, 1 = ship, 2 = hit, 3 = miss, 4 = sunk
    public enum GridSpotStatus
    {
        Empty,
        Ship,
        Hit,
        Miss,
        Sunk
    }
}
