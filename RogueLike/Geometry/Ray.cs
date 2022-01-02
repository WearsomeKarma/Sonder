
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Tools;

namespace Rogue_Like
{
    public struct Ray
    {
        public Integer_Vector_3 Ray__SOURCE { get; }
        public int Ray__SOURCE_X
            => Ray__SOURCE.X;
        public int Ray__SOURCE_Y
            => Ray__SOURCE.Y;
        public int Ray__SOURCE_Z
            => Ray__SOURCE.Z;

        public int Ray__STEP_X { get; }
        public int Ray__STEP_Y { get; }
        public int Ray__STEP_Z { get; }

        public int Ray__DISTANCE_SQUARED { get; }

        public Ray
        (
            Integer_Vector_3 source, 
            int step_x, 
            int step_y, 
            int step_z, 
            int distance_squared
        )
        {
            Ray__SOURCE = source;

            Ray__STEP_X = step_x;
            Ray__STEP_Y = step_y;
            Ray__STEP_Z = step_z;

            if (step_x == 0 && step_x == step_y && step_y == step_z)
            {
                Ray__DISTANCE_SQUARED = -1;
                return;
            }

            Ray__DISTANCE_SQUARED = distance_squared;
        }

        public override int GetHashCode()
        {
            int hash_source = Ray__SOURCE.GetHashCode();
            int hash_step =
                (new Integer_Vector_3(Ray__STEP_X, Ray__STEP_Y, Ray__STEP_Z).GetHashCode());

            return (hash_source << 5 + hash_source) + hash_step;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ray))
                return false;

            Ray ray = (Ray)obj;

            bool is_equal_source =
                Ray__SOURCE == ray.Ray__SOURCE;

            bool is_step_x_equal =
                Ray__STEP_X == ray.Ray__STEP_X;
            bool is_step_y_equal =
                Ray__STEP_Y == ray.Ray__STEP_Y;
            bool is_step_z_equal =
                Ray__STEP_Z == ray.Ray__STEP_Z;

            bool equals =
                is_equal_source
                &&
                is_step_x_equal
                && 
                is_step_y_equal
                &&
                is_step_z_equal;

            return equals;
        }

        public IEnumerable<Integer_Vector_3> Get__Positions__Ray()
        {
            //Integer_Vector_3? old = null;
            Integer_Vector_3 vec;

            int x = Ray__SOURCE_X;
            int y = Ray__SOURCE_Y;
            int z = Ray__SOURCE_Z;

            bool in_bounds;

            do
            {
                vec = 
                    new Integer_Vector_3(x, y, z);

                in_bounds =
                    Math_Helper.Distance_Squared
                    (
                        vec,
                        Ray__SOURCE
                    )
                    <=
                    Ray__DISTANCE_SQUARED;

                if (in_bounds)
                    yield return vec;

                x+=Ray__STEP_X;
                y+=Ray__STEP_Y;
                z+=Ray__STEP_Z;
            }
            while
            (
                in_bounds
            );
        }

        public override string ToString()
        {
            return $"ray:[{Ray__SOURCE}, dx:{Ray__STEP_X} dy:{Ray__STEP_Y} dz:{Ray__STEP_Z} -> distance^2:{Ray__DISTANCE_SQUARED}]";
        }

        public static Ray operator +(Ray ray, Integer_Vector_3 offset)
            => new Ray(offset, ray.Ray__STEP_X, ray.Ray__STEP_Y, ray.Ray__STEP_Z, ray.Ray__DISTANCE_SQUARED);
    }
}
