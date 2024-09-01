using UnityEngine;
using View;
using Model;
using Zenject;
using Utility;

namespace ViewModel
{
    [RequireComponent(typeof(TileViewPool))]

    public sealed class TileFactory : MonoBehaviour
    {
        [Inject] private TilePrefabContainer _tilePrefabContainer;
        [Inject] private VisualTileDataContainer _visualTileDataContainer;
        [SerializeField] private Transform _tilemapParent;
        private TileViewPool _tileViewPool;

        private void Awake()
        {
            _tileViewPool = GetComponent<TileViewPool>();

            _tileViewPool.InstantiatePool(_tilePrefabContainer.TilePrefab);
        }

        public TileViewModel CreateTile(TileModel tileModel)
        {    
            TileViewModel tileViewModel = new(tileModel, _visualTileDataContainer); 

            TileView tileView = _tileViewPool.GetPooledObject();

            tileView.gameObject.SetActive(true);
            
            tileView.Init(tileViewModel);

            tileViewModel.SetParent(_tilemapParent);

            return tileViewModel;
        }
    }
}

