using System.Collections.Generic;
using UnityEngine;
using View;
using Zenject;

namespace Model
{
    public sealed class PartDragController : MonoBehaviour
    {
        [Inject] private TilemapSelectionManager _tilemapSelectionManager;
        [Inject] private TilemapModel _tilemapModel;
        [Inject] private ExplotionManager _explotionManager;
        [SerializeField] private Camera _camera;
        private Transform _partTransform;
        private PartModel _partModel;
        private Vector2 _dragOffset;
        private Vector2Int _lastRoundedTouchPosition;
        private ContorllerState _currentState = ContorllerState.Idle;

        private enum ContorllerState
        {
            Idle,
            Dragging
        };

        private void Update()
        {
            switch(_currentState)
            {
                case ContorllerState.Idle:
                {
                    TryToPickUpPart();
                }   break;
                
                case ContorllerState.Dragging: 
                {
                    DragPart();
                    TryToPlacePart();
                }   break;
            }
        }

        #region PickingUp
        private void TryToPickUpPart()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUpPart();
            }
        }

        private void PickUpPart()
        {
            RaycastHit2D hit = Physics2D.CircleCast(_camera.ScreenToWorldPoint(Input.mousePosition), 1f, Vector2.up);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<PartView>(out PartView partView))
                {
                    _partModel = partView.ViewModel.Model;
                    _partTransform = partView.transform;

                    _dragOffset = (Vector2)_partTransform.position - GetTouchPosition();

                    _currentState = ContorllerState.Dragging;
                }
            }
        }
        #endregion
        
        #region Dragging
        
        private void DragPart()
        {
            _partTransform.position = GetTouchPosition() + _dragOffset;
                
            Vector2Int roundedTouchPosition = GetRoundedPartCenterPosition();

            if (TouchPositionChanged(roundedTouchPosition) == false) return;
            
            bool isValidPotentialPlacementPosition = true;

            _lastRoundedTouchPosition = roundedTouchPosition;

            List<Vector2Int> potentialTilePlacementPositions = new();

            for (int i = 0; i < _partModel.TilePositions.Length; i++)
            {   
                Vector2Int tilePosition = _partModel.TilePositions[i] + roundedTouchPosition;
                
                if (_tilemapModel.TilemapValidator.TileCanBePlaced(_partModel.TileModels[i].ContainedTile, tilePosition))
                {
                    potentialTilePlacementPositions.Add(tilePosition);
                }
                else 
                {
                    isValidPotentialPlacementPosition = false;
                    break;
                }
            }

            if (isValidPotentialPlacementPosition)
            {
                _tilemapSelectionManager.SetSelectedTilePositions(potentialTilePlacementPositions);
            }
            else 
            {
                _tilemapSelectionManager.ClearSelectedTiles();
            }
        }

        #endregion

        #region Placing
    
        private void TryToPlacePart()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _tilemapSelectionManager.ClearSelectedTiles();
                
                if (CheckIfPossibleToPlace())
                {
                    PlacePart();
                }
        
                _partTransform.localPosition = Vector3.zero;

                _partTransform = null;

                _currentState = ContorllerState.Idle;
            }
        }

        private bool CheckIfPossibleToPlace()
        {
            Vector2Int roundedTouchPosition = GetRoundedPartCenterPosition();

            for (int i = 0; i < _partModel.TilePositions.Length; i++)
            {   
                Vector2Int tilePosition = _partModel.TilePositions[i] + roundedTouchPosition;

                if (_tilemapModel.TilemapValidator.TileCanBePlaced(_partModel.TileModels[i].ContainedTile, tilePosition) == false)
                {
                    _partTransform.localPosition = Vector3.zero;
                    return false;
                }
            }
            
            return true;
        }

        private void PlacePart()
        {
            Vector2Int roundedTouchPosition = GetRoundedPartCenterPosition();

            for (int i = 0; i < _partModel.TilePositions.Length; i++)
            {   
                _tilemapModel.SetTile(_partModel.TilePositions[i] + roundedTouchPosition, _partModel.TileModels[i].ContainedTile);
            }

            _explotionManager.TryToExplodeTile(_partModel, roundedTouchPosition);

            _partModel.DestroyPart();
        }
        #endregion

        private Vector2 GetTouchPosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private Vector2Int GetRoundedPartCenterPosition()
        {
            Vector2 center = GetTouchPosition() - _partModel.CenterPosition.Value + _dragOffset;

            return new Vector2Int(Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.y));
        }

        private bool TouchPositionChanged(Vector2Int position) => position != _lastRoundedTouchPosition;
    }
}
