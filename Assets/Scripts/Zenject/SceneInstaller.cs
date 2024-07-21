using UnityEngine;
using Zenject;

namespace SaveLoaderProject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform unitsContainer;   
        public override void InstallBindings()
        {
            Container.Bind<UnitManager>().AsSingle().NonLazy();
            Container.Bind<ResourceService>().AsSingle().NonLazy();
            Container.Bind<PrefabInitializer>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IGameRepository>().To<GameRepository>().AsSingle().NonLazy();
            Container.Bind<ISaveLoader>().To<UnitSaveLoader>().AsSingle().WithArguments(unitsContainer).NonLazy();
            Container.Bind<ISaveLoader>().To<ResourceSaveLoader>().AsSingle().NonLazy();
            Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
        }
    }

}
