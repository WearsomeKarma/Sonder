
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public struct Plane_3
    {
        public Integer_Vector_3 Plane__MIN { get; }
        public Integer_Vector_3 Plane__MAX { get; }

        public int Plane__AREA__XY
            => (Plane__MAX_X - Plane__MIN_X) * (Plane__MAX_Y - Plane__MIN_Y);
        public int Plane__AREA__XZ
            => (Plane__MAX_X - Plane__MIN_X) * (Plane__MAX_Z - Plane__MIN_Z);
        public int Plane__AREA__YZ
            => (Plane__MAX_Y - Plane__MIN_Y) * (Plane__MAX_Z - Plane__MIN_Z);

        public Integer_Vector_3 Plane__MIN_X_Y__MAX_Z
            => new Integer_Vector_3(Plane__MIN_X, Plane__MIN_Y, Plane__MAX_Z);
        public Integer_Vector_3 Plane__MIN_X__MAX_Y_Z
            => new Integer_Vector_3(Plane__MIN_X, Plane__MAX_Y, Plane__MAX_Z);

        public Integer_Vector_3 Plane__MIN_Y_Z__MAX_X
            => new Integer_Vector_3(Plane__MAX_X, Plane__MIN_Y, Plane__MIN_Z);
        public Integer_Vector_3 Plane__MIN_Y__MAX_X_Z
            => new Integer_Vector_3(Plane__MAX_X, Plane__MIN_Y, Plane__MAX_Z);

        public Integer_Vector_3 Plane__MIN_X_Z__MAX_Y
            => new Integer_Vector_3(Plane__MIN_X, Plane__MAX_Y, Plane__MIN_Z);
        public Integer_Vector_3 Plane__MIN_Z__MAX_X_Y
            => new Integer_Vector_3(Plane__MAX_X, Plane__MAX_Y, Plane__MIN_Z);

        public int Plane__MIN_X
            => Plane__MIN.X;
        public int Plane__MAX_X
            => Plane__MAX.X;

        public int Plane__MIN_Y
            => Plane__MIN.Y;
        public int Plane__MAX_Y
            => Plane__MAX.Y;

        public int Plane__MIN_Z
            => Plane__MIN.Z;
        public int Plane__MAX_Z
            => Plane__MAX.Z;

        public Plane_3
        (
            Integer_Vector_3 min,
            Integer_Vector_3 max
        )
        {
            Plane__MIN = min;
            Plane__MAX = max;
        }

        public bool Contains__Point__Plane_3(Integer_Vector_3 point)
        {
            bool bounded_x =
                point.X >= Plane__MIN.X && point.X <= Plane__MAX.X;
            bool bounded_y =
                point.Y >= Plane__MIN.Y && point.Y <= Plane__MAX.Y;
            bool bounded_z =
                point.Z >= Plane__MIN.Z && point.Z <= Plane__MAX.Z;

            bool bounded =
                bounded_x
                &&
                bounded_y
                &&
                bounded_z;

            return bounded;
        }

        public IEnumerable<Integer_Vector_3> Get__Positions__Plane_3()
        {
            int length_x = Plane__MAX_X - Plane__MIN_X;
            int length_y = Plane__MAX_Y - Plane__MIN_Y;
            int length_z = Plane__MAX_Z - Plane__MIN_Z;

            // regardless of plane axis_type, make sure we iterate each axis at least once.
            if (length_x == 0)
                length_x = 1;
            if (length_y == 0)
                length_y = 1;
            if (length_z == 0)
                length_z = 1;

            for(int y=0;y<length_y;y++)
            {
                for(int z=0;z<length_z;z++)
                {
                    for(int x=length_x-1;x>=0;x--)
                    {
                        int vx = x + Plane__MIN_X;
                        int vy = y + Plane__MIN_Y;
                        int vz = z + Plane__MIN_Z;
                        yield return new Integer_Vector_3(vx,vy,vz);
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"plane:[{Plane__MIN} - {Plane__MAX}]";
        }
    }
}
