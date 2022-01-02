
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Get_Tile :
        Streamline_Argument
    {
        public Tile_Handle Get_Tile__TILE_HANDLE { get; }
        public Tile? Get_Tile__Tile { get; internal set; }

        public SA__Get_Tile(Tile_Handle tile_handle)
        {
            Get_Tile__TILE_HANDLE = tile_handle;
        }
    }
}
