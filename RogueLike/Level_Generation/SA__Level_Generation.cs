using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Level_Generation :
        Streamline_Argument
    {
        public int Generate_Level__SIZE_X { get; }
        public int Generate_Level__SIZE_Y { get; }
        public int Generate_Level__SIZE_Z { get; }

        public Rect_Prism Generate_Level__SPACE { get; }

        public int Generate_Level__ROOM_COUNT { get; }

        internal Level Generate_Level__Level__Internal { get; set; }

        internal SA__Level_Generation
        (
            int size_x, int size_y, int size_z,
            int roomCount
        )
        {
            Generate_Level__SIZE_X = size_x;
            Generate_Level__SIZE_Y= size_y;
            Generate_Level__SIZE_Z = size_z;
            
            Generate_Level__SPACE = 
                new Rect_Prism(size_x, size_y, size_z);

            Generate_Level__ROOM_COUNT = roomCount;
        }

        internal SA__Level_Generation(SA__Level_Generation e)
        : this
        (
            e.Generate_Level__SIZE_X,
            e.Generate_Level__SIZE_Y,
            e.Generate_Level__SIZE_Z,
            e.Generate_Level__ROOM_COUNT
        )
        {

        }
    }
}
