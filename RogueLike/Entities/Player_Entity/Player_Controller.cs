using OpenTK.Input;
using Xerxes_Engine.Export_OpenTK.Exports.Input;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like.Entities.Player_Entity
{
    public sealed class Player_Controller :
        Entity_Controller<Player>
    {
        internal static Vertex_Object_Handle Player_VO { get; private set; }

        private Direction _Player_Controller__Input_Movement { get; set; }

        public Player_Controller()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Input_Key_Down>
                (
                    Private_Handle__Input__Player_Controller
                )
                .Upstream  .Extending<SA__Declare_Vertex_Object>()
                .Downstream.Receiving<SA__Sealed_Under_Game>(Private_Seal__Under_Game__Player_Controller);
        }

        private void Private_Seal__Under_Game__Player_Controller
        (SA__Sealed_Under_Game e)
        {
            SA__Declare_Vertex_Object e1 =
                new SA__Declare_Vertex_Object
                (
                    Rogue_Like_Window.Font,
                    String_Batcher.CHAR_PIXEL_WIDTH,
                    String_Batcher.CHAR_PIXEL_HEIGHT,
                    new Integer_Vector_2[] { new Integer_Vector_2(69,0) }
                );

            Invoke__Ascending
                (e1);

            Player_VO = e1.Declare_Vertex_Object__Vertex_Object_Handle;
        }

        protected override void Handle_Control__Entity__Entity_Controller
        (SA__Control_Entity<Player> e)
        {
            Private_Check__Movement__Player(e);
        }

        private void Private_Handle__Input__Player_Controller
        (SA__Input_Key_Down e)
        {
            switch(e.Input_Keyboard__KEY)
            {
                case Key.W:
                    _Player_Controller__Input_Movement = Direction.North;
                    break;
                case Key.A:
                    _Player_Controller__Input_Movement = Direction.West;
                    break;
                case Key.S:
                    _Player_Controller__Input_Movement = Direction.South;
                    break;
                case Key.D:
                    _Player_Controller__Input_Movement = Direction.East;
                    break;
            }
        }

        private void Private_Check__Movement__Player
        (SA__Control_Entity<Player> e)
        {
            if (_Player_Controller__Input_Movement == Direction.Center)
                return;

            Integer_Vector_3 offset = World.Get__Offset(_Player_Controller__Input_Movement);
            _Player_Controller__Input_Movement = Direction.Center;

            Move__Entity__Entity_Controller(e, e.Control_Entity__ENTITY.Entity__Position + offset);
        }
    }
}
