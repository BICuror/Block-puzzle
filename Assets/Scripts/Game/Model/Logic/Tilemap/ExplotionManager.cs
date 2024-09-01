using System;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace Model
{
    public class ExplotionManager
    {
        private int _neededTilesForExplotions = 4;
        private int _milisecondsPerExplotionStep = 150;
        private TilemapModel _tilemapModel;
        private bool[,] _touchTilemap;
        private Vector2Int[] _checkDirections = new Vector2Int[4]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        
        public ExplotionManager(TilemapModel tilemapModel)
        {
            _tilemapModel = tilemapModel;
        }

        public Action<int> ExplotionStreakStopped;
        
        private TilemapValidator _tilemapValidator => _tilemapModel.TilemapValidator;
    
        public async void TryToExplodeTile(PartModel partModel, Vector2Int offset)
        {
            for (int i = 0; i < partModel.TilePositions.Length; i++)
            { 
                await TryToExplode(partModel.TilePositions[i] + offset);
            }
        }

        public async Task TryToExplode(Vector2Int position)
        {
            int foundTiles;
            
            ResetTouchTilemap();

            foundTiles = RecurseSearch(position);

            ResetTouchTilemap();

            if (foundTiles >= _neededTilesForExplotions)
            {
                await ExplodeTiles(position);
            }
        }

        private int RecurseSearch(Vector2Int position)
        {
            int foundTiles = 1;

            _touchTilemap[position.x, position.y] = true;

            for (int i = 0; i < _checkDirections.Length; i++)
            {
                Vector2Int checkPosition = _checkDirections[i] + position;

                if (TileShouldExplode(position, checkPosition))
                {
                    foundTiles += RecurseSearch(checkPosition);    
                }
            }

            return foundTiles;        
        }

        private async Task ExplodeTiles(Vector2Int startPosition)
        {
            int explodedTiles = await RecurciveExplode(startPosition); 

            ExplotionStreakStopped.Invoke(explodedTiles);
        }

        private async Task<int> RecurciveExplode(Vector2Int position)
        {
            int explodedTiles = 1;

            _touchTilemap[position.x, position.y] = true;

            for (int i = 0; i < _checkDirections.Length; i++)
            {
                Vector2Int checkPosition = _checkDirections[i] + position;

                if (TileShouldExplode(position, checkPosition))
                {
                    explodedTiles += await RecurciveExplode(checkPosition);    
                }
            }  
            
            _tilemapModel.SetTile(position, new EmptyTile());
            
            await Task.Delay(_milisecondsPerExplotionStep);
            
            return explodedTiles;
        }

        private void ResetTouchTilemap()
        {
            _touchTilemap = new bool[_tilemapModel.Width.Value, _tilemapModel.Height.Value];
        }

        private bool TileShouldExplode(Vector2Int mainPosition, Vector2Int checkPosition)
        {
            if (TileShouldBeChecked(checkPosition) == false) return false;

            return TilesMatch(mainPosition, checkPosition);
        }
        
        private bool TileShouldBeChecked(Vector2Int position)
        {
            if (_tilemapValidator.CheckPositionValidity(position) == false) return false; 
            
            return _touchTilemap[position.x, position.y] == false;
        }

        private bool TilesMatch(Vector2Int mainPosition, Vector2Int checkPosition)
        {
            IContainedTile mainTile = _tilemapModel.GetTile(mainPosition);

            IContainedTile checkTile = _tilemapModel.GetTile(checkPosition);

            return mainTile.MathcesWith(checkTile.GetTileType());
        }
    }
}

