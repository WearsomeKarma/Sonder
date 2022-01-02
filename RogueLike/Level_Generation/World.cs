using OpenTK;
using Rogue_Like.Entities.Player_Entity;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    internal class World :
        Xerxes_Object<World>
    {
        public const int TILE_SPAN_X = 17;
        public const int TILE_SPAN_Z = 8;
        public const int TILE_SPAN_Y = 6;

        private Level _World__Current_Level { get; set; }
        
        private Vector3 _World__Screen_Position { get; set; }

        public World()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Game_Window_Resized>(Private_Reposition__World)
                .Upstream  .Extending<SA__Declare_Vertex_Object>()
                .Downstream.Receiving<SA__Render>(Private_Render__World)
                .Upstream  .Extending<SA__Draw_Level>()
                .Upstream  .Extending<SA__Draw>()
                .Upstream  .Receiving<SA__Draw_Entity>(Private_Draw__Entity__World)
                .Downstream.Receiving<SA__Update>(Private_Spawn__Player__World)
                .Downstream.Extending<SA__Spawn_Entity<Player>>()
                .Downstream.Receiving<SA__Level_Generation>(Private_Generate__Level__World)
                .Downstream.Extending<SA__Level_Partitioning>()
                .Downstream.Extending<SA__Level_Hallway_Plotting>()
                .Downstream.Extending<SA__Level_Compositing>()
                .Upstream  .Receiving<SA__Set_Tile>(Private_Set__Tile__World);
        }

        private void Private_Set__Tile__World(SA__Set_Tile e)
            => _World__Current_Level[e.Set_Tile__POSITION] = e.Set_Tile__TILE;

        private bool hasSpawnedPlayer;
        private void Private_Spawn__Player__World
        (SA__Update e)
        {
            if (hasSpawnedPlayer)
                return;
            hasSpawnedPlayer = true;

            SA__Spawn_Entity<Player> e1 = 
                new SA__Spawn_Entity<Player>
                (
                    e,
                    new Integer_Vector_3(3,3)
                );

            Invoke__Descending
                (e1);
        }

        private void Private_Cast__Ray__World(SA__Cast_Ray e)
        {
            Integer_Vector_3 collision_point;
            Tile? tile = _World__Current_Level.Cast__Ray__Level(e.Cast_Ray__RAY, out collision_point);

            e.Cast_Ray__Collision_Position = collision_point;
            e.Cast_Ray__Collision = tile;
        }

        private void Private_Generate__Level__World(SA__Level_Generation e)
        {
            _World__Current_Level =
                new Level(e.Generate_Level__SPACE);

            Xerxes_Engine.Log.Write__Info__Log($"Partitioning Level...", this);
            SA__Level_Partitioning e_partition =
                new SA__Level_Partitioning(e.Generate_Level__SPACE, e.Generate_Level__ROOM_COUNT);

            Invoke__Descending(e_partition);

            Xerxes_Engine.Log.Write__Info__Log("Compositing Level...", this);
            SA__Level_Compositing e_composite =
                new SA__Level_Compositing(e_partition, null);

            Invoke__Descending(e_composite);
        }

        private void Private_Render__World
        (SA__Render e)
        {
            SA__Draw_Level e_draw_level =
                new SA__Draw_Level(e, _World__Current_Level, new Integer_Vector_3(), 0);

            Invoke__Ascending(e_draw_level);
        }

        private void Private_Reposition__World
        (SA__Game_Window_Resized e)
        {
            float width2 = e.SA__Resize_2D__WIDTH/2;
            float height2 = e.SA__Resize_2D__HEIGHT/2;

            _World__Screen_Position =
                new Vector3(-width2,-height2,0);
        }

        private void Private_Draw__Entity__World
        (SA__Draw_Entity e)
        {
            Entity entity = e.Draw_Entity__ENTITY;
            Integer_Vector_3 entityPos = e.Draw_Entity__ENTITY.Entity__Position;

            Vector3 basePosition = _World__Screen_Position;
            Vector3 offset = 
                new Vector3 
                (
                    entityPos.X * String_Batcher.CHAR_PIXEL_WIDTH,
                    entityPos.Y * String_Batcher.CHAR_PIXEL_HEIGHT,
                    0
                );

            Vector3 drawPos = basePosition + offset;

            SA__Draw e1 =
                new SA__Draw
                (
                    e
                );

            e1.Draw__Position = drawPos;
            e1.Draw__Vertex_Object_Handle =
                entity.Entity__VO;

            Invoke__Ascending
                (e1);
        }

#region Static Direction Translation
        public static Integer_Vector_3 Get__Offset(Direction d)
        {
            switch(d)
            {
                case Direction.North:
                    return new Integer_Vector_3(0,1);
                case Direction.North_East:
                    return new Integer_Vector_3(1,1);
                case Direction.East:
                    return new Integer_Vector_3(1,0);
                case Direction.South_East:
                    return new Integer_Vector_3(1,-1);
                case Direction.South:
                    return new Integer_Vector_3(0,-1);
                case Direction.South_West:
                    return new Integer_Vector_3(-1,-1);
                case Direction.West:
                    return new Integer_Vector_3(-1,0);
                case Direction.North_West:
                    return new Integer_Vector_3(-1,1);
                default:
                    return new Integer_Vector_3(0,0);
            }
        }
#endregion
    }
}
