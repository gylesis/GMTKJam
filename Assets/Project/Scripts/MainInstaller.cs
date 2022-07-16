using Project.Scripts;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [InjectOptional, SerializeField]
        private int _initialSceneId;

        [InjectOptional, SerializeField] private LevelsContainer _levelsContainer;
        
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container.BindFactory<Player, PlayerFactory>().FromComponentInNewPrefab(_player).AsSingle();
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerFacade>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();

            Container.BindInstance(_levelsContainer);
            Container.Bind<LevelSceneLoader>().AsSingle();

            Container.Bind<LevelInfoService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ButtonSpawner>().AsSingle();

            Container.BindInstance(_initialSceneId).WhenInjectedInto<LevelSpawner>();
            Container.Bind<LevelAdvancer>().AsSingle();
        }
    }
}