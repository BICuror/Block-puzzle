using System;
using Model;
using UnityEngine;
using UniRx;

namespace ViewModel
{
    public sealed class PartViewModel
    {   
        private CompositeDisposable _disposable = new();
        private PartModel _partModel;
        
        public PartViewModel(PartModel partModel)
        {
            _partModel = partModel;

            _partModel.PartDestoryed += DestroyPart;

            _partModel.CenterPosition.Subscribe(SetCenterPosition).AddTo(_disposable);
        }
        
        public event Action PartDestroyed;
        
        public PartModel Model => _partModel;
        public ReactiveProperty<Vector2> CenterPosition { get; private set; } = new();
        
        private void SetCenterPosition(Vector2 centerPosition) => CenterPosition.Value = centerPosition;

        private void DestroyPart()
        {
            PartDestroyed.Invoke();
        
            PartDestroyed = null;
        }
    }
}

