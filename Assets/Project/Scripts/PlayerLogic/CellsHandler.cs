using UnityEngine;

namespace Project.Scripts
{
    public class CellsHandler
    {
        private readonly LevelFinishService _levelFinishService;
        private readonly LevelDeathService _levelDeathService;

        public CellsHandler(LevelFinishService levelFinishService, LevelDeathService levelDeathService)
        {
            _levelDeathService = levelDeathService;
            _levelFinishService = levelFinishService;
        }
        
        public void OnFinishCell()
        {
            _levelFinishService.Finish();
            Debug.Log("WIN!");
        }

        public void OnDeath()
        {
            _levelDeathService.OnDied();
            Debug.Log("dead");
        }
        
    }
}