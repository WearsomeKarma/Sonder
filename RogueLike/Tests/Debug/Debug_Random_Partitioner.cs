
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class

        Debug_Random_Partitioner :
        Level_Partitioner
    {
        private System.Random Debug_Random_Partitioner__RANDOM { get; }

        public Debug_Random_Partitioner()
        {
            Debug_Random_Partitioner__RANDOM = new System.Random(0);
        }

        protected override void Handle_Partitioning__Level_Partitioner(SA__Level_Partitioning e)
        {
            int partition_count = 1;

            /*
            Xerxes_Engine.Log.Write__Info__Log($"Room count:{e.Level_Partitioning__ROOM_COUNT}.", this);

            for(int r=0;r<e.Level_Partitioning__ROOM_COUNT;r++)
            {
                partition_count = 
                    e.Level_Partitioning__KDTREE.KDTree__Partition_Count;

                List<Rect_Prism> spaces = 
                    new List<Rect_Prism>(e.Level_Partitioning__KDTREE.Get__Partitions__KDTree());

                int focus = Debug_Random_Partitioner__RANDOM.Next(spaces.Count);

                Rect_Prism space = spaces[focus];

                int x_range = space.Rect_Prism__MAX_X - space.Rect_Prism__MIN_X;
                int y_range = space.Rect_Prism__MAX_Y - space.Rect_Prism__MIN_Y;
                int z_range = space.Rect_Prism__MAX_Z - space.Rect_Prism__MIN_Z;

                int rand_x = Debug_Random_Partitioner__RANDOM.Next(x_range) + space.Rect_Prism__MIN_X;
                int rand_y = Debug_Random_Partitioner__RANDOM.Next(y_range) + space.Rect_Prism__MIN_Y;
                int rand_z = Debug_Random_Partitioner__RANDOM.Next(z_range) + space.Rect_Prism__MIN_Z;

                Integer_Vector_3 partition_point =
                    new Integer_Vector_3(rand_x, rand_y, rand_z);

                Xerxes_Engine.Log.Write__Info__Log($"Performing partition on:{partition_point}.", this);

                e.Level_Partitioning__KDTREE.Partition__KDTree(partition_point);
            }
            */

            throw new System.NotImplementedException();
        }
    }
}
