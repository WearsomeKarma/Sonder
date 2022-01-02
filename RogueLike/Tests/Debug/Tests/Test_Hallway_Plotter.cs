
using Xerxes_Engine;

namespace Rogue_Like
{
    public class Test_Hallway_Plotter :
        Xerxes_Test_Suite<Level_Hallway_Plotter>
    {
        public static void Main(string[] args)
        {
            if (args.Length < 0)
                return;

            Log.Initalize__Log(new Log_Arguments());

            int size_x = int.Parse(args[0]);
            int size_y = int.Parse(args[1]);
            int size_z = int.Parse(args[2]);

            Rect_Prism space = new Rect_Prism(size_x, size_y, size_z);

            SA__Level_Partitioning e_partitioning =
                Test_Partitioner.Test__Level_Partitioning(space);

            SA__Level_Hallway_Plotting e_plotting =
                new SA__Level_Hallway_Plotting(e_partitioning);

            Test_Hallway_Plotter test = new
                Test_Hallway_Plotter();

            test.Test<Debug_Hallway_Plotter, SA__Level_Hallway_Plotting>(e_plotting);
        }
    }
}
