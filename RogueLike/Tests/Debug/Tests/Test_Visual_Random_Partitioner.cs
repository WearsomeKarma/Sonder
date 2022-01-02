
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Templates;

namespace Rogue_Like
{
    public class Test_Visual_Random_Partitioner :
        Game_R2
    {
        public static int Level_Size_X { get; private set; }
        public static int Level_Size_Y { get; private set; }
        public static int Level_Size_Z { get; private set; }

        public static int Level_Room_Count { get; private set; }

        public Test_Visual_Random_Partitioner()
        {
            Declare__Export<Tile_Library>();

            Declare__Streams()
                .Downstream.Extending<SA__Load_Tile>()
                .Downstream.Extending<SA__Level_Generation>();

            Declare__Hierarchy()
                .Associate<Tile_Pipeline>()
                .Associate<Debug_Camera>()
                .Focus__Descendant<Debug_Camera>()
                    .Associate<World>()
                    .Focus__Descendant<World>()
                        .Associate<Debug_Random_Partitioner>()
                        .Associate<Debug_Compositor>();
        }

        protected override SA__Associate_Game_OpenTK Configure(SA__Configure_Game game_Arguments)
        {
            Core_Tile_Handles.TILE__SPAN_X = 16;
            Core_Tile_Handles.TILE__SPAN_Z = 8;
            Core_Tile_Handles.TILE__SPAN_Y = 7;

            int size_x = 40;
            int size_y = 40;
            int size_z = 40;
            int room_count = 3;

            game_Arguments
                .Check_For__Flag_Int__Configure_Root(nameof(size_x), ref size_x, false);
            game_Arguments
                .Check_For__Flag_Int__Configure_Root(nameof(size_y), ref size_y, false);
            game_Arguments
                .Check_For__Flag_Int__Configure_Root(nameof(size_z), ref size_z, false);

            game_Arguments
                .Check_For__Flag_Int__Configure_Root(nameof(room_count), ref room_count);

            Level_Size_X = size_x;
            Level_Size_Y = size_y;
            Level_Size_Z = size_z;

            Level_Room_Count = room_count;

            Xerxes_Engine.Log.Write__Info__Log($"Size_X:{size_x} Size_Y:{size_y} Size_Z:{size_z} -- Room Count:{Level_Room_Count}.", this);

            return base.Configure(game_Arguments);
        }

        protected override void Handle_Load__Content__Game()
        {
            SA__Load_Tile e_load_tile =
                new SA__Load_Tile("debug_tile_set.png");

            Invoke__Descending(e_load_tile);

            Core_Tile_Handles.TILE__DEBUG = e_load_tile.Load_Asset__Handle;

            SA__Level_Generation e_generate_level =
                new SA__Level_Generation(Level_Size_X, Level_Size_Y, Level_Size_Z, Level_Room_Count);

            Invoke__Descending(e_generate_level);
        }

        public static void Main(string[] args)
        {
            Xerxes_Engine.Log.Initalize__Log(new Xerxes_Engine.Log_Arguments());
            Xerxes_Engine.Export_OpenTK.Xerxes_OpenTK.Run<Test_Visual_Random_Partitioner>(new SA__Configure_Game(arguments: args));
        }
    }
}
