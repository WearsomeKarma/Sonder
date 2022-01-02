using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Level
    {
        private Level_Space _Level__MAP { get; }
        public int Level__SIZE_X { get; }
        public int Level__SIZE_Y { get; }
        public int Level__SIZE_Z { get; }

        public Level
        (Rect_Prism space)
        {
            Level__SIZE_X = space.Rect_Prism__MAX_X;
            Level__SIZE_Y = space.Rect_Prism__MAX_Y;
            Level__SIZE_Z = space.Rect_Prism__MAX_Z;

            _Level__MAP = new Level_Space(Level__SIZE_X, Level__SIZE_Y, Level__SIZE_Z);
        }

        public Tile? Cast__Ray__Level(Ray ray, out Integer_Vector_3 collided_position)
        {
            Integer_Vector_3? old = null;
            foreach(Integer_Vector_3 vec in ray.Get__Positions__Ray())
            {
                if (Private_Check_If__Point_Is_Outside_Level__Level(vec.X, vec.Y, vec.Z))
                    break;
                if ((this[vec]?.Tile__Is_Unpassable) ?? false)
                {
                    collided_position = vec;
                    return this[vec];
                }
                old = vec;
            }

            collided_position = ray.Ray__SOURCE;
            return null;
        }

        public void Set__Tile__Level(Integer_Vector_3 pos, Tile t)
        {
            if(Private_Assert__Point_Is_Outside_Level__Level(pos.X, pos.Y, pos.Z))
                return;

            _Level__MAP[pos] = t;
        }

        public Tile? Get__Tile__Level(Integer_Vector_3 pos)
        {
            if(Private_Assert__Point_Is_Outside_Level__Level(pos.X,pos.Y,pos.Z))
                return null;

            return _Level__MAP[pos];
        }

        public Tile? this[Integer_Vector_3 pos]
        {
            get => Get__Tile__Level(pos);
            set
            {
                Set__Tile__Level(pos, value ?? new Tile());
            }
        }

        public Tile? this[int x, int y, int z]
        {
            get => Get__Tile__Level(new Integer_Vector_3(x,y,z));
            set => this[new Integer_Vector_3(x,y,z)] = value;
        }

        public bool Check_If__Not_Passable__Level(Integer_Vector_3 pos)
        {
            if(Private_Check_If__Point_Is_Outside_Level__Level(pos.X, pos.Y, pos.Z))
                return true;

            Tile? t = Get__Tile__Level(pos);

            return !t?.Tile__Is_Unpassable ?? true;
        }

        private bool Private_Assert__Point_Is_Outside_Level__Level(int x, int y, int z)
        {
            bool isOutside = Private_Check_If__Point_Is_Outside_Level__Level(x,y,z);
            
            if(isOutside)
            {
                string format = string.Format("Index for {0},{1},{2} was out of bounds for level.",x,y,z);
                Log.Write__Log
                (
                    new Log_Message
                    (
                        Log_Message_Type.Warning__Critical,
                        format,
                        this
                    )
                );
            }

            return isOutside;
        }

        private bool Private_Check_If__Point_Is_Outside_Level__Level(int x, int y, int z)
        {
            bool unbounded_x =
                x < 0 || x >= Level__SIZE_X;
            bool unbounded_y =
                y < 0 || y >= Level__SIZE_Y;
            bool unbounded_z =
                z < 0 || z >= Level__SIZE_Z;

            return unbounded_x || unbounded_y || unbounded_z;
        }
    }
}
