using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts
{
    public class FourDirectionMovement : IPlayerMovement
    {
        public event Action<Cell> Moved;

        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;

        public FourDirectionMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService)
        {
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public async void Move(Cell cellToMove)
        {
            var movePos = cellToMove.Pivot.position + (Vector3.up * (_playerFacade.Transform.localScale.x / 2));
            
            await _playerFacade.Transform.DOMove(movePos, 1).AsyncWaitForCompletion();

            Cell cell = _levelInfoService.GetPlayerBottomCell();

            Moved?.Invoke(cell);
        }
    }
}