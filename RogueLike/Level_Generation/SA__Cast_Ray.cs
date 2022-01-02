
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Cast_Ray :
        Streamline_Argument
    {
        public Ray Cast_Ray__RAY { get; }
        public Tile? Cast_Ray__Collision { get; internal set; }
        public Integer_Vector_3 Cast_Ray__Collision_Position { get; internal set; }
        internal IEnumerable<Entity> Cast_Ray__Covered_Entities__Internal { get; set; }

        public SA__Cast_Ray(Ray ray)
        {
            Cast_Ray__RAY = ray;
        }
    }
}
