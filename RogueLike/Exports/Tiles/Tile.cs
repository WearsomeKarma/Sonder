
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public struct Tile
    {
        public static readonly Tile TILE_NULL = new Tile();

        public Sprite Tile__SPRITE { get; internal set; }
        public bool Tile__Is_Unpassable { get; set; }

        public Tile(Sprite tile_sprite, bool unpassable = true)
        {
            Tile__SPRITE = tile_sprite;
            Tile__Is_Unpassable = unpassable;
        }
    }
}
