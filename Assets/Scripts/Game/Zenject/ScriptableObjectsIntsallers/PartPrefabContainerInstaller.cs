using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Prefabs/PartPrefabContainerInstaller")]
    
    public sealed class PartPrefabContainerInstaller : ScriptableObjectInstaller<PartPrefabContainerInstaller> 
    {
        [SerializeField] private PartPrefabContainer _partPrefabContainer;

        public override void InstallBindings()
        {
            Container.Bind<PartPrefabContainer>().FromInstance(_partPrefabContainer).AsSingle().NonLazy();
        }
    }
}

