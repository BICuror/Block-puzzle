using UnityEngine;

namespace Model
{
    public sealed class TilemapValidator
    {
        private TilemapModel _tilemapModel;

        public TilemapValidator(TilemapModel tilemapModel)
        {
            _tilemapModel = tilemapModel;
        }
        
        public bool TileCanBePlaced(IContainedTile tileToPlace, Vector2Int position)
        {
            if (CheckPositionValidity(position) == false) return false;

            return tileToPlace.CanBePlaced(_tilemapModel.GetTile(position).GetTileType());
        }

        public bool CheckPositionValidity(Vector2Int position)
        {
            return IsValidX(position.x) && IsValidY(position.y);
        }

        private bool IsValidX(int x) => x >= 0 && x < _tilemapModel.Width.Value;
        private bool IsValidY(int y) => y >= 0 && y < _tilemapModel.Height.Value;
    }
}

