
using Xerxes_Engine;

namespace Rogue_Like
{
    public class Test_Partitioner :
        Xerxes_Test_Suite<Level_Partitioner>
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
                return;

            Log.Initalize__Log(new Log_Arguments());

            int size_x = int.Parse(args[0]);
            int size_y = int.Parse(args[1]);
            int size_z = int.Parse(args[2]);

            Rect_Prism space = new Rect_Prism(size_x, size_y, size_z);

            SA__Level_Partitioning e_partitioning = 
                Test__Level_Partitioning(space);

            throw new System.NotImplementedException();
            //Log.Write__Info__Log(e_partitioning.Level_Partitioning__KDTREE.ToString(), null);
        }

        internal static SA__Level_Partitioning Test__Level_Partitioning(Rect_Prism space)
        {
            Test_Partitioner test =
                new Test_Partitioner();

            SA__Level_Partitioning e_partitioning =
                null;

            throw new System.NotImplementedException();

            test.Test<Debug_Partitioner, SA__Level_Partitioning>(e_partitioning);

            return e_partitioning;
        }
    }
}
