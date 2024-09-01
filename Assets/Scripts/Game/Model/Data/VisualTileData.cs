using UnityEngine;

[CreateAssetMenu(fileName = "VisualTileData", menuName = "Data/VisualTileData")]

public sealed class VisualTileData : ScriptableObject 
{
    [SerializeField] private TileType _tileType;
    public TileType TileType => _tileType;

    [SerializeField] private Color _tileColor;
    public Color TileColor => _tileColor;    
}
