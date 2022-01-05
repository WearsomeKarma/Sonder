
namespace Rogue_Like
{
    public class Debug_Generator :
        Level_Generator
    {
        public Debug_Generator()
        {
            Declare__Hierarchy()
                .Associate<Debug_Partitioner>()
                .Associate<Debug_Hallway_Plotter>()
                .Associate<Debug_Random_Partitioner>();
        }
    }
}
