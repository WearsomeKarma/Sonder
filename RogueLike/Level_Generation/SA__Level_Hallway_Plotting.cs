
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Level_Hallway_Plotting :
        Streamline_Argument
    {
        private KDTree_R2_Lattice Level_Hallway_Plotting__STRUCTURE_LATTICE { get; }
        public bool Partition__Level_Hallway_Plotting(Integer_Vector_3 point)
            => Level_Hallway_Plotting__STRUCTURE_LATTICE.Partition__KDTree_R2_Lattice(point);
        private Dictionary<Rect_Prism, Room> Level_Hallway_Plotting__ROOM_LOOKUP { get; }
        public IEnumerable<Room> Get__Rooms__Level_Hallway_Plotting()
            => Level_Hallway_Plotting__ROOM_LOOKUP.Values;

        internal SA__Level_Hallway_Plotting
        (
            KDTree_R2_Lattice structure_lattice,
            Dictionary<Rect_Prism, Room> room_lookup
        )
        {
            Level_Hallway_Plotting__STRUCTURE_LATTICE = structure_lattice;
            Level_Hallway_Plotting__ROOM_LOOKUP = room_lookup;
        }
    }
}
