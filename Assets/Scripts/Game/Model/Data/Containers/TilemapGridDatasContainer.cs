using UnityEngine;
using Model;

[CreateAssetMenu(fileName = "TilemapGridDatasContainer", menuName = "Containers/Data/TilemapGridDatasContainer")]

public sealed class TilemapGridDatasContainer : ScriptableObject
{
    [SerializeField] private GridData[] _tilemapGridDatas;

    public GridData GetRadnomGridData() => _tilemapGridDatas[Random.Range(0, _tilemapGridDatas.Length)];
}
