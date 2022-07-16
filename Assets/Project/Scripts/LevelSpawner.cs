using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class LevelSpawner
    {
        private int _levelId;
        private readonly LevelInfoService _levelInfoService;
        private readonly LevelsContainer _levelsContainer;
        private Level _currentLevel;

        public LevelSpawner(LevelInfoService levelInfoService, LevelsContainer levelsContainer)
        {
            _levelsContainer = levelsContainer;
            _levelInfoService = levelInfoService; 
        }

        public Level LoadLevelById(int id)
        {
            Level levelPrefab = _levelsContainer.GetLevel(id);
            Level loadedLevel = Object.Instantiate(levelPrefab);

            Debug.Log("load");  
            
            if (_currentLevel != null)
            {
                UnloadCurrentLevel();
            }

            _currentLevel = loadedLevel;
            _levelId = id;
            _levelInfoService.OnLevelSpawned(loadedLevel);

            return loadedLevel;
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