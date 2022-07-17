using Zenject;

namespace Project.Scripts
{
    public class LevelSceneLoader : IInitializable
    {
        private readonly SessionObserver _sessionObserver;
        private LevelAdvancer _levelAdvancer;

        public LevelSceneLoader(SessionObserver sessionObserver, LevelAdvancer levelAdvancer)
        {
            _levelAdvancer = levelAdvancer;
            _sessionObserver = sessionObserver;
        }

        public void Initialize()
        {
            _levelAdvancer.StartLevel(_sessionObserver.Level);
        }
    }
}