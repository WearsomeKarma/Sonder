
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public struct Rect_Prism
    {
        public Plane_R3 Rect_Prism__MIN_PLANE { get; }
        public Plane_R3 Rect_Prism__MAX_PLANE { get; }
        public Plane_Type Rect_Prism__PLANE_TYPE { get; }

        public int Rect_Prism__VOLUME
            => 
            (Rect_Prism__MAX_X - Rect_Prism__MIN_X)
            *
            (Rect_Prism__MAX_Y - Rect_Prism__MIN_Y)
            *
            (Rect_Prism__MAX_Z - Rect_Prism__MIN_Z);

        public Integer_Vector_3 Rect_Prism__MIN
            => Rect_Prism__MIN_PLANE.Plane__MIN;
        public Integer_Vector_3 Rect_Prism__MAX
            => Rect_Prism__MAX_PLANE.Plane__MAX;

        public int Rect_Prism__MIN_X
            => Rect_Prism__MIN_PLANE.Plane__MIN_X;
        public int Rect_Prism__MAX_X
            => Rect_Prism__MAX_PLANE.Plane__MAX_X;
        public int Rect_Prism__DELTA_X
            => Rect_Prism__MAX_X - Rect_Prism__MIN_X;

        public int Rect_Prism__MIN_Y
            => Rect_Prism__MIN_PLANE.Plane__MIN_Y;
        public int Rect_Prism__MAX_Y
            => Rect_Prism__MAX_PLANE.Plane__MAX_Y;
        public int Rect_Prism__DELTA_Y
            => Rect_Prism__MAX_Y - Rect_Prism__MIN_Y;

        public int Rect_Prism__MIN_Z
            => Rect_Prism__MIN_PLANE.Plane__MIN_Z;
        public int Rect_Prism__MAX_Z
            => Rect_Prism__MAX_PLANE.Plane__MAX_Z;
        public int Rect_Prism__DELTA_Z
            => Rect_Prism__MAX_Z - Rect_Prism__MIN_Z;

        public Rect_Prism(int size_x, int size_y, int size_z)
        : this(new Integer_Vector_3(), new Integer_Vector_3(size_x, size_y, size_z))
        {}

        public Rect_Prism(Integer_Vector_3 space)
        : this(new Integer_Vector_3(), space)
        {
        }

        public Rect_Prism
        (
            Integer_Vector_3 min,
            Integer_Vector_3 max,
            Plane_Type axis_type = Plane_Type.XY
        )
        {
            Rect_Prism__PLANE_TYPE =
                axis_type;

            int min_x = min.X;
            int max_x = max.X;

            int min_y = min.Y;
            int max_y = max.Y;

            int min_z = min.Z;
            int max_z = max.Z;

            Integer_Vector_3 min_plane_max;
            Integer_Vector_3 max_plane_min;

            switch(axis_type)
            {
                default:
                case Plane_Type.XY:
                    min_plane_max =
                        new Integer_Vector_3(max_x, max_y, min_z);
                    max_plane_min =
                        new Integer_Vector_3(min_x, min_y, max_z);

                    Rect_Prism__MIN_PLANE =
                        new Plane_R3(min, min_plane_max, Rect_Prism__PLANE_TYPE);

                    Rect_Prism__MAX_PLANE =
                        new Plane_R3(max_plane_min, max, Rect_Prism__PLANE_TYPE);

                    break;
                case Plane_Type.XZ:
                    min_plane_max =
                        new Integer_Vector_3(max_x, min_y, max_z);
                    max_plane_min =
                        new Integer_Vector_3(min_x, max_y, min_z);

                    Rect_Prism__MIN_PLANE =
                        new Plane_R3(min, min_plane_max, Rect_Prism__PLANE_TYPE);

                    Rect_Prism__MAX_PLANE =
                        new Plane_R3(max_plane_min, max, Rect_Prism__PLANE_TYPE);

                    break;
                case Plane_Type.YZ:
                    min_plane_max =
                        new Integer_Vector_3(min_x, max_y, max_z);
                    max_plane_min =
                        new Integer_Vector_3(max_x, min_y, min_z);

                    Rect_Prism__MIN_PLANE =
                        new Plane_R3(min, min_plane_max, Rect_Prism__PLANE_TYPE);

                    Rect_Prism__MAX_PLANE =
                        new Plane_R3(max_plane_min, max, Rect_Prism__PLANE_TYPE);

                    break;
            }
        }

        public bool Contains__Point__Rect_Prism(Integer_Vector_3 point)
        {
            Integer_Vector_3 min = Rect_Prism__MIN_PLANE.Plane__MIN;
            Integer_Vector_3 max = Rect_Prism__MAX_PLANE.Plane__MAX;

            bool bounded_x =
                point.X >= min.X && point.X <= max.X;
            bool bounded_y =
                point.Y >= min.Y && point.Y <= max.Y;
            bool bounded_z =
                point.Z >= min.Z && point.Z <= max.Z;

            bool bounded =
                bounded_x
                &&
                bounded_y
                &&
                bounded_z;

            return bounded;
        }

        public IEnumerable<Integer_Vector_3> Get__Positions__Rect_Prism()
        {
            for(int z=Rect_Prism__MIN_Z;z<=Rect_Prism__MAX_Z;z++)
            {
                for(int y=Rect_Prism__MIN_Y;y<=Rect_Prism__MAX_Y;y++)
                {
                    for(int x=Rect_Prism__MAX_X;x>=Rect_Prism__MIN_X;x--)
                    {
                        Integer_Vector_3 vec =
                            new Integer_Vector_3(x,y,z);

                        yield return vec;
                    }
                }
            }
        }

        public IEnumerable<Integer_Vector_3> Get__Vertices__Rect_Prism()
        {
            yield return Rect_Prism__MIN;

            switch(Rect_Prism__PLANE_TYPE)
            {
                default:
                case Plane_Type.XY: //Z exclusive.

                    //<min_plane>
                    yield return 
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Y__MAX_X_Z;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_X__MAX_Y_Z;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MAX;
                    //</min_plane>

                    //<max_plane>
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN;
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN_Y__MAX_X_Z;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_X__MAX_Y_Z;
                    //</max_plane>

                    break;
                case Plane_Type.XZ: //Y exclusive

                    //<min_plane>
                    yield return 
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Z__MAX_X_Y;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_X__MAX_Y_Z;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MAX;
                    //</min_plane>

                    //<max_plane>
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN;
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN_Z__MAX_X_Y;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Z__MAX_X_Y;
                    //</max_plane>

                    break;
                case Plane_Type.YZ: //X exclusive

                    //<min_plane>
                    yield return 
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Z__MAX_X_Y;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Y__MAX_X_Z;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MAX;
                    //</min_plane>

                    //<max_plane>
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN;
                    yield return
                        Rect_Prism__MAX_PLANE
                        .Plane__MIN_Z__MAX_X_Y;
                    yield return
                        Rect_Prism__MIN_PLANE
                        .Plane__MIN_Y__MAX_X_Z;
                    //</max_plane>

                    break;
            }

            yield return Rect_Prism__MAX;
        }

        public override string ToString()
        {
            return $"rect_prism:[{Rect_Prism__MIN_PLANE} - {Rect_Prism__MAX_PLANE}, {Rect_Prism__PLANE_TYPE}]";
        }

        public static Plane_R3 Get__Closest_Plane
        (
            Rect_Prism space, 
            Integer_Vector_3 point,
            bool tie__min_or_max = true
        )
        {
            int compare_x =
                point.X 
                -
                (space.Rect_Prism__DELTA_X/2);
            int compare_y =
                point.Y 
                -
                (space.Rect_Prism__DELTA_Y/2);
            int compare_z =
                point.Z
                -
                (space.Rect_Prism__DELTA_Z/2);

            int abs_compare_x =
                System.Math.Abs(compare_x);
            int abs_compare_y =
                System.Math.Abs(compare_y);
            int abs_compare_z =
                System.Math.Abs(compare_z);

            Axis_Type major_extrema;
            Axis_Type minor_extrema;

            if(abs_compare_x > abs_compare_y)
            {
                if (abs_compare_y > abs_compare_z)
                {
                    major_extrema = Axis_Type.X;
                    minor_extrema = Axis_Type.Y;
                }
                else
                {
                    if (abs_compare_x > abs_compare_z)
                    {
                        major_extrema = Axis_Type.X;
                        minor_extrema = Axis_Type.Z;
                    }
                    else
                    {
                        major_extrema = Axis_Type.Z;
                        minor_extrema = Axis_Type.X;
                    }
                }
            }
            else
            {
                if (abs_compare_x > abs_compare_z)
                {
                    major_extrema = Axis_Type.Y;
                    minor_extrema = Axis_Type.X;
                }
                else
                {
                    if (abs_compare_y > abs_compare_z)
                    {
                        major_extrema = Axis_Type.Y;
                        minor_extrema = Axis_Type.Z;
                    }
                    else
                    {
                        major_extrema = Axis_Type.Z;
                        minor_extrema = Axis_Type.Y;
                    }
                }
            }

            Plane_Type closest_plane_type;

            switch(major_extrema | minor_extrema)
            {
                default:
                case Axis_Type.X | Axis_Type.Y:
                    closest_plane_type = Plane_Type.XY;
                    break;
                case Axis_Type.X | Axis_Type.Z:
                    closest_plane_type = Plane_Type.XZ;
                    break;
                case Axis_Type.Y | Axis_Type.Z:
                    closest_plane_type = Plane_Type.YZ;
                    break;
            }

            bool is_min_or_max;

            switch(major_extrema)
            {
                default:
                case Axis_Type.X:
                    is_min_or_max =
                        compare_x <= 0;
                    break;
                case Axis_Type.Y:
                    is_min_or_max =
                        compare_y <= 0;
                    break;
                case Axis_Type.Z:
                    is_min_or_max =
                        compare_z <= 0;
                    break;
            }

            Rect_Prism rot__prism =
                new Rect_Prism(space.Rect_Prism__MIN, space.Rect_Prism__MAX, closest_plane_type);

            return 
                (is_min_or_max)
                ? rot__prism.Rect_Prism__MIN_PLANE
                : rot__prism.Rect_Prism__MAX_PLANE;
        }

        public static Rect_Prism Get__Internal(Rect_Prism space)
        {
            int min_x, max_x;
            int min_y, max_y;
            int min_z, max_z;

            if (space.Rect_Prism__MAX_X - space.Rect_Prism__MIN_X <= 2)
                min_x = max_x = space.Rect_Prism__MIN_X;
            else
            {
                min_x = space.Rect_Prism__MIN_X+1;
                max_x = space.Rect_Prism__MAX_X-1;
            }
            if (space.Rect_Prism__MAX_Y - space.Rect_Prism__MIN_Y <= 2)
                min_y = max_y = space.Rect_Prism__MIN_Y;
            else
            {
                min_y = space.Rect_Prism__MIN_Y + 1;
                max_y = space.Rect_Prism__MAX_Y - 1;
            }
            if (space.Rect_Prism__MAX_Z - space.Rect_Prism__MIN_Z <= 2)
                min_z = max_z = space.Rect_Prism__MIN_Z;
            else
            {
                min_z = space.Rect_Prism__MIN_Z + 1;
                max_z = space.Rect_Prism__MAX_Z - 1;
            }

            Integer_Vector_3 min = new Integer_Vector_3(min_x, min_y, min_z);
            Integer_Vector_3 max = new Integer_Vector_3(max_x, max_y, max_z);

            return new Rect_Prism(min, max, space.Rect_Prism__PLANE_TYPE);
        }

        public static void Split
        (
            Rect_Prism space,
            Integer_Vector_3 position,
            Plane_Type axis_type,
            out Rect_Prism left, 
            out Rect_Prism right
        )
        {
            int left__min_x =
                space.Rect_Prism__MIN_X;
            int left__min_y =
                space.Rect_Prism__MIN_Y;
            int left__min_z =
                space.Rect_Prism__MIN_Z;

            int left__max_x =
                space.Rect_Prism__MAX_X;
            int left__max_y =
                space.Rect_Prism__MAX_Y;
            int left__max_z =
                space.Rect_Prism__MAX_Z;
        
            int right__min_x =
                space.Rect_Prism__MIN_X;
            int right__min_y =
                space.Rect_Prism__MIN_Y;
            int right__min_z =
                space.Rect_Prism__MIN_Z;

            int right__max_x =
                space.Rect_Prism__MAX_X;
            int right__max_y =
                space.Rect_Prism__MAX_Y;
            int right__max_z =
                space.Rect_Prism__MAX_Z;

            switch(axis_type)
            {
                default:
                case Plane_Type.XY:
                    //left__max_plane__min_z = node.Node__KEY.Z;
                    left__max_z = position.Z;

                    right__min_z = position.Z;
                    //right__min_plane__max_z = node.Node__KEY.Z;
                    break;
                case Plane_Type.XZ:
                    //left__max_plane__min_y = node.Node__KEY.Y;
                    left__max_y = position.Y;

                    right__min_y = position.Y;
                    //right__min_plane__max_y = node.Node__KEY.Y;
                    break;
                case Plane_Type.YZ:
                    //left__max_plane__min_x = node.Node__KEY.X;
                    left__max_x = position.X;

                    right__min_x = position.X;
                    //right__min_plane__max_x = node.Node__KEY.X;
                    break;
            }

            Integer_Vector_3 left__min =
                new Integer_Vector_3
                (
                    left__min_x,
                    left__min_y,
                    left__min_z
                );
            Integer_Vector_3 left__max =
                new Integer_Vector_3
                (
                    left__max_x,
                    left__max_y,
                    left__max_z
                );

            Integer_Vector_3 right__min =
                new Integer_Vector_3
                (
                    right__min_x,
                    right__min_y,
                    right__min_z
                );
            Integer_Vector_3 right__max =
                new Integer_Vector_3
                (
                    right__max_x,
                    right__max_y,
                    right__max_z
                );

            left = 
                new Rect_Prism
                (
                    left__min,
                    left__max,
                    axis_type
                );

            right =
                new Rect_Prism
                (
                    right__min,
                    right__max,
                    axis_type
                );
        }
    }
}
