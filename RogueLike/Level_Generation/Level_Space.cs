
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Level_Space
    {
        private Tile[,,] Level_Space__SPACE { get; }

        public Level_Space
        (
            int size_x, 
            int size_y, 
            int size_z,
            int tile_types = 0
        )
        {
            Level_Space__SPACE =
                new Tile[size_x, size_y, size_z];
        }

        public Tile this[Integer_Vector_3 index]
        {
            get 
            {
                return Level_Space__SPACE[index.X, index.Y, index.Z];
            }
            set
            {
                Level_Space__SPACE[index.X, index.Y, index.Z] = value;
            }
        }
    }
}
