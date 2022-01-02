
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Load_Asset<Handle> :
    SA__Load_Asset_Base
    where Handle : Distinct_Handle
    {
        public Handle Load_Asset__Handle { get; internal set; }

        public SA__Load_Asset(string filename)
        : base(filename)
        {
        }
    }
}
