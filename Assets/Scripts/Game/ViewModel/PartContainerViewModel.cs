using Model;
using View;
using UniRx;
using System;

namespace ViewModel
{
    public sealed class PartContainerViewModel
    {
        private PartContainerModel _partContainerModel;
        private PartContainerView _partContainerView;
        private PartFactory _partFactory;
        private PartViewModel _currentPart;

        public PartContainerViewModel(PartContainerModel partContainerModel, PartContainerView partContainerView, PartFactory partFactory)
        {
            _partContainerModel = partContainerModel;
            _partContainerView = partContainerView;
            _partFactory = partFactory;
        
            _partContainerModel.CurrentState.Subscribe(UpdateContainerState);
        }

        private void UpdateContainerState(PartContainerState partContainerState)
        {
            switch (partContainerState)
            {
                case PartContainerState.Empty: break;
                case PartContainerState.Full: CreatePart(); break;
                default: throw new NotImplementedException();
            }
        }

        private void CreatePart()
        {
            _currentPart = _partFactory.CreatePart(_partContainerView.PartParent);

            _partContainerModel.SetPart(_currentPart.Model);

            _currentPart.PartDestroyed += OnPartDestroyed;
        }

        private void OnPartDestroyed()
        {
            _currentPart.PartDestroyed -= OnPartDestroyed;

            _currentPart = null;

            _partContainerModel.ResetPart();
        
            _partContainerModel.SetState(PartContainerState.Empty);
        }
    }
}

