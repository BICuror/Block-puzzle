using Zenject;
using UnityEngine;

namespace Installers
{
    public class DIEntryPoint : MonoInstaller
    {   
        [SerializeField] private FactoriesInstaller _factoriesInstaller;
        [SerializeField] private PartSystemInstaller _partSystemInstaller;
        [SerializeField] private TilemapInstaller _tilemapInstaller;
        [SerializeField] private ScoringSystemInstaller _scoringSystemInstaller;

        public override void InstallBindings()
        {
            _factoriesInstaller.InstallBindings(Container);
            _partSystemInstaller.InstallBindings(Container, _factoriesInstaller.PartFactoryInstance);
            _tilemapInstaller.InstallBindings(Container, _factoriesInstaller.TileFactoryInstance);
            _scoringSystemInstaller.InstallBindings(Container, _tilemapInstaller.ExplotionManagerInstance);
        }
    }
}
