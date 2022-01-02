
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Room
    {
        public Rect_Prism Room__BOUNDED_SPACE { get; }
        public Rect_Prism Room__USABLE_SPACE { get; }
        private IEnumerable<Integer_Vector_3> _Room__EXITS { get; }
        public IEnumerable<Integer_Vector_3> Room__EXITS
            => new List<Integer_Vector_3>(_Room__EXITS);

        public int Room__ROOM_TYPE { get; }

        public bool Room__Hallway_Inaccessible { get; protected set; }

        internal Room
        (
            int room_type,
            Rect_Prism bound_space, 
            Rect_Prism usable_space,
            IEnumerable<Integer_Vector_3> exits
        )
        {
            Room__BOUNDED_SPACE =
                bound_space;

            Room__USABLE_SPACE  =
                usable_space;

            _Room__EXITS =
                new List<Integer_Vector_3>(exits);

            Room__ROOM_TYPE = room_type;
        }
    }
}
