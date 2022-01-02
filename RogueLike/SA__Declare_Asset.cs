
using Xerxes_Engine;

namespace Rogue_Like
{
    public class SA__Declare_Asset<Handle, Asset> :
    Streamline_Argument
    where Handle : Distinct_Handle
    {
        public Handle Declare_Asset__Asset_Handle { get; internal set; }
        public string Declare_Asset__ASSET_ALIAS { get; }
        public Asset Declare_Asset__ASSET { get; }

        public SA__Declare_Asset
        (
            string alias,
            Asset asset
        )
        {
            Declare_Asset__ASSET_ALIAS = alias;
            Declare_Asset__ASSET = asset;
        }
    }
}   
