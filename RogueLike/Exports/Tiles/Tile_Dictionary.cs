
using Xerxes_Engine;

namespace Rogue_Like
{
    internal sealed class Tile_Dictionary :
        Distinct_Handle_Dictionary<Tile_Handle, Tile>
    {
        public Tile_Handle Add__Tile__Tile_Dictionary(string alias, Tile t)
        {
            Tile_Handle handle = Protected_Declare__Element__Distinct_Handle_Dictionary(alias, t);

            return handle;
        }

        public Tile Get__Tile__Tile_Dictionary(Tile_Handle tile_handle)
        {
            if (tile_handle == null)
            {
                Log.Write__Error__Log("Tile handle is null!", this);
                return new Tile();
            }
            return Protected_Get__Element__Distinct_Handle_Dictionary(tile_handle);
        }

        protected override Tile_Handle Handle_Get__New_Handle__Distinct_Handle_Dictionary
        (string internalStringHandle)
        {
            return new Tile_Handle(internalStringHandle, this);
        }
    }
}
