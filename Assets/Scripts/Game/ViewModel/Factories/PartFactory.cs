using Model;
using View;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Utility;

namespace ViewModel
{
    public sealed class PartFactory : MonoBehaviour
    {
        [Inject] private PartGridDatasContainer _partGridDatasContainer;
        [Inject] private PartPrefabContainer _partPrefabContainerView;
        [Inject] private TileFactory _tileFactory;
        
        public PartViewModel CreatePart(Transform partContainer)
        {
            PartModel partModel = new();
            PartViewModel partViewModel = new(partModel);
            PartView partView = Instantiate(_partPrefabContainerView.PartPrefab, partContainer.transform.position, Quaternion.identity, partContainer);
            partView.Init(partViewModel);
            
            CreateTileModels(partModel, partView.TileParent);
            
            return partViewModel;
        } 

        private void CreateTileModels(PartModel partModel, Transform tileParent)
        {
            List<TileModel> tileModels = new();
            List<Vector2Int> tilePositions = new();
            
            TileType[,] tileGrid = _partGridDatasContainer.GetRadnomGridData().GetTileGrid();

            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                for (int y = 0; y < tileGrid.GetLength(1); y++)
                {
                    if (tileGrid[x, y] == TileType.Empty) continue;
                    
                    Vector2Int newPosition = new Vector2Int(x, y);

                    tilePositions.Add(newPosition);
                        
                    tileModels.Add(CreateTileModel(tileParent, tileGrid[x, y]));
                } 
            }

            partModel.SetTiles(tileModels.ToArray());
            partModel.SetTilePositions(tilePositions.ToArray());
        }   

        private TileModel CreateTileModel(Transform tileParent, TileType tileType)
        {
            TileModel tileModel = new();
            TileViewModel tileViewModel = _tileFactory.CreateTile(tileModel);

            tileModel.SetTile(ContainedTileProvider.Get(tileType));

            tileViewModel.SetParent(tileParent);

            return tileModel;
        }
    }
}

