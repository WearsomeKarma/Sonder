
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Debug_Compositor :
        Level_Compositor
    {
        public Debug_Compositor()
        {
            Declare__Streams()
                .Upstream.Extending<SA__Get_Tile>();
        }

        protected override void Handle_Construct__Hallways__Level_Constructor(IEnumerable<Integer_Vector_3> points)
        {
            IEnumerator<Integer_Vector_3> enumerator = points.GetEnumerator();
            
            bool is_empty = !enumerator.MoveNext();
            
            if (is_empty)
                return;

            Integer_Vector_3 previous = enumerator.Current; 
    
            while(enumerator.MoveNext())
            {
                Private_Draw__Hallway__Debug_Constructor(previous, enumerator.Current);

                previous = enumerator.Current;
            }
        }

        private void Private_Draw__Hallway__Debug_Constructor(Integer_Vector_3 previous, Integer_Vector_3 current)
        {
            int delta_x = current.X - previous.X;
            int delta_y = current.Y - previous.Y;
    
            int length_x = System.Math.Abs(delta_x);
            int length_y = System.Math.Abs(delta_y);

            int sign_dx = System.Math.Sign(delta_x);
            int sign_dy = System.Math.Sign(delta_y);

            //for debug we just draw straight lines.

            for(int x = 0; x < length_x; x++)
            {
                int dx = (sign_dx * x) + previous.X;
                
                Integer_Vector_2 vec = new Integer_Vector_2(dx, previous.Y);
            }

            for(int y = 0; y < length_y; y++)
            {
                int dy = (sign_dy * y) + previous.Y;
                
                Integer_Vector_2 vec = new Integer_Vector_2(previous.X, dy);
            }
        }

        protected override void Handle_Construct__Rooms__Level_Constructor(IEnumerable<Rect_Prism> rooms)
        {
            Tile_Handle debug_tiles =
                Core_Tile_Handles.TILE__DEBUG;

            SA__Get_Tile e_get_tile =
                new SA__Get_Tile(debug_tiles);

            Invoke__Ascending(e_get_tile);

            if (e_get_tile.Get_Tile__Tile == null)
                return;

            Tile tile = (Tile)e_get_tile.Get_Tile__Tile;

            Sprite s = tile.Tile__SPRITE;

            foreach(Rect_Prism room in rooms)
            {
                Rect_Prism rotated = new Rect_Prism(room.Rect_Prism__MIN, room.Rect_Prism__MAX, Plane_Type.XZ);
                rotated = Rect_Prism.Get__Internal(rotated);

                Plane_R3 floor = rotated.Rect_Prism__MIN_PLANE;

                Xerxes_Engine.Log.Write__Info__Log($"Compositing rotated floor:{floor}. on axis: {rotated.Rect_Prism__PLANE_TYPE}", this);

                foreach(Integer_Vector_3 vec in floor.Get__Positions__Plane_R3())
                {
                    throw new System.NotImplementedException();
                    //Set__Tile__Level_Constructor(vec, tile);
                }
            }
        }

        protected override Tile Handle_Merge__Tiles__Level_Constructor(Room room, Tile setting, Tile existing, Integer_Vector_3 position)
        {
            return setting;
        }
    }
}
