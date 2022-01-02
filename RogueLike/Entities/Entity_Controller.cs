using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Entity_Controller<T> :
        Entity_Controller_Base
        where T : Entity
    {
        public Entity_Controller()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Control_Entity<T>>
                (Handle_Control__Entity__Entity_Controller)
                .Upstream  .Extending<SA__Move_Entity<T>>();
        }

        protected virtual void Handle_Control__Entity__Entity_Controller
        (SA__Control_Entity<T> e)
        {
            
        }

        protected void Move__Entity__Entity_Controller
        (SA__Control_Entity<T> e, Integer_Vector_3 position)
        {
            SA__Move_Entity<T> e1 =
                new SA__Move_Entity<T>
                (e, position);

            Invoke__Ascending
                (e1);
        }
    }
}
