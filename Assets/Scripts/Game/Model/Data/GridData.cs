using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public struct Row
    {
        public TileType[] TileTypesRow;

        public Row(int Length)
        {
            TileTypesRow = new TileType[Length];
        }

        public void Set(int index, TileType value) => TileTypesRow[index] = value;
        public TileType Get(int index) => TileTypesRow[index];
        public int GetLength() => TileTypesRow.GetLength(0);
    }
    
    [CreateAssetMenu(fileName = "GridData", menuName = "Data/GridData")]
    public sealed class GridData : ScriptableObject
    {
        public int Width = 5;
        public int Height = 5;
        public Row[] TileGrid;
        public GridDataType GridDataType;
        
        private void OnValidate()
        {
            if (TileGrid.Length != Height || TileGrid[0].GetLength() != Width)
            {
                TransferTilesToNewGrid();
            }
        }

        public TileType[,] GetTileGrid()
        {
            TileType[,] tileGrid = new TileType[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    tileGrid[x, Height - 1 - y] = TileGrid[y].Get(x);
                }
            }

            return tileGrid;
        }
        
        public void FillTileGridIfEmpty()
        {
            if (TileGrid == null) ReinstantiateGrid();
        }
        
        public void ReinstantiateGrid()
        {
            TileGrid = new Row[Height];

            for (int x = 0; x < TileGrid.Length; x++)
            {
                TileGrid[x] = new Row(Width);
            }
        }

        public void TransferTilesToNewGrid()
        {
            Row[] copyTileGrid = TileGrid;

            ReinstantiateGrid();

            int copyWitdh = Mathf.Min(Width, copyTileGrid[0].GetLength());
            int copyHeight = Mathf.Min(Height, copyTileGrid.Length);

            for (int y = 0; y < copyHeight; y++)
            {
                for (int x = 0; x < copyWitdh; x++)
                {
                    TileGrid[y].Set(x, copyTileGrid[y].Get(x));
                }
            }
        }
    }

    public enum GridDataType
    {
        Default,
        Colored,
        Solid
    }
}
