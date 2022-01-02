
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Level_Hallway_Plotting :
        Streamline_Argument
    {
        public KDTree_R3 Level_Hallway_Plotting__KDTREE { get; }

        internal SA__Level_Hallway_Plotting(SA__Level_Partitioning e)
        {
            Level_Hallway_Plotting__KDTREE =
                new KDTree_R3(e.Level_Partitioning__SPACE);

            foreach(KDTree_R3.KDTree_R3_Node n in e.Level_Partitioning__KDTREE.Traverse__In_Order__KDTree())
                Level_Hallway_Plotting__KDTREE.Partition__KDTree(n.Node__KEY);
        }
    }
}
