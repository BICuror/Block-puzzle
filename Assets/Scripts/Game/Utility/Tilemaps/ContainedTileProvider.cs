using Model;
using System;

namespace Utility
{
    public static class ContainedTileProvider
    {
        public static IContainedTile Get(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty: return new EmptyTile();
                case TileType.Solid: return new SolidTile();
                case TileType.Red: return new RedTile();
                case TileType.Green: return new GreenTile();
                case TileType.Blue: return new BlueTile();
            }

            throw new NotImplementedException($"{tileType} can't be instantiated");
        }
    }
}

