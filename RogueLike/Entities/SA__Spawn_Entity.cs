using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public sealed class SA__Spawn_Entity<T> :
        SA__Chronical
        where T : Entity
    {
        public Integer_Vector_3 Spawn_Entity__Position { get; internal set; }

        public SA__Spawn_Entity
        (
            SA__Chronical e,
            Integer_Vector_3? position
        )
        : base(e)
        {
            Spawn_Entity__Position = position ?? new Integer_Vector_3();
        }
    }
}
