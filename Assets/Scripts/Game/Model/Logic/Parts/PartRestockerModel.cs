using UniRx;

namespace Model
{
    public sealed class PartRestockerModel
    {
        private PartContainerModel[] _partContainerModels;
        private int _partContainersAmount;
        
        public PartRestockerModel(int partContainersAmount)
        {
            _partContainersAmount = partContainersAmount;

            _restockConditionDelegate = AreAllContainersEmpty;
        }
        
        private delegate bool RestockConditionDelegate(PartContainerModel[] partContainerModels); 
        private RestockConditionDelegate _restockConditionDelegate;

        public PartContainerModel[] PartContainerModels => _partContainerModels;
        
        public void SetAllContainersState(PartContainerState state)
        {
            for (int i = 0; i < _partContainerModels.Length; i++)
            {
                _partContainerModels[i].SetState(state);
            }
        }

        public void CreatePartContainerModels()
        {
            _partContainerModels = new PartContainerModel[_partContainersAmount];

            for (int i = 0; i < _partContainerModels.Length; i++)
            {
                _partContainerModels[i] = new();
            }

            foreach (var partContainerModel in _partContainerModels)
            {
                partContainerModel.CurrentState.Subscribe(_ => {CheckContainersStateToRefill();});
            }
        }

        private void CheckContainersStateToRefill()
        {
            if (_restockConditionDelegate(_partContainerModels))
            {
                foreach (var partContainerModel in _partContainerModels)
                {
                    partContainerModel.SetState(PartContainerState.Full);
                }
            }
        }

        private bool AreAllContainersEmpty(PartContainerModel[] partContainerModels)
        {
            foreach (var partContainerModel in partContainerModels)
            {
                if (partContainerModel.CurrentState.Value != PartContainerState.Empty) return false;
            }

            return true;
        }
    }
}

