using UnityEngine;
using Model;
using Zenject;
using ViewModel;

public sealed class EnrtyPoint : MonoBehaviour
{
    [SerializeField] private TilemapGridDataSetter _tilemapGridDataSetter;
    [Inject] private PartRestockerModel _partRestockerModel;
    [Inject] private PartRestockerViewModel _partRestockerViewModel;

    [SerializeField] private GridData _gridData;

    private void Start()
    {
        _tilemapGridDataSetter.SetRandomTilemap();

        _partRestockerModel.CreatePartContainerModels();

        _partRestockerViewModel.CreatePartContainerViewModels();
    }
}
