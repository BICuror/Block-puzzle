using System;
using UniRx;
using UnityEngine;

namespace Model
{
    public sealed class PartModel
    {
        private CompositeDisposable _disposable = new();
        private TileModel[] _tileModels;
        private Vector2Int[] _tilePositions;
        
        public event Action PartDestoryed;

        public TileModel[] TileModels => _tileModels;
        public Vector2Int[] TilePositions => _tilePositions; 
        public ReactiveProperty<Vector2> CenterPosition { get; private set; } = new(); 
        public ReactiveProperty<int> Width { get; private set; } = new();
        public ReactiveProperty<int> Height { get; private set; } = new();  

        public void SetTiles(TileModel[] tileModels) 
        {
            _tileModels = tileModels;      
        }

        public void SetTilePositions(Vector2Int[] tilePositions)
        {
            _tilePositions = tilePositions;

            for (int i = 0; i < _tilePositions.Length; i++)
            {
                _tileModels[i].SetPosition(_tilePositions[i]);
            }

            CalculateSize();
            CalculateCenterPosition();
        }

        private void CalculateSize()
        {
            int maxPositionX = 0;
            int maxPositionY = 0;
            
            foreach (Vector2Int tilePosition in _tilePositions)
            {
                maxPositionX = Mathf.Max(maxPositionX, tilePosition.x);
                maxPositionY = Mathf.Max(maxPositionY, tilePosition.y);
            }

            Width.Value = maxPositionX;
            Height.Value = maxPositionY;
        }

        private void CalculateCenterPosition()
        {
            CenterPosition.Value = new Vector2(Width.Value / 2f, Height.Value / 2f);
        }

        public void DestroyPart() 
        {
            foreach (TileModel tileModel in _tileModels)
            {
                tileModel.DestroyTile();
            }

            _disposable.Dispose();

            PartDestoryed.Invoke();
        }
    }
}

