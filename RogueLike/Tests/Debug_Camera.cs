
using OpenTK;
using OpenTK.Input;
using Xerxes_Engine.Export_OpenTK.Exports.Input;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Debug_Camera :
        Isometric_Camera
    {
        public Debug_Camera()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Input_Key_Down>
                (Private_Handle__Key_Down__Debug_Camera)
                .Downstream.Receiving<SA__Configure_Game>
                (Private_Configure__Camera);
        }

        private void Private_Configure__Camera(SA__Configure_Game e)
        {
            int camera_x = 0;
            int camera_y = 0;
            int camera_z = 0;

            int camera_radius = 9;

            int camera_ray_step = 10;

            int camera_ray_grain = 1;

            e.Check_For__Flag_Int__Configure_Root(nameof(camera_x), ref camera_x, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(camera_y), ref camera_y, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(camera_z), ref camera_z, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(camera_radius), ref camera_radius, false);

            e.Check_For__Flag_Int__Configure_Root(nameof(camera_ray_step), ref camera_ray_step, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(camera_ray_grain), ref camera_ray_grain, false);

            float ray_sphere_step  = 1f / ((float)camera_ray_step);
            float ray_sphere_grain = 1f / ((float)camera_ray_grain); 

            Isometric_Camera__Isometric_Position =
                new Integer_Vector_3(camera_x, camera_y, camera_z);

            Isometric_Camera__RAY_SPHERE = 
                new Ray_Sphere(camera_radius);

            Xerxes_Engine.Log.Write__Info__Log($"Configured Debug Camera: {Isometric_Camera__Isometric_Position}.", this);
        }

        private void Private_Handle__Key_Down__Debug_Camera(SA__Input_Key_Down e)
        {
            switch(e.Input_Keyboard__KEY)
            {
                case Key.W:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(1,0,0);
                    break;
                case Key.S:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(-1,0,0);
                    break;
                case Key.A:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(0,0,-1);
                    break;
                case Key.D:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(0,0,1);
                    break;
                case Key.Q:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(0,1,0);
                    break;
                case Key.E:
                    Isometric_Camera__Isometric_Position
                        += new Integer_Vector_3(0,-1,0);
                    break;
                case Key.Up:
                    Camera__Position += new Vector3(0,-10f,0);
                    break;
                case Key.Down:
                    Camera__Position += new Vector3(0,10f,0);
                    break;
                case Key.Left:
                    Camera__Position += new Vector3(10f,0,0);
                    break;
                case Key.Right:
                    Camera__Position += new Vector3(-10f,0,0);
                    break;
                case Key.Plus:
                    Camera__Zoom *= 1.5f;
                    break;
                case Key.Minus:
                    Camera__Zoom /= 1.5f;
                    break;
            }
            
            Xerxes_Engine.Log.Write__Info__Log($"Zoom:{Camera__Zoom}, Position:{Isometric_Camera__Isometric_Position}.", this);

            e.SA__Consume();
        }
    }
}
