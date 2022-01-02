
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Test_Ray_Sphere :
        Xerxes_Object<Test_Ray_Sphere>
    {
        public Test_Ray_Sphere()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Configure_Root>
                (Private_Run__Test);
        }

        private void Private_Run__Test(SA__Configure_Root e)
        {
            int plane_assess_x =0;
            int plane_assess_y =0;
            int plane_assess_z =0;

            int source_x = 0;
            int source_y = 0;
            int source_z = 0;

            int offset_x = 0;
            int offset_y = 0;
            int offset_z = 0;

            int radius = 3;

            int step = 10;
            int step_grain = 1;

            e.Check_For__Flag_Int__Configure_Root(nameof(plane_assess_x), ref plane_assess_x, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(plane_assess_y), ref plane_assess_y, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(plane_assess_z), ref plane_assess_z, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(source_x), ref source_x, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(source_y), ref source_y, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(source_z), ref source_z, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(offset_x), ref offset_x, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(offset_y), ref offset_y, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(offset_z), ref offset_z, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(radius), ref radius, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(step), ref step, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(step_grain), ref step_grain, false);

            Integer_Vector_3 source = 
                new Integer_Vector_3(source_x, source_y, source_z);
            Integer_Vector_3 offset =
                new Integer_Vector_3(offset_x, offset_y, offset_z);

            Ray_Sphere ray_sphere =
                new Ray_Sphere(radius);

            Log.Write__Info__Log($"Ray Sphere:{ray_sphere}.", this);

            HashSet<Integer_Vector_3> points = new HashSet<Integer_Vector_3>();

            List<Integer_Vector_3> points_plane_x = new List<Integer_Vector_3>();
            List<Integer_Vector_3> points_plane_y = new List<Integer_Vector_3>();
            List<Integer_Vector_3> points_plane_z = new List<Integer_Vector_3>();

            foreach(Ray ray in ray_sphere.Get__Rays__Ray_Sphere(offset))
            {
                Log.Write__Info__Log($"Ray:{ray}.", this);
                foreach(Integer_Vector_3 pos in ray.Get__Positions__Ray())
                {
                    if (points.Contains(pos))
                        continue;
                    points.Add(pos);
                    Log.Write__Info__Log($"\t>{pos}", this);

                    if (pos.X == plane_assess_x)
                        points_plane_x.Add(pos);
                    if (pos.Y == plane_assess_y)
                        points_plane_y.Add(pos);
                    if (pos.Z == plane_assess_z)
                        points_plane_z.Add(pos);
                }
            }

            Integer_Vector_3 min =
                new Integer_Vector_3
                (
                    source_x - radius,
                    source_y - radius,
                    source_z - radius
                );

            Integer_Vector_3 max =
                new Integer_Vector_3
                (
                    source_x + radius,
                    source_y + radius,
                    source_z + radius
                );

            Rect_Prism space = new Rect_Prism(min, max);

            foreach(Integer_Vector_3 plane_point in points_plane_x)
                Log.Write__Info__Log($"PLANE_X: {plane_point}", this);
            foreach(Integer_Vector_3 plane_point in points_plane_y)
                Log.Write__Info__Log($"PLANE_Y: {plane_point}", this);
            foreach(Integer_Vector_3 plane_point in points_plane_z)
                Log.Write__Info__Log($"PLANE_Z: {plane_point}", this);

            foreach(Integer_Vector_3 pos in space.Get__Positions__Rect_Prism())
                if (!points.Contains(pos) && Xerxes_Engine.Export_OpenTK.Tools.Math_Helper.Distance_Squared(source, pos) < radius)
                    Log.Write__Info__Log($"Not including rect_prism point:{pos}", this);
        }
        
        public static void Main(string[] args)
        {
            Log.Initalize__Log(new Log_Arguments());

            SA__Configure_Root configure_test =
                new SA__Configure_Root(args);

            Xerxes.Test<Test_Ray_Sphere, SA__Configure_Root>(configure_test);
        }
    }
}
