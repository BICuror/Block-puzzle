using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Data/PartGridDatasContainerInstaller")]
    
    public sealed class PartGridDatasContainerInstaller : ScriptableObjectInstaller<PartGridDatasContainerInstaller> 
    {
        [SerializeField] private PartGridDatasContainer _partGridDatasContainer;

        public override void InstallBindings()
        {
            Container.Bind<PartGridDatasContainer>().FromInstance(_partGridDatasContainer).AsSingle().NonLazy();
        }
    }
}