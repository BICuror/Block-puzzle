using System;
using UniRx;
using UnityEngine;

namespace Model
{
    public sealed class TileModel
    {
        private IContainedTile _containedTile;

        public event Action<IContainedTile> ContainedTileUpdated;
        public event Action TileDestoryed;

        public ReactiveProperty<Vector2Int> TilePosition { get; private set; } = new (new Vector2Int(-1, -1));
        public ReactiveProperty<bool> Selected { get; private set; } = new();
        public IContainedTile ContainedTile => _containedTile;

        public void SetTile(IContainedTile tileContained)
        {
            _containedTile = tileContained;

            ContainedTileUpdated?.Invoke(_containedTile);
        }

        public void SetSelectionState(bool state) => Selected.Value = state;

        public void SetPosition(Vector2Int newPosition) => TilePosition.Value = newPosition;

        public void DestroyTile()
        {
            TileDestoryed.Invoke();   
        }
    }
}
