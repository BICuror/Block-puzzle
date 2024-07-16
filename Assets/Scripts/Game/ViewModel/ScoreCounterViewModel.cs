using Model;
using UniRx;

namespace ViewModel
{
    public sealed class ScoreCounterViewModel
    {
        private CompositeDisposable _disposable = new();
        private ScoreCounterModel _scoreCounterModel;
        
        public ScoreCounterViewModel(ScoreCounterModel scoreCounterModel)
        {
            _scoreCounterModel = scoreCounterModel;

            _scoreCounterModel.Score.Subscribe(SetScore).AddTo(_disposable);
        }

        public ReactiveProperty<int> Score { get; private set; } = new(); 

        private void SetScore(int value)
        {
            Score.Value = value;
        }
    }
}

