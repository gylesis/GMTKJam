using Project.Scripts.Raycast;
using Project.Scripts.Raycast.Selecting;
using Project.Scripts;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [InjectOptional, SerializeField] private int _initialSceneId;

        [InjectOptional, SerializeField] private LevelsContainer _levelsContainer;
        [SerializeField] private StickerPrefabContainer _stickerPrefabContainer;

        [SerializeField] private Player _player;
        [SerializeField] private StaticData _staticData;

        public override void InstallBindings()
        {
            //Application.targetFrameRate = 60;

            Container.Bind<StaticData>().FromInstance(_staticData).AsSingle();

            Container.BindFactory<Player, PlayerFactory>().FromComponentInNewPrefab(_player).AsSingle();
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerFacade>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();

            Container.BindInstance(_levelsContainer);
            Container.BindInterfacesAndSelfTo<LevelSceneLoader>().AsSingle().NonLazy();

            Container.Bind<LevelInfoService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ButtonSpawner>().AsSingle();

            Container.BindInstance(_initialSceneId).WhenInjectedInto<LevelSpawner>();
            Container.Bind<LevelAdvancer>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();

            Container.Bind<PlayerMovementContainer>().AsSingle().NonLazy();
            Container.Bind<IPlayerMovement>().To<StraightMovement>().AsTransient();
            Container.Bind<IPlayerMovement>().To<LeftMovement>().AsTransient();

            Container.Bind<IRayProvider>().To<MousePositionRayProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISelector>().To<RayCastBasedSelector>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISelectionResponse>().To<InteractionResponce>().AsSingle();

            Container.Bind<PlayerCubicSlotContainer>().AsSingle();
            Container.Bind<SelectedStickerObserver>().AsSingle();
            Container.BindInstance(_stickerPrefabContainer).AsSingle();
        }
    }
}