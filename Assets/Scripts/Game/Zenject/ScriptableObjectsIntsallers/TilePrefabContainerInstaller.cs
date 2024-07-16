using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Prefabs/TilePrefabContainerInstaller")]

    public sealed class TilePrefabContainerInstaller : ScriptableObjectInstaller<TilePrefabContainerInstaller> 
    {
        [SerializeField] private TilePrefabContainer _tilePrefabContainer;

        public override void InstallBindings()
        {
            Container.Bind<TilePrefabContainer>().FromInstance(_tilePrefabContainer).AsSingle().NonLazy();
        }
    }
}
