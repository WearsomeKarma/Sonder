
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Debug_Partitioner :
        Level_Partitioner
    {
        protected override void Handle_Partitioning__Level_Partitioner
        (SA__Level_Partitioning e)
        {
            //TODO: Make test
            
            int one_forth__x = e.Level_Partitioning__SPACE.Rect_Prism__MAX_X/4;
            int one_forth__y = e.Level_Partitioning__SPACE.Rect_Prism__MAX_Y/4;
            int one_forth__z = e.Level_Partitioning__SPACE.Rect_Prism__MAX_Z/4;

            Integer_Vector_3 point =
                new Integer_Vector_3(one_forth__x, one_forth__y, one_forth__z);

            e.Level_Partitioning__KDTREE.Partition__KDTree(point);
            e.Level_Partitioning__KDTREE.Partition__KDTree(2*point);
            e.Level_Partitioning__KDTREE.Partition__KDTree(3*point);

            foreach(Rect_Prism space in e.Level_Partitioning__KDTREE.Get__Partitions__KDTree())
                Xerxes_Engine.Log.Write__Info__Log($"Partition: {space.ToString()}", this);
        }
    }
}
