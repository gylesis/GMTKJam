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
                _inGameUIController.SetText("Name is not set");
            }
            else
            {
                _inGameUIController.SetText(level.LevelTitle);
            }

            DOVirtual.DelayedCall(4, () => _inGameUIController.HideText());


            _playerCubicSlotsBuilder.ClearPrevious();
            _buttonRestricter.SetStickersPhase();
        }

        public void GoNextLevel()
        {
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