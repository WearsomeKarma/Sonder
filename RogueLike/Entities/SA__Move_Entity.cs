using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public sealed class SA__Move_Entity<T> :
        SA__Chronical
        where T : Entity
    {
        internal T Move_Entity__ENTITY__Internal { get; }
        internal Integer_Vector_3 Move_Entity__POSITION__Internal { get; }
    
        public SA__Move_Entity(SA__Control_Entity<T> e, Integer_Vector_3 position)
        : base (e)
        {
            Move_Entity__ENTITY__Internal = e.Control_Entity__ENTITY;
            Move_Entity__POSITION__Internal = position;
        }
    }
}
