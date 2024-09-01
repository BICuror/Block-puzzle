using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public sealed class TilemapSelectionManager
    {
        private TilemapModel _tilemapModel;
        private List<Vector2Int> _currentlySelectedTilePositions = new();

        public TilemapSelectionManager(TilemapModel tilemapModel)
        {
            _tilemapModel = tilemapModel;
        }

        public void SetSelectedTilePositions(List<Vector2Int> positions)
        {
            List<Vector2Int> disabledPositions = _currentlySelectedTilePositions.Except(positions).ToList();
            SetTileSelectionStates(disabledPositions, false);

            List<Vector2Int> enabledPositions = positions.Except(_currentlySelectedTilePositions).ToList();
            SetTileSelectionStates(enabledPositions, true);

            _currentlySelectedTilePositions = positions;
        }

        public void ClearSelectedTiles()
        {
            SetTileSelectionStates(_currentlySelectedTilePositions, false);
        
            _currentlySelectedTilePositions = new();
        }

        private void SetTileSelectionStates(List<Vector2Int> positions, bool state)
        {
            foreach (Vector2Int position in positions)
            {
                _tilemapModel.GetTileModel(position).SetSelectionState(state);
            }
        }
    }
}