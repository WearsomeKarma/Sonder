using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public sealed class SA__Draw_Entity :
        SA__Draw
    {
        public Entity Draw_Entity__ENTITY { get; }
        internal SA__Render Draw_Entity__RENDER_ARGUMENT { get; }

        public SA__Draw_Entity
        (SA__Render e, Entity entity)
        : base(e)
        {
            Draw_Entity__ENTITY = entity;
            Draw_Entity__RENDER_ARGUMENT = e;
        }
    }
}
