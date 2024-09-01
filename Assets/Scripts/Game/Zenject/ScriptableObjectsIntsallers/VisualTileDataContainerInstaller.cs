using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Data/VisualTileDataContainer")]

    public sealed class VisualTileDataContainerInstaller : ScriptableObjectInstaller<VisualTileDataContainerInstaller> 
    {
        [SerializeField] private VisualTileDataContainer _visualTileDataContainer;

        public override void InstallBindings()
        {
            Container.Bind<VisualTileDataContainer>().FromInstance(_visualTileDataContainer).AsSingle().NonLazy();
        }
    }
}
