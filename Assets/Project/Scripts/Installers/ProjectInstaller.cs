using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LevelsContainer _levelsContainer;
        [SerializeField] private SoundPlayer _soundPlayer;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundPlayer>().FromInstance(_soundPlayer).AsSingle();
            Container.Bind<LevelsContainer>().FromInstance(_levelsContainer).AsSingle();
            Container.Bind<SessionObserver>().AsSingle().NonLazy();
        }
    }
}