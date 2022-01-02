
using System.Collections.Generic;
using OpenTK;
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Engine_Objects;

namespace Rogue_Like
{
    public class Isometric_Camera :
        Camera
    {
        protected Ray_Sphere Isometric_Camera__RAY_SPHERE { get; set; }

        private Integer_Vector_3 _isometric_Camera__Isometric_Position;
        protected Integer_Vector_3 Isometric_Camera__Isometric_Position 
        { 
            get => _isometric_Camera__Isometric_Position; 
            set 
            {
                _isometric_Camera__Isometric_Position = value;
                Isometric_Camera__COLLISION_CACHE.Clear();
            }
        }

        private HashSet<Integer_Vector_3> Isometric_Camera__COLLISION_CACHE { get; }

        public Isometric_Camera()
        {
            Declare__Streams()
                .Upstream.Receiving<SA__Draw_Level>(Handle_Draw__Level__Isometic_Camera)
                .Upstream.Extending<SA__Draw>();

            Isometric_Camera__RAY_SPHERE = 
                new Ray_Sphere(10);

            Isometric_Camera__COLLISION_CACHE = new HashSet<Integer_Vector_3>();
        }

        protected virtual void Handle_Draw__Level__Isometic_Camera(SA__Draw_Level e)
        {
            Isometric_Camera__COLLISION_CACHE.Clear();
            foreach(Ray ray in Isometric_Camera__RAY_SPHERE.Get__Rays__Ray_Sphere(Isometric_Camera__Isometric_Position))
            {
                Integer_Vector_3 tile_pos;
                Tile? tile = e.Cast__Ray__Draw_Level(ray, out tile_pos);

                if (tile == null || Isometric_Camera__COLLISION_CACHE.Contains(tile_pos))
                    continue;
                Isometric_Camera__COLLISION_CACHE.Add(tile_pos);

                Tile draw_tile = (Tile)tile;

                SA__Draw e_draw_tile =
                    new SA__Draw(e.Draw_Level__RENDER);

                e_draw_tile
                    .Draw__Vertex_Object_Handle = draw_tile.Tile__SPRITE.Sprite__Active_Vertex_Object;

                int x = Core_Tile_Handles.TILE__SPAN_X * (tile_pos.X + tile_pos.Z);
                int y = 
                    Core_Tile_Handles.TILE__SPAN_Z * (tile_pos.X - tile_pos.Z)
                    +
                    Core_Tile_Handles.TILE__SPAN_Y * (tile_pos.Y);

                Vector3 draw_pos =
                    new Vector3(x,y,0); 

                Matrix4 projection = Matrix4.Identity; 

                e_draw_tile.Draw__Projection_Matrix = projection;

                e_draw_tile
                    .Draw__Position =
                    draw_pos;

                Invoke__Ascending(e_draw_tile);
            }
        }
    }
}
