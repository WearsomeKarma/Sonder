
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Level_Compositing :
        Streamline_Argument
    {
        private KDTree_R3 Level_Constructing__ROOMS { get; }
        private Integer_Vector_3_Graph Level_Constructing__HALLWAYS { get; }

        internal SA__Level_Compositing
        (
            SA__Level_Partitioning e_paritioning,
            SA__Level_Hallway_Plotting e_hallway_plotting
        )
        {
            Level_Constructing__ROOMS = e_paritioning.Level_Partitioning__KDTREE;
            //int v = (e_hallway_plotting.Level_Hallway_Plotting__KDTREE.KDTree__Partition_Count + 2) * 4;
            //Level_Constructing__HALLWAYS = new Integer_Vector_3_Graph(v);
        }

        public IEnumerable<Rect_Prism> Get__Rooms__Level_Constructing()
        {
            return Level_Constructing__ROOMS?.Get__Partitions__KDTree();
        }

        public IEnumerable<Integer_Vector_3> Get__Hallway_Points__Level_Constructing()
        {
            if (Level_Constructing__HALLWAYS == null)
                yield break;
            foreach(int v in Level_Constructing__HALLWAYS.Search__Breadth_First__Graph(0))
                yield return Level_Constructing__HALLWAYS[v];
        }
    }
}
