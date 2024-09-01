using Model;
using UnityEngine;

[CreateAssetMenu(fileName = "PartGridDatasContainer", menuName = "Containers/Data/PartGridDatasContainer")]

public sealed class PartGridDatasContainer : ScriptableObject
{
    [SerializeField] private GridData[] _partGridDatas;

    public GridData GetRadnomGridData() => _partGridDatas[Random.Range(0, _partGridDatas.Length)];
}
