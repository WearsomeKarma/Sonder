
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Level_Partitioning :
        Streamline_Argument
    {
        public int Level_Partitioning__ROOM_COUNT { get; }
        public KDTree_R3 Level_Partitioning__KDTREE { get; }
        public Rect_Prism Level_Partitioning__SPACE
            => Level_Partitioning__KDTREE.KDTree__SPACE;

        internal SA__Level_Partitioning(Rect_Prism space, int room_count)
        {
            Level_Partitioning__KDTREE = new KDTree_R3(space);
            Level_Partitioning__ROOM_COUNT = room_count;
        }
    }
}
