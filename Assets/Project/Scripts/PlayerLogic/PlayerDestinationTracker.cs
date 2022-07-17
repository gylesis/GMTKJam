using System;
using Zenject;

namespace Project.Scripts
{
    public class PlayerDestinationTracker : ITickable
    {
        private PlayerFacade _playerFacade;

        private bool _allowToTrack;
        private LevelInfoService _levelInfoService;
        private Cell _startCell;

        public event Action<Cell> CellChanged; 
        
        public PlayerDestinationTracker(PlayerFacade playerFacade, LevelInfoService levelInfoService)
        {
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public void StartTracking()
        {
            _allowToTrack = true;

            _startCell = _levelInfoService.GetPlayerBottomCell();
        }

        public void StopTracking()
        {
            _allowToTrack = false;
        }
        
        public void Tick()
        {
            if(_allowToTrack == false) return;

            Cell cell = _levelInfoService.GetPlayerBottomCell();

            if (cell != _startCell)
            {
                CellChanged?.Invoke(cell);
            }
            
        }
    }
}