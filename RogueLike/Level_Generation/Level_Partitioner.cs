
using Xerxes_Engine;

namespace Rogue_Like
{
    public abstract class Level_Partitioner :
        Xerxes_Object<Level_Partitioner>
    {
        public Level_Partitioner()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Level_Partitioning>
                (Handle_Partitioning__Level_Partitioner);
        }

        protected abstract void Handle_Partitioning__Level_Partitioner
        (SA__Level_Partitioning e);
    }
}
