using TMPro;
using UnityEngine;
using ViewModel;
using Zenject;
using UniRx;

namespace View
{
    public sealed class ScoreCounterView : MonoBehaviour
    {
        [Inject] private ScoreCounterViewModel _scoreCounterViewModel;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private CompositeDisposable _disposable = new();
        
        private void Start()
        {
            _scoreCounterViewModel.Score.Subscribe(SetScoreText).AddTo(_disposable);
        }

        private void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}

