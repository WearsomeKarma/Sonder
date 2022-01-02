
using OpenTK;
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Engine_Objects;

namespace Rogue_Like
{
    public class Debug_Tile_Scene_Layer :
        Scene_Layer
    {
        private Level Debug_Tile_Scene_Layer__Debug_Map {get; set;}

        private Vector3 Debug_Tile_Scene_Layer__Map_Offset { get; set; }

        public Debug_Tile_Scene_Layer()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Render>
                (Private_Render__Tile_Map__Debug_Tile_Scene_Layer)
                .Downstream.Receiving<SA__Level_Generation>
                (Private_Generate__Debug_Map__Debug_Tile_Scene_Layer)
                .Upstream.Extending<SA__Batch_Vertex_Object>()
                .Upstream.Extending<SA__Get_Tile>()
                .Upstream.Extending<SA__Draw>();
        }

        private void Private_Generate__Debug_Map__Debug_Tile_Scene_Layer
        (SA__Level_Generation e)
        {
            int rc_sum = e.Generate_Level__SIZE_X + e.Generate_Level__SIZE_Y;
            Debug_Tile_Scene_Layer__Map_Offset =
                new Vector3(-(Core_Tile_Handles.TILE__SPAN_X*rc_sum)/2, -(Core_Tile_Handles.TILE__SPAN_Y*rc_sum)/2, 0);

            SA__Get_Tile e_get_tile =
                new SA__Get_Tile(Test_Tiles.Test_Tiles__Debug_Tile_Handle);

            Invoke__Ascending(e_get_tile);

            if (e_get_tile.Get_Tile__Tile == null)
            {
                Xerxes_Engine.Log.Write__Error__Log("Failed to load debug tile set!", this);
                return;
            }

            Tile tile = (Tile)e_get_tile.Get_Tile__Tile;
            if (tile.Tile__SPRITE.Sprite__Active_Vertex_Object == null)
                return;

            Texture_R2 tile_texture =
                tile
                .Tile__SPRITE
                .Sprite__Active_Vertex_Object
                .Vertex_Object_Handle__Texture;

            Debug_Tile_Scene_Layer__Debug_Map = 
                new Level
                (
                    new Rect_Prism
                    (
                        e.Generate_Level__SIZE_X, 
                        e.Generate_Level__SIZE_Y, 
                        e.Generate_Level__SIZE_Z
                    )
                );

            for(int y=0;y<e.Generate_Level__SIZE_Y;y++)
            {
                for(int x=0;x<e.Generate_Level__SIZE_X;x++)
                {
                    Debug_Tile_Scene_Layer__Debug_Map[x,y,0] = tile;
                }
            }

            Sprite s = tile.Tile__SPRITE;
            s.Set__Active_Vertex_Object__Sprite(1);
            Debug_Tile_Scene_Layer__Debug_Map[3,3,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(3);
            Debug_Tile_Scene_Layer__Debug_Map[3,4,0] = new Tile(s); 
            s.Set__Active_Vertex_Object__Sprite(2);
            Debug_Tile_Scene_Layer__Debug_Map[3,5,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(6);
            Debug_Tile_Scene_Layer__Debug_Map[2,5,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(4);
            Debug_Tile_Scene_Layer__Debug_Map[1,5,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(12);
            Debug_Tile_Scene_Layer__Debug_Map[1,4,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(8);
            Debug_Tile_Scene_Layer__Debug_Map[1,3,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(9);
            Debug_Tile_Scene_Layer__Debug_Map[2,3,0] = new Tile(s);

            s.Set__Active_Vertex_Object__Sprite(0);
            Debug_Tile_Scene_Layer__Debug_Map[2,4,1] = new Tile(s);

            s.Set__Active_Vertex_Object__Sprite(6);
            Debug_Tile_Scene_Layer__Debug_Map[6,2,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(10);
            Debug_Tile_Scene_Layer__Debug_Map[7,2,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(3);
            Debug_Tile_Scene_Layer__Debug_Map[7,1,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(9);
            Debug_Tile_Scene_Layer__Debug_Map[8,2,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(12);
            Debug_Tile_Scene_Layer__Debug_Map[7,3,0] = new Tile(s);

            s.Set__Active_Vertex_Object__Sprite(0);
            Debug_Tile_Scene_Layer__Debug_Map[6,1,1] = new Tile(s);
            Debug_Tile_Scene_Layer__Debug_Map[8,3,1] = new Tile(s);

            s.Set__Active_Vertex_Object__Sprite(5);
            Debug_Tile_Scene_Layer__Debug_Map[7,7,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(12);
            Debug_Tile_Scene_Layer__Debug_Map[7,6,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(9);
            Debug_Tile_Scene_Layer__Debug_Map[6,7,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(6);
            Debug_Tile_Scene_Layer__Debug_Map[8,7,0] = new Tile(s);
            s.Set__Active_Vertex_Object__Sprite(3);
            Debug_Tile_Scene_Layer__Debug_Map[7,8,0] = new Tile(s);

            s.Set__Active_Vertex_Object__Sprite(0);
            Debug_Tile_Scene_Layer__Debug_Map[6,8,1] = new Tile(s);
            Debug_Tile_Scene_Layer__Debug_Map[8,6,1] = new Tile(s);
        }

        private void Private_Render__Tile_Map__Debug_Tile_Scene_Layer(SA__Render e)
        {
            for(int z=0;z<2;z++)
            {
                for(int y=0;y<Debug_Tile_Scene_Layer__Debug_Map.Level__SIZE_Y;y++)
                {
                    for(int x=Debug_Tile_Scene_Layer__Debug_Map.Level__SIZE_X-1;x>=0;x--)
                    {
                        SA__Draw e_draw_tile = new SA__Draw(e);

                        e_draw_tile.Draw__Projection_Matrix =
                            OpenTK.Matrix4.Identity;

                        Integer_Vector_3 vec = new Integer_Vector_3(x,y,z);

                        e_draw_tile.Draw__Vertex_Object_Handle =
                            Debug_Tile_Scene_Layer__Debug_Map[vec].Value.Tile__SPRITE.Sprite__Active_Vertex_Object;

                        int dx = 
                            Core_Tile_Handles.TILE__SPAN_X * (x+y);
                        int dy =
                            Core_Tile_Handles.TILE__SPAN_Y * (x-y)
                            +
                            (Core_Tile_Handles.TILE__SPAN_Z * z);

                        e_draw_tile.Draw__Position =
                            new Vector3(dx,dy,0);

                        Invoke__Ascending(e_draw_tile);
                    }
                }
            }
        }
    }
}
