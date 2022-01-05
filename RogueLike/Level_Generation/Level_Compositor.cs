
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public abstract class Level_Compositor :
        Xerxes_Object<Level_Compositor>
    {
        public Level_Compositor()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Level_Compositing>
                (Private_Construct__Level__Level_Constructor)
                .Upstream.Extending<SA__Set_Tile>();
        }

        protected abstract void Handle_Construct__Rooms__Level_Constructor
        (IEnumerable<Rect_Prism> rooms);

        protected abstract void Handle_Construct__Hallways__Level_Constructor
        (IEnumerable<Integer_Vector_3> points);

        protected abstract Tile Handle_Merge__Tiles__Level_Constructor
        (Room room, Tile setting, Tile existing, Integer_Vector_3 position);

        public void Private_Construct__Level__Level_Constructor(SA__Level_Compositing e)
        {
        }
    }
}
