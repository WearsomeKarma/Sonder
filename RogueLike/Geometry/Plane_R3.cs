
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public struct Plane_R3
    {
        public Integer_Vector_3 Plane__MIN { get; }
        public Integer_Vector_3 Plane__MAX { get; }

        public Plane_Type Plane__PLANE_TYPE { get; }

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
        public int Plane__DELTA_X
            => Plane__MAX_X - Plane__MIN_X;

        public int Plane__MIN_Y
            => Plane__MIN.Y;
        public int Plane__MAX_Y
            => Plane__MAX.Y;
        public int Plane__DELTA_Y
            => Plane__MAX_Y - Plane__MIN_Y;

        public int Plane__MIN_Z
            => Plane__MIN.Z;
        public int Plane__MAX_Z
            => Plane__MAX.Z;
        public int Plane__DELTA_Z
            => Plane__MAX_Z - Plane__MIN_Z;

        public Plane_R3
        (
            Integer_Vector_3 min,
            Integer_Vector_3 max,
            Plane_Type axis_type
        )
        {
            Plane__MIN = min;
            Plane__MAX = max;

            Plane__PLANE_TYPE = axis_type;
        }

        public bool Contains__Point__Plane_3(Integer_Vector_2 point)
        {
            switch(Plane__PLANE_TYPE)
            {
                default:
                case Plane_Type.XY:
                    return Contains__Point__Plane_3(new Integer_Vector_3(point.X, point.Y, Plane__MIN_Z));
                case Plane_Type.XZ:
                    return Contains__Point__Plane_3(new Integer_Vector_3(point.X, Plane__MIN_Y, point.Y));
                case Plane_Type.YZ:
                    return Contains__Point__Plane_3(new Integer_Vector_3(Plane__MIN_X, point.X, point.Y));
            }
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

        public IEnumerable<Integer_Vector_3> Get__Vertices__Plane_R3()
        {
            switch(Plane__PLANE_TYPE)
            {
                default:
                case Plane_Type.XY:
                    yield return Plane__MIN;
                    yield return Plane__MIN_X__MAX_Y_Z;
                    yield return Plane__MAX;
                    yield return Plane__MIN_Y__MAX_X_Z;
                    break;
                case Plane_Type.XZ:
                    yield return Plane__MIN;
                    yield return Plane__MIN_X__MAX_Y_Z;
                    yield return Plane__MAX;
                    yield return Plane__MIN_Z__MAX_X_Y;
                    break;
                case Plane_Type.YZ:
                    yield return Plane__MIN;
                    yield return Plane__MIN_Y__MAX_X_Z;
                    yield return Plane__MAX;
                    yield return Plane__MIN_Z__MAX_X_Y;
                    break;
            }
        }

        public IEnumerable<Integer_Vector_3> Get__Positions__Plane_R3()
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

        public static void Split
        (
            Plane_R3 plane,
            Integer_Vector_3 point,
            Axis_Type split_type,
            out Plane_R3 left_plane,
            out Plane_R3 right_plane
        )
        {
            int 
                left_plane__max_x = plane.Plane__MAX_X,
                left_plane__max_y = plane.Plane__MAX_Y,
                left_plane__max_z = plane.Plane__MAX_Z;
            int 
                right_plane__min_x = plane.Plane__MIN_X,
                right_plane__min_y = plane.Plane__MIN_Y,
                right_plane__min_z = plane.Plane__MIN_Z;

            switch(plane.Plane__PLANE_TYPE)
            {
                default:
                case Plane_Type.XY:
                    if (split_type == Axis_Type.X)
                    {
                        left_plane__max_x = point.X;
                        right_plane__min_x = point.X;
                        break;
                    }

                    left_plane__max_y = point.Y;
                    right_plane__min_y = point.Y;
                    break;

                case Plane_Type.XZ:
                    if (split_type == Axis_Type.X)
                    {
                        left_plane__max_x = point.X;
                        right_plane__min_x = point.X;
                        break;
                    }

                    left_plane__max_z = point.Z;
                    right_plane__min_z = point.Z;
                    break;

                case Plane_Type.YZ:
                    if (split_type == Axis_Type.Y)
                    {
                        left_plane__max_y = point.Y;
                        right_plane__min_y = point.Y;
                        break;
                    }

                    left_plane__max_z = point.Z;
                    right_plane__min_z = point.Z;
                    break;
            }

            Integer_Vector_3 left_plane__max =
                new Integer_Vector_3(left_plane__max_x, left_plane__max_y, left_plane__max_z);

            Integer_Vector_3 right_plane__min =
                new Integer_Vector_3(right_plane__min_x, right_plane__min_y, right_plane__min_z);

            left_plane =
                new Plane_R3(plane.Plane__MIN, left_plane__max, plane.Plane__PLANE_TYPE);
            right_plane =
                new Plane_R3(right_plane__min, plane.Plane__MAX, plane.Plane__PLANE_TYPE);
        }
    }
}
