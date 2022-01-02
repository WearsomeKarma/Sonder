
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Draw_Level :
        Streamline_Argument
    {
        private Level Draw_Level__LEVEL__Reference { get; }

        internal SA__Render Draw_Level__RENDER { get; }
        
        public Integer_Vector_3 Draw_Level__FOCUS_SOURCE { get; }
        public int Draw_Level__FOCUS_RADIUS { get; }

        internal SA__Draw_Level
        (
            SA__Render e,
            Level level,
            Integer_Vector_3 focus_source,
            int focus_radius
        )
        {
            Draw_Level__RENDER = e;

            Draw_Level__LEVEL__Reference = level;
            Draw_Level__FOCUS_SOURCE = focus_source;
            Draw_Level__FOCUS_RADIUS = focus_radius;
        }

        public Tile? Cast__Ray__Draw_Level(Ray ray, out Integer_Vector_3 collision_point)
        {
            return Draw_Level__LEVEL__Reference.Cast__Ray__Level(ray, out collision_point);
        }
    }
}
