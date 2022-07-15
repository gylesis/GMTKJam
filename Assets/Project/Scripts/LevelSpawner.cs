using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class LevelSpawner : IInitializable
    {
        private readonly LevelInfoService _levelInfoService;
        private readonly LevelsContainer _levelsContainer;

        public LevelSpawner(LevelInfoService levelInfoService, LevelsContainer levelsContainer)
        {
            _levelsContainer = levelsContainer;
            _levelInfoService = levelInfoService;
        }

        public void Initialize()
        {
            LoadLevelById(1);
        }

        public void LoadLevelById(int id)
        {
            Level level = _levelsContainer.GetLevel(id);

            Level instance = Object.Instantiate(level);

            _levelInfoService.OnLevelSpawned(instance);
        }
    }
}