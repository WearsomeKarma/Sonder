
using Xerxes_Engine;

namespace Rogue_Like
{
    public abstract class Asset_Pipeline<SA__Load, SA__Declare, Asset_Handle, Asset> :
    Xerxes_Object<Asset_Pipeline<SA__Load, SA__Declare, Asset_Handle, Asset>>
    where SA__Declare : SA__Declare_Asset<Asset_Handle, Asset>
    where SA__Load : SA__Load_Asset<Asset_Handle> 
    where Asset_Handle : Distinct_Handle
    {
        public Asset_Pipeline()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Load>(Private_Load__Asset__Asset_Pipeline)
                .Upstream.Extending<SA__Declare>();
        }

        private void Private_Load__Asset__Asset_Pipeline(SA__Load e)
        {
            Asset asset =
                Handle_Load__Asset__Asset_Pipeline(e);

            SA__Declare e_declare_asset =
                Handle_Formulate__Declare__Asset_Pipeline(e, asset);

            Invoke__Ascending(e_declare_asset);

            e.Load_Asset__Handle =
                e_declare_asset.Declare_Asset__Asset_Handle;
        }

        protected abstract Asset Handle_Load__Asset__Asset_Pipeline(SA__Load e);

        protected abstract SA__Declare Handle_Formulate__Declare__Asset_Pipeline(SA__Load e, Asset asset);
    
        protected virtual string Handle_Get__Asset_Alias__Asset_Pipeline(SA__Load e)
        {
            return e.Load_Asset_Base__FILENAME;
        }
    }
}
