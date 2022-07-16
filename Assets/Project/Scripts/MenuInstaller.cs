using Project.Scripts;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelsContainer _levelsContainer;
        public override void InstallBindings()
        {
            Container.Bind<LevelSceneLoader>().AsSingle();
            Container.BindInstance(_levelsContainer);
        }
    }
}
