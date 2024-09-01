using UnityEngine;
using ViewModel;
using UniRx;
using Utility;

namespace View
{
    [RequireComponent(typeof(SpriteRenderer))]
    
    public sealed class TileView : PoolObject
    {
        [SerializeField] private GameObject _selectionIndicator;
        private CompositeDisposable _disposable;
        private SpriteRenderer _spriteRenderer;
        private TileViewModel _tileViewModel;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Init(TileViewModel tileViewModel) 
        {
            _disposable = new();

            _tileViewModel = tileViewModel;
        
            _tileViewModel.Position.Subscribe(SetPosition).AddTo(_disposable);
            _tileViewModel.Parent.Subscribe(SetParent).AddTo(_disposable);
            _tileViewModel.TileColor.Subscribe(SetTileColor).AddTo(_disposable);
            _tileViewModel.Selected.Subscribe(UpdateSelectedState).AddTo(_disposable);

            _tileViewModel.TileDestoryed += DestroyTile;
        }
        
        protected override void ResetObject()
        {
            _disposable.Dispose();

            _tileViewModel = null;
        }
        
        private void SetPosition(Vector2Int position)
        {
            Vector3 newPosition = new Vector3(position.x, position.y, 0f) * transform.localScale.x;
        
            transform.localPosition = newPosition;
        }

        private void SetParent(Transform newParent) => transform.SetParent(newParent);

        private void SetTileColor(Color newColor) => _spriteRenderer.color = newColor;
        
        private void UpdateSelectedState(bool state) => _selectionIndicator.SetActive(state);

        private void DestroyTile()
        {
            _tileViewModel.TileDestoryed -= DestroyTile;

            ReturnObjectToPool();
        }
    }
}

