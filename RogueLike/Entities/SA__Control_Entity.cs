using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Control_Entity<T> :
        SA__Chronical
        where T : Entity
    {
        public T Control_Entity__ENTITY { get; }

        internal SA__Control_Entity(SA__Chronical e, T entity)
        : base(e)
        {
            Control_Entity__ENTITY = entity;
        }
    }
}
