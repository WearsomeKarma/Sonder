
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Set_Tile :
        Streamline_Argument
    {
        public Tile Set_Tile__TILE { get; }
        public Integer_Vector_3 Set_Tile__POSITION { get; }

        public SA__Set_Tile(Integer_Vector_3 position, Tile tile)
        {
            Set_Tile__POSITION = position;
            Set_Tile__TILE = tile;
        }
    }
}
