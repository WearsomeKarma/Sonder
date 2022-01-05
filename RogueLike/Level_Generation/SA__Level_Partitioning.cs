
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Level_Partitioning :
        Streamline_Argument
    {
        private KDTree_R3 Level_Partitioning__KDTREE { get; }
        public Rect_Prism Level_Partitioning__SPACE
            => Level_Partitioning__KDTREE.KDTree__SPACE;
        public IEnumerable<Rect_Prism> Level_Partitioning__PARTITIONS
            => Level_Partitioning__KDTREE.Get__Partitions__KDTree();
        public bool Partition__Level_Partitioning(Integer_Vector_3 position)
            => Level_Partitioning__KDTREE.Partition__KDTree(position);

        private Dictionary<Rect_Prism, Room> Level_Partitioning__ROOM_LOOKUP { get; }
        public Room Get__Room__Level_Partitioning(Integer_Vector_3 position)
        {
            Rect_Prism? nullable_partition = Level_Partitioning__KDTREE[position];
            if (nullable_partition == null)
                return null;

            Rect_Prism partition = (Rect_Prism)nullable_partition;

            if (Level_Partitioning__ROOM_LOOKUP.ContainsKey(partition))
                return Level_Partitioning__ROOM_LOOKUP[partition];

            return null;
        }
        public bool Declare__Room__Level_Partitioning(Integer_Vector_3 position, Room room)
        {
            bool invalid_position =
                KDTree_R3.Assert__Invalid_Position(Level_Partitioning__KDTREE, position, this);

            if (invalid_position)
                return false;

            Rect_Prism? nullable_partition =
                Level_Partitioning__KDTREE[position];

            if (nullable_partition == null)
                return false; //TODO: Log.

            Rect_Prism partition =
                (Rect_Prism)nullable_partition;

            if (Level_Partitioning__ROOM_LOOKUP.ContainsKey(partition))
                return false; //TODO: Log.

            Level_Partitioning__ROOM_LOOKUP.Add(partition, room);

            return true;
        }

        internal SA__Level_Partitioning
        (
            KDTree_R3 tree,
            Dictionary<Rect_Prism, Room> room_lookup
        )
        {
            Level_Partitioning__KDTREE = tree;
        }
    }
}
