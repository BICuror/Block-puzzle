using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Data/TilemapGridDatasContainerInstaller")]
    
    public sealed class TilemapGridDatasContainerInstaller : ScriptableObjectInstaller<TilemapGridDatasContainerInstaller> 
    {
        [SerializeField] private TilemapGridDatasContainer _tilemapGridDatasContainer;

        public override void InstallBindings()
        {
            Container.Bind<TilemapGridDatasContainer>().FromInstance(_tilemapGridDatasContainer).AsSingle().NonLazy();
        }
    }
}