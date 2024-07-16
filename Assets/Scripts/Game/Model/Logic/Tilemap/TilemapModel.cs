using UniRx;
using UnityEngine;
using Utility;

namespace Model
{
    public sealed class TilemapModel
    {
        private int _height, _width;
        private TileModel[,] _tileModelMap;
        private TilemapValidator _tilemapValidator;

        public TilemapModel()
        {
            _tilemapValidator = new TilemapValidator(this);
        }

        public TilemapValidator TilemapValidator => _tilemapValidator;
        public IntReactiveProperty Height { get; private set; } = new();
        public IntReactiveProperty Width { get; private set; } = new();
        public ReactiveProperty<TileModel[,]> TileModelMap { get; private set; } = new();

        public TileModel GetTileModel(Vector2Int position)
        {
            return _tileModelMap[position.x, position.y];
        }

        public IContainedTile GetTile(Vector2Int position)
        {
            return _tileModelMap[position.x, position.y].ContainedTile;
        }

        public void SetTile(Vector2Int position, IContainedTile containedTile)
        {
            _tileModelMap[position.x, position.y].SetTile(containedTile); 
        }
        
        public void ApplyTilemapData(TileType[,] tilemapData) 
        {
            if (_tileModelMap != null) DestroyTilesInTileMap();
            
            SetWidth(tilemapData.GetLength(0));
            SetHeight(tilemapData.GetLength(1));

            GenerateTileModelMap();
            
            PasteTileModelMap(tilemapData);
        }
        
        private void PasteTileModelMap(TileType[,] tilemapData)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    IContainedTile containedTile = ContainedTileProvider.Get(tilemapData[x, y]);

                    _tileModelMap[x, y].SetTile(containedTile);
                }
            }   
        }

        private void SetWidth(int width)
        {
            _width = width;
            Width.Value = width;
        }

        private void SetHeight(int height)
        {
            _height = height;
            Height.Value = height;
        }

        private void DestroyTilesInTileMap()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tileModelMap[x, y].DestroyTile();
                }
            }
        }

        private void GenerateTileModelMap()
        {
            _tileModelMap = new TileModel[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tileModelMap[x, y] = new TileModel();
                }
            }
            
            TileModelMap.Value = _tileModelMap;
        }
    }
}

