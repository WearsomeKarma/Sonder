
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public sealed class Tile_Pipeline :
    Asset_Pipeline<SA__Load_Tile, SA__Declare_Tile, Tile_Handle, Tile>
    {
        public const int TILE_WIDTH = 32;
        public const int TILE_HEIGHT = 32;

        public const int TILE_SHEET_COLUMNS = 14;

        public Tile_Pipeline()
        {
            Declare__Streams()
                .Upstream.Extending<SA__Load_Texture_R2>()
                .Upstream.Extending<SA__Declare_Vertex_Object>()
                .Upstream.Extending<SA__Declare_Sprite>();
        }

        protected override SA__Declare_Tile Handle_Formulate__Declare__Asset_Pipeline(SA__Load_Tile e, Tile asset)
        {
            SA__Declare_Tile e_declare_tile =
                new SA__Declare_Tile(e.Load_Asset_Base__FILENAME, asset);

            return e_declare_tile;
        }

        protected override Tile Handle_Load__Asset__Asset_Pipeline(SA__Load_Tile e)
        {
            string file_name = e.Load_Asset_Base__FILENAME;

            SA__Load_Texture_R2 e_load_tile_texture =
                new SA__Load_Texture_R2(file_name);

            Invoke__Ascending(e_load_tile_texture);

            Texture_R2 tile_texture =
                (Texture_R2)e_load_tile_texture.Load_Texture_R2__Texture;

            Integer_Vector_2 index;

            Vertex_Object_Handle[] tile_VOHs = new Vertex_Object_Handle[TILE_SHEET_COLUMNS];
            
            for(int i=0;i<TILE_SHEET_COLUMNS;i++)
            {
                index = new Integer_Vector_2(i, 0);

                SA__Declare_Vertex_Object e_declare_tile_vo =
                    new SA__Declare_Vertex_Object
                    (
                        tile_texture,
                        TILE_WIDTH,
                        TILE_HEIGHT,
                        new Integer_Vector_2[] { index },
                        new Integer_Vector_2[] { new Integer_Vector_2(0,0) }
                    );

                Invoke__Ascending(e_declare_tile_vo);

                tile_VOHs[i] =
                    e_declare_tile_vo
                    .Declare_Vertex_Object__Vertex_Object_Handle;
            }
            
            SA__Declare_Sprite e_tile_sprite =
                new SA__Declare_Sprite(tile_VOHs);

            Invoke__Ascending(e_tile_sprite);

            Tile tile = 
                new Tile(e_tile_sprite.Declare_Sprite__Sprite);

            return tile;
        }
    }
}
