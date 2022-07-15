using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelsContainer _levelsContainer;
        public override void InstallBindings()
        {
            Container.BindFactory<Player, PlayerFactory>().FromComponentInNewPrefab(_player).AsSingle();
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerFacade>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();

            Container.Bind<LevelsContainer>().FromInstance(_levelsContainer).AsSingle();
            Container.Bind<LevelInfoService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSpawner>().AsSingle();
        }
    }
}