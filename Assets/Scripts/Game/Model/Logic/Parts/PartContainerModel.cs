using UniRx;

namespace Model
{
    public sealed class PartContainerModel
    {
        private PartModel _partModel;
    
        public ReactiveProperty<PartContainerState> CurrentState {get; private set;} = new();
        public PartModel CurrentPartModel => _partModel;

        public void SetState(PartContainerState state)
        {
            if (CurrentState.Value == state) return;

            CurrentState.Value = state;
        }

        public void SetPart(PartModel partModel) => _partModel = partModel;
        
        public void ResetPart() => _partModel = null;
    }
}

