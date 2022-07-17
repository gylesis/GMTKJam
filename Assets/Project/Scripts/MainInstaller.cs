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
        [SerializeField] private AnimationCurvesData _curvesData;
        [SerializeField] private LevelNameText _levelNameText;
        public override void InstallBindings()
        {
            //Application.targetFrameRate = 60;

            Container.Bind<LevelNameText>().FromInstance(_levelNameText).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDestinationTracker>().AsSingle();
            
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
            Container.Bind<IPlayerMovement>().To<FourDirectionMovement>().AsTransient();
            Container.Bind<IPlayerMovement>().To<JumpMovement>().AsTransient();

            Container.Bind<IRayProvider>().To<MousePositionRayProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISelector>().To<RayCastBasedSelector>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISelectionResponse>().To<InteractionResponce>().AsSingle();

            Container.Bind<UICubicSlotContainer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StickersVisualizer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SelectedStickerObserver>().AsSingle();
            Container.BindInstance(_stickerPrefabContainer).AsSingle();

            Container.Bind<PlayerCubicSlotsBuilder>().AsSingle();
            Container.BindInstance(_curvesData);
            Container.Bind<SelectionManager>().FromComponentInHierarchy().AsSingle();

        }
    }
}