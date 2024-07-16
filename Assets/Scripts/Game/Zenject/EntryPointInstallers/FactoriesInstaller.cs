using UnityEngine;
using Zenject;
using ViewModel;

namespace Installers
{
    public sealed class FactoriesInstaller : MonoBehaviour
    {
        [SerializeField] private TileFactory _tileFactory;
        [SerializeField] private PartFactory _partFactory;

        public TileFactory TileFactoryInstance => _tileFactory;
        public PartFactory PartFactoryInstance => _partFactory;

        public void InstallBindings(DiContainer container)
        {
            InstallTileFactory(container);
            InstallPartFactory(container);
        }

        private void InstallTileFactory(DiContainer container)
        {
            container.Bind<TileFactory>().FromInstance(_tileFactory).AsSingle().NonLazy();  
        }

        private void InstallPartFactory(DiContainer container)
        {
            container.Bind<PartFactory>().FromInstance(_partFactory).AsSingle().NonLazy();
        }
    }
}
