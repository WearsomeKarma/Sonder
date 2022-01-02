
using Xerxes_Engine;

namespace Rogue_Like
{
    public class Tile_Handle :
    Distinct_Handle
    {
        internal Tile_Handle
        (
            string internalStringHandle, 
            object source
        ) 
        : base
        (
            internalStringHandle, 
            source
        )
        {
        }
    }
}
