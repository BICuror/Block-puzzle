using UnityEngine;
using View;

[CreateAssetMenu(fileName = "PartPrefabContainer", menuName = "Containers/Prefabs/PartPrefabContainer")]

public class PartPrefabContainer : ScriptableObject
{
    [SerializeField] private PartView _partPrefab;

    public PartView PartPrefab => _partPrefab;
}
