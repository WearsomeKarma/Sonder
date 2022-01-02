
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Tools;

namespace Rogue_Like
{
    public class Ray_Sphere
    {
        public int Ray_Sphere__RADIUS { get; }
        public int Ray_Sphere__RADIUS_2
            => Ray_Sphere__RADIUS * Ray_Sphere__RADIUS;

        private List<Ray> Ray_Sphere__RAYS { get; }

        public Ray_Sphere(int radius)
        {
            Ray_Sphere__RADIUS = radius;

            Ray_Sphere__RAYS = new List<Ray>();

            int min_x = - radius;
            int min_y = - radius;
            int min_z = - radius;

            int max_x = radius;
            int max_y = radius;
            int max_z = radius;

            HashSet<Integer_Vector_3> points = new HashSet<Integer_Vector_3>();

            for(int z=min_z;z<=max_z;z++)
            {
                if (Private_Assert__Extrema_Z__Ray_Sphere(z, points))
                    continue;

                for(int y=min_y;y<=max_y;y++)
                {
                    if (Private_Assert__Extrema_Y__Ray_Sphere(y, points))
                        continue;

                    int z2y2 = (z*z) + (y*y);

                    if (z2y2 > Ray_Sphere__RADIUS_2)
                        continue;

                    for(int x=max_x;x>=min_x;x--)
                    {
                        if ((x*x) + z2y2 > Ray_Sphere__RADIUS_2)
                            continue;

                        if (Private_Assert__Extrema_X__Ray_Sphere(x, points))
                            continue;

                        bool rel_prime =
                            Private_Check_If__Relatively_Prime__Ray_Sphere(x,y,z);

                        if (rel_prime)
                            Private_Record__Ray__Ray_Sphere
                            (
                                new Integer_Vector_3 
                                (
                                    x,
                                    y,
                                    z
                                ),
                                points
                            );
                    }
                }
            }
        }

        private bool Private_Assert__Extrema_X__Ray_Sphere(float i, HashSet<Integer_Vector_3> points)
        {
            if (Private_Affirm__Not_Extrema__Ray_Sphere(i))
                return false;

            Integer_Vector_3 vec = new Integer_Vector_3(System.Math.Sign(i),0,0);
            Private_Record__Ray__Ray_Sphere(vec, points);

            return true;
        }
        
        private bool Private_Assert__Extrema_Y__Ray_Sphere(float i, HashSet<Integer_Vector_3> points)
        {
            if (Private_Affirm__Not_Extrema__Ray_Sphere(i))
                return false;

            Integer_Vector_3 vec = new Integer_Vector_3(0,System.Math.Sign(i),0);
            Private_Record__Ray__Ray_Sphere(vec, points);

            return true;
        }

        private bool Private_Assert__Extrema_Z__Ray_Sphere(float i, HashSet<Integer_Vector_3> points)
        {
            if (Private_Affirm__Not_Extrema__Ray_Sphere(i))
                return false;

            Integer_Vector_3 vec = new Integer_Vector_3(0,0,System.Math.Sign(i));
            Private_Record__Ray__Ray_Sphere(vec, points);

            return true;
        }

        private bool Private_Affirm__Not_Extrema__Ray_Sphere(float i)
            => !Math_Helper.Tolerable__Equality__Float(i * i, Ray_Sphere__RADIUS_2);

        private bool Private_Check_If__Relatively_Prime__Ray_Sphere(float x, float y, float z)
        {
            if (x == 0 && x == y && y == z)
                return false;

            x = System.Math.Abs(x);
            y = System.Math.Abs(y);
            z = System.Math.Abs(z);

            float max = (x < y) ? (y < z ? z : y) : (x < z ? z : x);

            for(float d=max;d>1;d-=0.5f)
            {
                bool divides_x = 
                    x != 0
                    &&
                    Math_Helper.Tolerable__Divides(d,x);
                bool divides_y = 
                    y != 0
                    &&
                    Math_Helper.Tolerable__Divides(d,y);
                bool divides_z = 
                    z != 0
                    &&
                    Math_Helper.Tolerable__Divides(d,z);

                if (divides_x && divides_y && divides_z)
                {
                    return false;
                }
            }

            return true;
        }

        private void Private_Record__Ray__Ray_Sphere(Integer_Vector_3 ray, HashSet<Integer_Vector_3> points)
        {
            if(points.Contains(ray))
                return;

            Ray_Sphere__RAYS.Add(new Ray(new Integer_Vector_3(), ray.X, ray.Y, ray.Z, Ray_Sphere__RADIUS_2));
        }

        public IEnumerable<Ray> Get__Rays__Ray_Sphere(Integer_Vector_3? nullable_offset = null)
        {
            foreach(Ray ray in Ray_Sphere__RAYS)
                yield return ray + (nullable_offset ?? new Integer_Vector_3());
        }

        public override string ToString()
        {
            string s = $"ray_sphere:[radius:{Ray_Sphere__RADIUS}]";

            s += "<";

            foreach(Ray ray in Ray_Sphere__RAYS)
                s+= $"{ray},";

            s = s.Substring(0, s.Length - 1);
            s += ">";

            return s;
        }
    }
}
