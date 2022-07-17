using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class LevelAdvancer : IInitializable, IDisposable
    {
        private readonly LevelSpawner _levelSpawner;
        private readonly SessionObserver _sessionObserver;
        private readonly PlayerFacade _playerFacade;
        private InGameUIController _inGameUIController;
        private LevelFinishService _levelFinishService;
        private PlayerCubicSlotsBuilder _playerCubicSlotsBuilder;
        private UICubicSlotContainer _uiCubicSlotContainer;
        private UIContainer _uiContainer;
        private ButtonRestricter _buttonRestricter;
        private Level _currentSpawnedLevel;
        public Level CurrentLevel => _levelSpawner.GetCurrentLevel();

        public LevelAdvancer(LevelSpawner levelSpawner, SessionObserver sessionObserver, PlayerFacade playerFacade,
            InGameUIController inGameUIController, PlayerCubicSlotsBuilder playerCubicSlotsBuilder,
            UICubicSlotContainer uiCubicSlotContainer, UIContainer uiContainer, ButtonRestricter buttonRestricter)
        {
            _uiContainer = uiContainer;
            _uiCubicSlotContainer = uiCubicSlotContainer;
            _playerCubicSlotsBuilder = playerCubicSlotsBuilder;
            _inGameUIController = inGameUIController;
            _playerFacade = playerFacade;
            _sessionObserver = sessionObserver;
            _levelSpawner = levelSpawner;
            _buttonRestricter = buttonRestricter;
        }

        public void Initialize()
        {
            _uiContainer.RestartLevelButton.onClick.AddListener((() => ResetLevel()));
        }
        
        public void StartLevel(int id)
        {
            if (_currentSpawnedLevel != null)
            {
                _currentSpawnedLevel.FinishCellMoved -= OnFinishCellMoved;
            }

            Debug.Log($"Start level {id}");
            SetLevel(id);
        }

        public void ResetLevel()
        {
            if (_currentSpawnedLevel != null)
            {
                _currentSpawnedLevel.FinishCellMoved -= OnFinishCellMoved;
            }
            Debug.Log("Reset Level");
            SetLevel(_sessionObserver.Level);
        }

        public void SkipLevel()
        {
            if (_currentSpawnedLevel != null)
            {
                _currentSpawnedLevel.FinishCellMoved -= OnFinishCellMoved;
            }
            Debug.Log("Skip level");
            GoNextLevel();
        }

        private void SetLevel(int levelId)
        {
            Debug.Log("Set level");
            
            _currentSpawnedLevel = _levelSpawner.LoadLevelById(levelId);
            
            _currentSpawnedLevel.FinishCellMoved += OnFinishCellMoved;
            
            _playerFacade.SpawnPlayer();
            _playerFacade.ResetPlayer();

            _currentSpawnedLevel.PlacePlayer(_playerFacade.Transform);

            _playerFacade.ShowPlayer();

            if (_currentSpawnedLevel.LevelTitle == String.Empty)
            {
                _inGameUIController.SetText("Name is not set");
            }
            else
            {
                _inGameUIController.SetText(_currentSpawnedLevel.LevelTitle);
            }

            DOVirtual.DelayedCall(4, () => _inGameUIController.HideText());


            _playerCubicSlotsBuilder.ClearPrevious();
            _buttonRestricter.SetStickersPhase();
        }

        public void GoNextLevel()
        {
            Debug.Log("Go next level");
            _uiCubicSlotContainer.ClearUISlots();

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

        public void Dispose()
        {
            _uiContainer.RestartLevelButton.onClick.RemoveAllListeners();
        }
    }
}