using System;
using Model;

namespace Utility
{
    public static class TileTypeSwitcher
    {
        private static TileType[] _coloredTileTypes = new TileType[4]
        {
            TileType.Empty,
            TileType.Red,
            TileType.Green,
            TileType.Blue
        };

        private static TileType[] _solidTileTypes = new TileType[2]
        {
            TileType.Empty,
            TileType.Solid
        };

        public static TileType SwitchTileType(TileType tileType)
        {
            return SwitchTileType(tileType, GridDataType.Default);
        }

        public static TileType SwitchTileType(TileType tileType, GridDataType gridDataType)
        {
            switch (gridDataType)
            {
                case GridDataType.Default: return SwitchTileOnDefaultGrid(tileType);
                case GridDataType.Colored: return SwitchTileOnColoredtGrid(tileType);
                case GridDataType.Solid: return SwitchTileOnSolidGrid(tileType);
                default: throw new NotImplementedException();
            }
        }

        private static TileType SwitchTileOnDefaultGrid(TileType tileType)
        {
            if ((int)tileType + 1 == Enum.GetNames(typeof(TileType)).Length)
            {
                return (TileType)0;
            }   
            else 
            {
                return (TileType)((int)tileType + 1);
            }   
        }

        private static TileType SwitchTileOnColoredtGrid(TileType tileType)
        {
            return SwitchThroughArray(tileType, _coloredTileTypes);
        }

        private static TileType SwitchTileOnSolidGrid(TileType tileType)
        {
            return SwitchThroughArray(tileType, _solidTileTypes);
        }

        private static TileType SwitchThroughArray(TileType tileType, TileType[] tileTypes)
        {
            int arrayIndex = Array.IndexOf(tileTypes, tileType);

            if (arrayIndex == -1) return tileTypes[0];
        
            if (arrayIndex + 1 < tileTypes.Length)
            {
                return tileTypes[arrayIndex + 1];
            }
            else 
            {
                return tileTypes[0];
            }
        }
    }
}

