using UnityEngine;
using Zenject;
using Model;
using ViewModel;

namespace Installers
{
    public class ScoringSystemInstaller : MonoBehaviour
    {
        public void InstallBindings(DiContainer container, ExplotionManager explotionManager)
        {
            ScoreCounterModel scoreCounterModel = new(explotionManager);
            ScoreCounterViewModel scoreCounterViewModel = new(scoreCounterModel);
            
            container.Bind<ScoreCounterModel>().FromInstance(scoreCounterModel).AsSingle().NonLazy();
            container.Bind<ScoreCounterViewModel>().FromInstance(scoreCounterViewModel).AsSingle().NonLazy();
        }
    }
}