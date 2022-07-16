using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class LevelSpawner : IInitializable
    {
        private int _levelId;
        private readonly LevelInfoService _levelInfoService;
        private readonly LevelsContainer _levelsContainer;
        private Level _currentLevel;

        public LevelSpawner(LevelInfoService levelInfoService, LevelsContainer levelsContainer, int levelId)
        {
            _levelId = levelId;
            _levelsContainer = levelsContainer;
            _levelInfoService = levelInfoService;
        }

        public int LevelId => _levelId;

        public void Initialize()
        {
            LoadLevelById(_levelId);
        }

        public void LoadLevelById(int id)
        {
            Level level = _levelsContainer.GetLevel(id);
            Level instance = Object.Instantiate(level);

            if (_currentLevel != null)
            {
                UnloadCurrentLevel();
            }
            _currentLevel = instance;
            _levelId = id;
            _levelInfoService.OnLevelSpawned(instance);
        }

        public void UnloadCurrentLevel()
        {
            Object.Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }

        public bool IsLastLevel()
        {
            return _levelId >= _levelsContainer.Count;
        }
    }
}