using Model;
using UniRx;
using UnityEngine;
using System;

namespace ViewModel
{
    public sealed class TileViewModel
    {
        private CompositeDisposable _disposable = new();
        private TileModel _model;
        private VisualTileDataContainer _visualTileDataContainer;
        
        public TileViewModel(TileModel model, VisualTileDataContainer visualTileDataContainer)
        {   
            _model = model;

            _model.ContainedTileUpdated += UpdateVisualTileData; 
            
            _model.TilePosition.Subscribe(SetPosition).AddTo(_disposable);

            _model.Selected.Subscribe(UpdateSelectedState).AddTo(_disposable);
            
            _model.TileDestoryed += DestroyTile;

            _visualTileDataContainer = visualTileDataContainer; 
        }

        public event Action TileDestoryed;
        
        public ReactiveProperty<Vector2Int> Position { get; private set; } = new(new Vector2Int(-1, -1));
        public ReactiveProperty<Color> TileColor { get; private set; } = new();
        public ReactiveProperty<Transform> Parent { get; private set; } = new();
        public ReactiveProperty<bool> Selected { get; private set; } = new();
        
        private void SetPosition(Vector2Int position) => Position.Value = position;
        
        public void SetParent(Transform parent) => Parent.Value = parent;

        private void UpdateVisualTileData(IContainedTile containedTile)
        {
            VisualTileData visualTileData = _visualTileDataContainer.GetTileData(containedTile.GetTileType());

            TileColor.Value = visualTileData.TileColor;
        }

        private void UpdateSelectedState(bool state) => Selected.Value = state;

        private void DestroyTile()
        {
            _model.ContainedTileUpdated -= UpdateVisualTileData;

            _model.TileDestoryed -= DestroyTile;

            _disposable.Dispose();
            
            TileDestoryed.Invoke();
        }
    }
}