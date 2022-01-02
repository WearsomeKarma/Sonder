
using Xerxes_Engine;

namespace Rogue_Like
{
    public abstract class Level_Hallway_Plotter :
        Xerxes_Object<Level_Hallway_Plotter>
    {
        public Level_Hallway_Plotter()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Level_Hallway_Plotting>
                (Handle_Plotting__Hallways__Level_Hallway_Plotter);
        }

        protected abstract void Handle_Plotting__Hallways__Level_Hallway_Plotter
        (SA__Level_Hallway_Plotting e);
    }
}
