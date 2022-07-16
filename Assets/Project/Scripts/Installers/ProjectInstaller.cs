using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LevelsContainer _levelsContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelsContainer>().FromInstance(_levelsContainer).AsSingle();
            Container.Bind<SessionObserver>().AsSingle().NonLazy();
        }
    }
}