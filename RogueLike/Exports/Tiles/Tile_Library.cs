
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Tile_Library :
        OpenTK_Export
    {
        private Tile_Dictionary Tile_Library__DICTIONARY { get; } 

        public Tile_Library()
        {
            Tile_Library__DICTIONARY =
                new Tile_Dictionary();
        }

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Declare__Receiving<SA__Get_Tile>
                (Private_Get__Tile__Tile_Library);
            Declare__Receiving<SA__Declare_Tile>
                (Private_Declare__Tile__Tile_Library);
        }

        private void Private_Get__Tile__Tile_Library(SA__Get_Tile e)
        {
            Tile tile = 
                Tile_Library__DICTIONARY.Get__Tile__Tile_Dictionary(e.Get_Tile__TILE_HANDLE);

            e.Get_Tile__Tile = tile;
        }

        private void Private_Declare__Tile__Tile_Library(SA__Declare_Tile e)
        {
            Tile_Handle handle = Tile_Library__DICTIONARY
                .Add__Tile__Tile_Dictionary(e.Declare_Asset__ASSET_ALIAS, e.Declare_Asset__ASSET);

            e.Declare_Asset__Asset_Handle = handle;
        }
    }
}
