using Project.Scripts;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Scripts
{
    public class LevelSceneLoader
    {
        private const string GameplaySceneName = "Main";

        [Inject]
        private LevelsContainer _levelsContainer;
        readonly ZenjectSceneLoader _sceneLoader;
        public LevelSceneLoader(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadLevelById(int id)
        {
            _sceneLoader.LoadSceneAsync(GameplaySceneName, LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(id).WhenInjectedInto<MainInstaller>();
                container.BindInstance(_levelsContainer).WhenInjectedInto<MainInstaller>();
            });
        }


    }
}
