using Model;
using UnityEngine;

namespace Utility
{
    public sealed class PartFlipper
    {
        public void MirrorX(PartModel partModel)
        {
            Vector2Int[] tilePositions = partModel.TilePositions;

            int width = GetWidth(partModel);

            Vector2Int[] newPosition = new Vector2Int[tilePositions.Length];

            for (int x = 0; x < tilePositions.Length; x++)
            {
                newPosition[x] = new Vector2Int(width - tilePositions[x].x, tilePositions[x].y);
            }

            partModel.SetTilePositions(newPosition);
        }

        private int GetWidth(PartModel partModel)
        {
            int maxWidth = 0;
                
            foreach (Vector2Int tilePosition in partModel.TilePositions)
            {
                maxWidth = Mathf.Max(maxWidth, tilePosition.x);
            }

            return maxWidth;
        }
    }
}

