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
        public Level CurrentLevel => _levelSpawner.GetCurrentLevel();

        public LevelAdvancer(LevelSpawner levelSpawner, SessionObserver sessionObserver, PlayerFacade playerFacade, LevelNameText levelNameText)
        {
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

        private bool SetLevel(int levelId)
        {
            Level level = _levelSpawner.LoadLevelById(levelId);
            level.FinishCellMoved += OnFinishCellMoved;
            _playerFacade.SpawnPlayer();

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
            
            return true;
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

        }

        private async void OnFinishCellMoved(Level level)
        {
            level.FinishCellMoved -= OnFinishCellMoved;

            _sessionObserver.SetLevel(_sessionObserver.Level + 1);

            Debug.Log("processed");

            await Task.Delay(2000);

            SetLevel(_sessionObserver.Level);
        }

    }
}