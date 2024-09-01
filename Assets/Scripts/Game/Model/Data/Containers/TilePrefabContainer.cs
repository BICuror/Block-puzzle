using UnityEngine;
using View;

[CreateAssetMenu(fileName = "TilePrefabContainer", menuName = "Containers/Prefabs/TilePrefabContainer")]

public sealed class TilePrefabContainer : ScriptableObject 
{
    [SerializeField] private TileView _tilePrefab;

    public TileView TilePrefab => _tilePrefab;
}
