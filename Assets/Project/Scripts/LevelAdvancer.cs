using Project.Scripts;
using Project.Scripts;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class LevelAdvancer
    {
        private LevelSpawner _levelSpawner;
        private LevelsContainer _container;

        [Inject]
        public LevelAdvancer(LevelSpawner levelSpawner, LevelsContainer container)
        {
            _levelSpawner = levelSpawner;
            _container = container;
        }

        public void TryAdvanceLevel()
        {

            if (_levelSpawner.IsLastLevel())
            {
                Debug.LogWarning("This was the last level!");
                return;
            }
            _levelSpawner.LoadLevelById(_levelSpawner.LevelId + 1);
        }
    }
}
