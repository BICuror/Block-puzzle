using Zenject;
using Model;
using ViewModel;
using UnityEngine;

namespace Installers
{
    public sealed class PartSystemInstaller : MonoBehaviour
    {
        [SerializeField] private PartContainerViewContainer _partContainerViewContainer;

        public void InstallBindings(DiContainer container, PartFactory partFactory)
        {
            container.Bind<PartContainerViewContainer>().FromInstance(_partContainerViewContainer).AsSingle().NonLazy();
            
            PartRestockerModel partRestockerModel = new(_partContainerViewContainer.Length);
            PartRestockerViewModel partRestockerViewModel = new(partRestockerModel, _partContainerViewContainer, partFactory);
            
            container.Bind<PartRestockerModel>().FromInstance(partRestockerModel).AsSingle().NonLazy();
            container.Bind<PartRestockerViewModel>().FromInstance(partRestockerViewModel).AsSingle().NonLazy();
        }
    }
}
