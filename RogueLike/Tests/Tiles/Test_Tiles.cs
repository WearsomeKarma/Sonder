
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Engine_Objects;
using Xerxes_Engine.Export_OpenTK.Exports.Graphics;
using Xerxes_Engine.Export_OpenTK.Exports.Graphics.R2;
using Xerxes_Engine.Export_OpenTK.Exports.Serialization;

namespace Rogue_Like
{
    public class Test_Tiles : Game
    {
        public static Tile_Handle Test_Tiles__Debug_Tile_Handle { get; private set; }

        private static string Test_Tiles__Tile_File { get; set; }

        public Test_Tiles()
        {
            Core_Tile_Handles.TILE__SPAN_X = 16;
            Core_Tile_Handles.TILE__SPAN_Z = 7;
            Core_Tile_Handles.TILE__SPAN_Y = 8;

            Declare__Export<Render_Service>();
            Declare__Export<Asset_Pipe>();
            Declare__Export<Vertex_Object_Library>();
            Declare__Export<Sprite_Library>();

            Declare__Export<Tile_Library>();

            Declare__Streams()
                .Downstream.Extending<SA__Load_Tile>()
                .Upstream.Extending<SA__Declare_Vertex_Object>()
                .Downstream.Extending<SA__Level_Generation>();

            Declare__Hierarchy()
                .Associate<Tile_Pipeline>()
                .Associate<Camera>()
                .Focus__Descendant<Camera>()
                    .Associate<Debug_Tile_Scene_Layer>();
        }

        protected override SA__Associate_Game_OpenTK Configure(SA__Configure_Game game_Arguments)
        {
            string file_name = "debug_tile_set.png";

            game_Arguments.Check_For__Flag_String__Configure_Root(nameof(file_name), ref file_name);

            Xerxes_Engine.Log.Write__Info__Log($"Using file:{file_name} for testing.", this);

            Test_Tiles__Tile_File =
                file_name;

            return base.Configure(game_Arguments);
        }

        protected override void Handle_Load__Content__Game()
        {
            SA__Load_Tile e_load_tile =
                new SA__Load_Tile(Test_Tiles__Tile_File);

            Invoke__Descending(e_load_tile);

            Test_Tiles__Debug_Tile_Handle =
                e_load_tile.Load_Asset__Handle;

            Invoke__Descending(new SA__Level_Generation(10,10,10,2));
        }

        public static void Main(string[] args)
        {
            Xerxes_Engine.Log.Initalize__Log(new Xerxes_Engine.Log_Arguments(throwOnError: false));
            Xerxes_OpenTK.Run<Test_Tiles>(new SA__Configure_Game(arguments: args));
        }
    }
}
