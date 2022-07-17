using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class LevelAdvancer
    {
        private readonly LevelSpawner _levelSpawner;
        private readonly SessionObserver _sessionObserver;
        private readonly PlayerFacade _playerFacade;

        public LevelAdvancer(LevelSpawner levelSpawner, SessionObserver sessionObserver, PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
            _sessionObserver = sessionObserver;
            _levelSpawner = levelSpawner;
        }

        public void StartLevel(int id)
        {
            TryAdvanceLevel(id);
        }
                
        private bool TryAdvanceLevel(int levelId)       
        {
            if (_levelSpawner.IsLastLevel())
            {
                Debug.LogWarning("This was the last level!");
                return false;
            }

            Level level = _levelSpawner.LoadLevelById(levelId);
            level.FinishCellMoved += OnFinishCellMoved;
            _playerFacade.SpawnPlayer();

            level.PlacePlayer(_playerFacade.Transform);
            _playerFacade.ShowPlayer();
            return true;
        }

        public void GoNextLevel()
        {
            var nextLevel = _sessionObserver.Level + 1;
            if (TryAdvanceLevel(nextLevel))
            {
                _sessionObserver.SetLevel(nextLevel);
            }
        }
        
        private async void OnFinishCellMoved(Level level)
        {
            level.FinishCellMoved -= OnFinishCellMoved;
            
            _sessionObserver.SetLevel(_sessionObserver.Level + 1);

            Debug.Log("processed");

            await Task.Delay(2000);

            TryAdvanceLevel(_sessionObserver.Level);
        }
        
    }
}