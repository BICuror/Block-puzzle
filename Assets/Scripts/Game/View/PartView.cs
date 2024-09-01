using UnityEngine;
using ViewModel;
using UniRx;

namespace View
{
    public sealed class PartView : MonoBehaviour
    {
        [SerializeField] private Transform _tileParent;
        private CompositeDisposable _disposable = new();
        private PartViewModel _partViewModel;
        
        public void Init(PartViewModel partViewModel)
        {
            _partViewModel = partViewModel;
        
            _partViewModel.PartDestroyed += DestroyPart;

            _partViewModel.CenterPosition.Subscribe(SetTileParentPosition).AddTo(_disposable);
        }
        
        public Transform TileParent => _tileParent;
        public PartViewModel ViewModel => _partViewModel;
        
        public void DestroyPart()
        {
            Destroy(gameObject);
        }

        private void SetTileParentPosition(Vector2 position)
        {
            Vector3 newPosition = new Vector3(-position.x, -position.y, 0f);

            _tileParent.localPosition = newPosition;
        }
    }
}

