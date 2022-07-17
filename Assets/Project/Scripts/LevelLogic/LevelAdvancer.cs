using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts
{
    public class LevelAdvancer
    {
        private readonly LevelSpawner _levelSpawner;
        private readonly SessionObserver _sessionObserver;
        private readonly PlayerFacade _playerFacade;
        private LevelNameText _levelNameText;
        private LevelFinishService _levelFinishService;
        private PlayerCubicSlotsBuilder _playerCubicSlotsBuilder;
        public Level CurrentLevel => _levelSpawner.GetCurrentLevel();

        public LevelAdvancer(LevelSpawner levelSpawner, SessionObserver sessionObserver, PlayerFacade playerFacade,
            LevelNameText levelNameText, PlayerCubicSlotsBuilder playerCubicSlotsBuilder)
        {
            _playerCubicSlotsBuilder = playerCubicSlotsBuilder;
            _levelNameText = levelNameText;
            _playerFacade = playerFacade;
            _sessionObserver = sessionObserver;
            _levelSpawner = levelSpawner;
        }

        public void StartLevel(int id)
        {
            SetLevel(id);
        }

        public void ResetLevel()
        {
            SetLevel(_sessionObserver.Level);
        }

        public void SkipLevel()
        {
            GoNextLevel();
        }
        
        private void SetLevel(int levelId)
        {
            Level level = _levelSpawner.LoadLevelById(levelId);
            level.FinishCellMoved += OnFinishCellMoved;

            _playerFacade.SpawnPlayer();
            _playerFacade.ResetPlayer();
            
            level.PlacePlayer(_playerFacade.Transform);
            
            _playerFacade.ShowPlayer();

            if (level.LevelTitle == String.Empty)
            {
                _levelNameText.SetText("Name is not set");
            }
            else
            {
                _levelNameText.SetText(level.LevelTitle);
            }

            DOVirtual.DelayedCall(4, () => _levelNameText.HideText());

            _playerCubicSlotsBuilder.ClearPrevious();
        }

        public void GoNextLevel()
        {
            var nextLevel = _sessionObserver.Level + 1;

            if (_levelSpawner.IsLastLevel())
            {
                Debug.LogWarning("This was the last level!");
                return;
            }

            _sessionObserver.SetLevel(nextLevel);

            SetLevel(nextLevel);

        }

        private async void OnFinishCellMoved(Level level)
        {
           // GoNextLevel();
            
            level.FinishCellMoved -= OnFinishCellMoved;

            //_sessionObserver.SetLevel(_sessionObserver.Level + 1);

           // Debug.Log("processed");

            //await Task.Delay(2000);

           // SetLevel(_sessionObserver.Level);
        }
    }
}