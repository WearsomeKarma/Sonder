
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Level_Compositing :
        Streamline_Argument
    {
        private Level Level_Compositing__LEVEL { get; }
        private IEnumerable<Room> Level_Compositing__ROOMS { get; }
        private IEnumerable<Room> Level_Compositing__HALLWAYS { get; }

        internal SA__Level_Compositing
        (
            Level level,
            IEnumerable<Room> rooms,
            IEnumerable<Room> hallways
        )
        {
            Level_Compositing__LEVEL = level;
            Level_Compositing__ROOMS = rooms;
            Level_Compositing__HALLWAYS = hallways;
        }

        public void Set__Tile__Level_Compositing(Integer_Vector_3 position, Tile tile)
            => Level_Compositing__LEVEL[position] = tile;
    }
}
