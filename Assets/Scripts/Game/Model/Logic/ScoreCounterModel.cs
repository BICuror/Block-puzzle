using UniRx;

namespace Model
{
    public sealed class ScoreCounterModel
    {
        private int _currentScore;
        
        public ScoreCounterModel(ExplotionManager explotionManager)
        {
            explotionManager.ExplotionStreakStopped += AddScore; 
        }
        
        public ReactiveProperty<int> Score { get; private set; } = new(); 
        
        public void AddScore(int value)
        {
            _currentScore += value;

            Score.Value = _currentScore;
        }
    }
}

