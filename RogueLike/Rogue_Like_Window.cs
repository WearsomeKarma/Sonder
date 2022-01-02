using Rogue_Like.Entities.Player_Entity;
using Xerxes_Engine.Export_OpenTK;
using Xerxes_Engine.Export_OpenTK.Exports.Graphics;
using Xerxes_Engine.Export_OpenTK.Exports.Graphics.R2;
using Xerxes_Engine.Export_OpenTK.Exports.Serialization;

namespace Rogue_Like
{
    public sealed class Rogue_Like_Window :
        Game
    {
        internal static Texture_R2 Font { get; private set; }

        public Rogue_Like_Window()
        {
            Declare__Streams()
                .Upstream   .Extending<SA__Load_Texture_R2>()
                .Upstream   .Extending<SA__Declare_Vertex_Object>();

            Declare__Export<Asset_Pipe>();
            Declare__Export<Render_Service>();
            Declare__Export<Vertex_Object_Library>();
            Declare__Export<Sprite_Library>();

            Declare__Hierarchy()
                .Associate<World>()
                .Focus__Descendant<World>()
                    .Associate<Entity_System>()
                    .Focus__Descendant<Entity_System>()
                        .Associate<Entity_Manager<Player>>()
                        .Focus__Descendant<Entity_Manager<Player>>()
                            .Associate<Player_Controller>();
        }

        protected override void Handle_Load__Content__Game()
        {
            SA__Load_Texture_R2 e1 =
                new SA__Load_Texture_R2
                (
                    "font.png"
                );

            Invoke__Ascending(e1);

            Texture_R2? font = (Texture_R2)e1.Load_Texture_R2__Texture;

            Font = (Texture_R2)font;
        }
    }
}
