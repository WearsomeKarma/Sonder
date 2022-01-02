
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Load_Asset_Base :
        Streamline_Argument
    {
        public string Load_Asset_Base__FILENAME { get; }

        public SA__Load_Asset_Base(string filename)
        {
            Load_Asset_Base__FILENAME = filename;
        }
    }
}
