using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualTileDataContainer", menuName = "Containers/Visuals/VisualTileDataContainer")]

public sealed class VisualTileDataContainer : ScriptableObject 
{
    [SerializeField] private VisualTileData[] _visualTileDatas;

    public VisualTileData GetTileData(TileType tileType)
    {
        return _visualTileDatas.Single(visualData => visualData.TileType == tileType);
    }

    private void OnValidate()
    {
        foreach (TileType tileType in Enum.GetValues(typeof(TileType)))
        {
            try
            {
                VisualTileData visualTileData = GetTileData(tileType);
            }
            catch 
            {
                Debug.LogError($"Missing or multiple VisualTileData of type {tileType} in VisualTileDataContainer");
            }
        }
    }
}