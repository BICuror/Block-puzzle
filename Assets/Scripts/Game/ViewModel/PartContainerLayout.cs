using Model;
using UniRx;
using UnityEngine;
using View;
using Zenject;

namespace ViewModel
{
    public sealed class PartContainerLayout : MonoBehaviour
    {
        [Inject] private PartContainerViewContainer _partContainerViewContainer;
        [Inject] private TilemapModel _tilemapModel;
        [SerializeField] private float _gapWidth = 1f;
        [SerializeField] private float _partContainerWidth = 3f;
        [SerializeField] private float _heightOffset = -2.5f;
        private CompositeDisposable _disposable = new();
        
        private void Start()
        {
            _tilemapModel.Width.Subscribe(AjustPartContainersPositions).AddTo(_disposable);    
        }

        private void AjustPartContainersPositions(int tilemapWidth)
        {
            PartContainerView[] partContainers = _partContainerViewContainer.Get();
            
            int containersAmount = partContainers.Length;
            
            float tilemapCenterX = tilemapWidth / 2;

            float sequenceLength = (_gapWidth + _partContainerWidth) * containersAmount - _gapWidth;

            float startingXPosition = tilemapCenterX - sequenceLength / 2;

            float curretXPosition = startingXPosition;

            for (int i = 0; i < containersAmount; i++)
            {
                float conatinerXPosition = curretXPosition + _partContainerWidth / 2;

                curretXPosition += _gapWidth + _partContainerWidth;

                partContainers[i].transform.localPosition = new Vector3(conatinerXPosition, _heightOffset, 0f);
            }
        }
    }
}

