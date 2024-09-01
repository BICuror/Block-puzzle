using UnityEngine;
using Zenject;

namespace Model 
{
    public sealed class TilemapGridDataSetter : MonoBehaviour
    {
        [Inject] private TilemapGridDatasContainer _tilemapGridDatasContainer;
        [Inject] private TilemapModel _tilemapModel;

        public void SetRandomTilemap()
        {
            TileType[,] tilemap = _tilemapGridDatasContainer.GetRadnomGridData().GetTileGrid();

            _tilemapModel.ApplyTilemapData(tilemap);
        }
    }
}

