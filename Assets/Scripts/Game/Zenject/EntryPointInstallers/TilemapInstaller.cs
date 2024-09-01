using Model;
using ViewModel;
using Zenject;

namespace Installers
{
    public sealed class TilemapInstaller : MonoInstaller
    {
        private TilemapModel _tilemapModelInstance;
        private ExplotionManager _explotionManager;

        public TilemapModel TilemapModelInstance => _tilemapModelInstance; 
        public ExplotionManager ExplotionManagerInstance => _explotionManager;

        public void InstallBindings(DiContainer container, TileFactory tileFactory)
        {
            InstalTilemap(container, tileFactory);

            InstallExplotionManager(container);

            InstallTilemapSelectionManager(container);
        }

        private void InstalTilemap(DiContainer container, TileFactory tileFactory)
        {
            _tilemapModelInstance = new();
            TilemapViewModel tilemapViewModel = new(_tilemapModelInstance, tileFactory);

            container.Bind<TilemapModel>().FromInstance(_tilemapModelInstance).AsSingle().NonLazy();
            container.Bind<TilemapViewModel>().FromInstance(tilemapViewModel).AsSingle().NonLazy();  
        }

        private void InstallExplotionManager(DiContainer container)
        {
            _explotionManager = new(_tilemapModelInstance);
            container.Bind<ExplotionManager>().FromInstance(_explotionManager).AsSingle().NonLazy();  
        }

        private void InstallTilemapSelectionManager(DiContainer container)
        {
            TilemapSelectionManager tilemapSelectionManager = new(_tilemapModelInstance);
            container.Bind<TilemapSelectionManager>().FromInstance(tilemapSelectionManager).AsSingle().NonLazy();  
        }
    }
}
