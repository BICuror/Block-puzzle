using Model;
using UniRx;
using UnityEngine;

namespace ViewModel
{
    public sealed class TilemapViewModel
    {
        private CompositeDisposable _disposable = new();
        private TileFactory _tileFactory;
        private TilemapModel _model;
        private TileViewModel[,] _tileViewMap; 
        
        public TilemapViewModel(TilemapModel model, TileFactory tileFactory)
        {
            _model = model;
            _tileFactory = tileFactory;

            _model.Height.Subscribe(SetHeight).AddTo(_disposable);
            _model.Width.Subscribe(SetWidth).AddTo(_disposable);

            _model.TileModelMap.Subscribe(GenerateTilemap).AddTo(_disposable);
        }   
        
        public IntReactiveProperty Height { get; private set; } = new();
        public IntReactiveProperty Width { get; private set; } = new();
        
        public void SetHeight(int height) => Height.Value = height;
        public void SetWidth(int width) => Width.Value = width;
        
        public void GenerateTilemap(TileModel[,] tileModelMap)
        {
            _tileViewMap = new TileViewModel[Width.Value, Height.Value];

            for (int x = 0; x < Width.Value; x++)
            {
                for (int y = 0; y < Height.Value; y++)
                {
                    TileViewModel newTile = _tileFactory.CreateTile(tileModelMap[x, y]);

                    tileModelMap[x, y].SetPosition(new Vector2Int(x, y));

                    _tileViewMap[x, y] = newTile;
                }
            }
        }
    }
}

